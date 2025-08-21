using Clinica.Models;

public class PacienteConsulta
{
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; }

    public int ConsultaId { get; set; }
    public Consulta Consulta { get; set; }
}
