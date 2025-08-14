using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        
        public string Cpf { get; set; }

        [Range(0, 120, ErrorMessage = "A idade deve ser entre 0 e 120 anos.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O número do telefone é obrigatório.")]
        public string Telefone { get; set; }

        public ICollection<Consulta> Consultas { get; set; }

    }
}
