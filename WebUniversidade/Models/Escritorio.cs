using System.ComponentModel.DataAnnotations;

namespace WebUniversidade.Models
{
    public class Escritorio
    {
        [Key]
        public int InstrutorId { get; set; }

        [StringLength(50)]
        [Display(Name = "Localizacao do Escritorio")]
        public string Localizacao { get; set; }

        public Instrutor Instrutor { get; set; }
    }
}