# O que �

Projeto simples para praticar boas pr�ticas de arquitetura e desenvolvimento de software projetando
um simples sistema de gerenciamento de cursos, com autentica��o/autoriza��o, notificaca��;

## Os requisitos que temos até agora
- Um usuário pode ser aluno, professor ou coordenador.
- Como coordenador, posso criar cursos
- Como coordenador, posso atribuir professores e alunos aos cursos
- Como coordenador posso trocar o professor de uma turma e remover/adicionar alunos em um curso
- Como coordenador, posso definir cursos e turmas para um período
- Um curso possuirá para um período apenas um professor
- Uma turma poderá ter no máximo N alunos e será definida por semestre e por curso
- Um usuário terá apenas as notas dele
- O professor da turma poderá "passar" um aluno se o coordenador também aprovar
- Os alunos poderão ser aprovados ou reprovados por nota no curso
- A nota é necessária para ser aprovado em um curso é a me
- A quantidade de provas para um curso é definida pelo professor e coordenador.
- Quando a quantidade de faltas de um aluno chegar próximo a 25%, uma notificação será enviada ao aluno informando do perigo de reprovação.


## Diagrama b�sico das entidades at� ent�o
![asdasda](/docs/ClassManagerDiagrama.png "asdasda")