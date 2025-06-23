using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Data;

public class AplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Leccion> Lecciones { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
    public DbSet<ProgresoLeccion> ProgresosLecciones { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Reseña> Resenas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relaciones opcionales o claves foráneas explícitas si lo deseas
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.CursosImpartidos)
            .WithOne(c => c.Instructor)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProgresoLeccion>()
            .HasIndex(p => new { p.UsuarioId, p.LeccionId })
            .IsUnique();
    }
}