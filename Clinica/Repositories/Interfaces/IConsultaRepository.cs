using Clinica.Models;

public interface IConsultaRepository
{
    Task<IEnumerable<Consulta>> GetAllAsync();
    Task<Consulta> GetByIdAsync(int id);
    Task AddAsync(Consulta consulta);
    Task UpdateAsync(Consulta consulta);
    Task DeleteAsync(Consulta consulta);
    Task<IEnumerable<Consulta>> GetByPacienteIdAsync(int pacienteId);
}
