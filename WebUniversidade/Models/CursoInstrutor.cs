namespace WebUniversidade.Models
{
    public class CursoInstrutor
    {
        public int CursoId { get; set; }
        public int InstrutorId { get; set; }
        public Curso Curso { get; set; }
        public Instrutor Instrutor { get; set; }
    }
}