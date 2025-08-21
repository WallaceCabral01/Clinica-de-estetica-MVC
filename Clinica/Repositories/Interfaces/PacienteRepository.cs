using Clinica.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes
                .Include(p => p.PacienteConsultas)
                .ThenInclude(pc => pc.Consulta)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Paciente paciente)
        {
            _context.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paciente paciente)
        {
            _context.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Paciente paciente)
        {
            _context.Remove(paciente);
            await _context.SaveChangesAsync();
        }
    }
}

