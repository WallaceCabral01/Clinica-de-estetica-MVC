using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime DatadaConsulta { get; set; }

        [Required(ErrorMessage = "O tratamento é obrigatório")]
        public string Tratamento { get; set; }

        // Pacientes selecionados no formulário
        public ICollection<PacienteConsulta> PacienteConsultas { get; set; } = new List<PacienteConsulta>();
    }
}
