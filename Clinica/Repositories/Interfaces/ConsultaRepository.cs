using Clinica.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Consultas
                .Include(c => c.PacienteConsultas)
                .ThenInclude(pc => pc.Paciente)
                .ToListAsync();
        }

        public async Task<Consulta?> GetByIdAsync(int id)
        {
            return await _context.Consultas
                .Include(c => c.PacienteConsultas)
                .ThenInclude(pc => pc.Paciente)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Consulta consulta, List<int> pacientesIds)
        {
            _context.Add(consulta);
            await _context.SaveChangesAsync();

            foreach (var pacienteId in pacientesIds)
            {
                _context.PacienteConsultas.Add(new PacienteConsulta
                {
                    PacienteId = pacienteId,
                    ConsultaId = consulta.Id
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consulta consulta, List<int> pacientesIds)
        {
            _context.Update(consulta);
            await _context.SaveChangesAsync();

            var antigos = _context.PacienteConsultas.Where(pc => pc.ConsultaId == consulta.Id);
            _context.PacienteConsultas.RemoveRange(antigos);

            foreach (var pacienteId in pacientesIds)
            {
                _context.PacienteConsultas.Add(new PacienteConsulta
                {
                    PacienteId = pacienteId,
                    ConsultaId = consulta.Id
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Consulta consulta)
        {
            var relacionamentos = _context.PacienteConsultas.Where(pc => pc.ConsultaId == consulta.Id);
            _context.PacienteConsultas.RemoveRange(relacionamentos);

            _context.Remove(consulta);
            await _context.SaveChangesAsync();
        }
    }
}

