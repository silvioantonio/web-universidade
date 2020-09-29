using System.ComponentModel.DataAnnotations;

namespace WebUniversidade.Models
{
    public abstract class Pessoa
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Sobrenome { get; set; }

        [Display(Name ="Nome Completo")]
        public string NomeCompleto {
            get {
                return Nome + ", " + Sobrenome;
            } 
        }

    }
}
