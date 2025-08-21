using Clinica.Models;

namespace Clinica.Repositories
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<Consulta>> GetAllAsync();
        Task<Consulta?> GetByIdAsync(int id);
        Task AddAsync(Consulta consulta, List<int> pacientesIds);
        Task UpdateAsync(Consulta consulta, List<int> pacientesIds);
        Task DeleteAsync(Consulta consulta);
    }
}
