using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.Lecciones
{
    [Authorize(Roles = "Profesor")]
    public class EditarModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Cloudinary _cloudinary;

        [BindProperty]
        public Leccion Leccion { get; set; } = new();

        [BindProperty]
        public IFormFile? VideoFile { get; set; }

        public EditarModel(AplicationDbContext context, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            _context = context;
            _userManager = userManager;
            _cloudinary = cloudinary;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var leccion = await _context.Lecciones.FindAsync(id);
            if (leccion == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();

            var curso = await _context.Cursos.FindAsync(leccion.CursoId);
            if (curso == null || curso.InstructorId != user.Id) return Forbid();

            Leccion = leccion; // Cargar datos para editar
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();

            var leccionEnDb = await _context.Lecciones.Include(l => l.Curso)
                .FirstOrDefaultAsync(l => l.Id == Leccion.Id);

            if (leccionEnDb == null) return NotFound();

            if (leccionEnDb.Curso == null || leccionEnDb.Curso.InstructorId != user.Id) return Forbid();

            // Actualizar campos normales
            leccionEnDb.Titulo = Leccion.Titulo;
            leccionEnDb.Contenido = Leccion.Contenido;
            leccionEnDb.DuracionMinutos = Leccion.DuracionMinutos;
            leccionEnDb.Orden = Leccion.Orden ?? leccionEnDb.Orden;

            // Subir video si hay nuevo archivo
            if (VideoFile != null)
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(VideoFile.FileName, VideoFile.OpenReadStream()),
                    Folder = "lecciones_videos"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    leccionEnDb.VideoUrl = uploadResult.SecureUrl.ToString(); // << guardamos en el objeto que se guarda
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al subir el video a Cloudinary.");
                    Console.WriteLine($"Error al subir video: {uploadResult.Error?.Message}");
                    return Page();
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Cursos/Detalle", new { id = leccionEnDb.CursoId });
        }
    }
}
