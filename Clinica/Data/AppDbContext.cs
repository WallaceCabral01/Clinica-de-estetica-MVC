using Microsoft.EntityFrameworkCore;

namespace Clinica.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<PacienteConsulta> PacienteConsultas { get; set; } // <- adiciona a tabela de junção

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a chave composta
            modelBuilder.Entity<PacienteConsulta>()
                .HasKey(pc => new { pc.PacienteId, pc.ConsultaId });

            // Relacionamento PacienteConsulta -> Paciente
            modelBuilder.Entity<PacienteConsulta>()
                .HasOne(pc => pc.Paciente)
                .WithMany(p => p.PacienteConsultas)
                .HasForeignKey(pc => pc.PacienteId);

            // Relacionamento PacienteConsulta -> Consulta
            modelBuilder.Entity<PacienteConsulta>()
                .HasOne(pc => pc.Consulta)
                .WithMany(c => c.PacienteConsultas)
                .HasForeignKey(pc => pc.ConsultaId);
        }
    }
}

