﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClassManager.Business.Entities;

public class Curso : BaseEntity
{
    public string Nome { get; set; }
    public Guid ProfessorId { get; set; }
    public Usuario Professor { get; set; }
    public string Tags { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }
}