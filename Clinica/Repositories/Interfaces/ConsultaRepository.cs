using Clinica.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;

        public ConsultaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            // Inclui o Paciente na consulta para evitar lazy loading nulo
            return await _context.Consultas
                .Include(c => c.Paciente)
                .ToListAsync();
        }

        public async Task<Consulta> GetByIdAsync(int id)
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(c => c.ConsultaId == id);
        }

        public async Task AddAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Consulta consulta)
        {
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
        }

        // Se quiser um método específico para buscar consultas por paciente
        public async Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId)
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .Where(c => c.PacienteId == pacienteId)
                .ToListAsync();
        }
    }
}

