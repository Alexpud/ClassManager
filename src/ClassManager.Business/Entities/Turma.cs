using ClassManager.Business.Interfaces.Entities;

namespace ClassManager.Business.Entities;

public class Turma : BaseEntity, IEntidadeTempo
{
    public Guid SemestreId { get; set; }
    public string Nome { get; set; }
    public Curso Curso { get; set; }
    public Guid CursoId { get; set; }
    public Usuario Professor { get; set; }
    public Guid ProfessorId { get; set; }
    public List<Usuario> Alunos { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }
}