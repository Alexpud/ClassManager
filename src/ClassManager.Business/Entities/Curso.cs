using ClassManager.Business.Interfaces.Entities;

namespace ClassManager.Business.Entities;

public class Curso : BaseEntity, IEntidadeTempo
{
    public string Nome { get; set; }
    public string Tags { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }
    public List<Turma> Turmas { get; set; }
}