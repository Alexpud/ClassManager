namespace ClassManager.Business.Dtos.Curso;

public class CriarTurmaDto
{
    public List<Guid> AlunoIds { get; set; }
    public Guid ProfessorId { get; set; }
    public Guid CursoId { get; set; }
    public string Nome { get; set; }
}
