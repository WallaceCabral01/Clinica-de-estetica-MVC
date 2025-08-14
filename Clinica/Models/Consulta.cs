namespace Clinica.Models
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public string Procedimento { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DatadaConsulta { get; set; }
    }
}
