using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.Lecciones
{
    [Authorize(Roles = "Profesor")]
    public class CrearModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly Cloudinary _cloudinary;
        [BindProperty]
        public Leccion Leccion { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int CursoId { get; set; }

        public string CursoTitulo { get; set; } = "";

        [BindProperty]
        public IFormFile? VideoFile { get; set; }

        public CrearModel(AplicationDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var curso = await _context.Cursos
                .AsNoTracking()
                .Include(c => c.Lecciones)
                .FirstOrDefaultAsync(c => c.Id == CursoId);

            if (curso == null) return NotFound();

            CursoTitulo = curso.Titulo;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var curso = await _context.Cursos
                .Include(c => c.Lecciones)
                .FirstOrDefaultAsync(c => c.Id == CursoId);

            if (curso == null)
                return NotFound();

            Leccion.CursoId = CursoId;
            Leccion.Orden = curso.Lecciones.Count + 1;

            // ✅ Subir video a Cloudinary si se adjuntó
            if (Leccion.VideoFile != null)
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(Leccion.VideoFile.FileName, Leccion.VideoFile.OpenReadStream()),
                    Folder = "lecciones_videos"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Leccion.VideoUrl = uploadResult.SecureUrl.ToString();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al subir el video a Cloudinary.");
                    return Page();
                }
            }

            _context.Lecciones.Add(Leccion);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Cursos/Detalle", new { id = CursoId });
        }
    }
}
