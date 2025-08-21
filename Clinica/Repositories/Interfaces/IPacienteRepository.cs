using Clinica.Models;

namespace Clinica.Repositories
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente?> GetByIdAsync(int id);
        Task AddAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
        Task DeleteAsync(Paciente paciente);
    }
}
