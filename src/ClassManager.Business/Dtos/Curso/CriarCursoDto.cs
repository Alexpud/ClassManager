namespace ClassManager.Business.Dtos.Curso;

public class CriarCursoDto
{
    public string Nome { get; set; }
    public Guid ProfessorId { get; set; }
    public List<string> Tags { get; set; }
}
