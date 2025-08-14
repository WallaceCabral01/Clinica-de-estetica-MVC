using Clinica.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Paciente> GetByIdAsync(int id)
    {
        return await _context.Pacientes.FindAsync(id);
    }

    public async Task AddAsync(Paciente paciente)
    {
        await _context.Pacientes.AddAsync(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Paciente paciente)
    {
        _context.Pacientes.Update(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Paciente paciente)
    {
        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
    }
}
