using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo5_DataAnnotations.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }

        public List<Funcionario> Funcionarios {get;set;}
    }
}