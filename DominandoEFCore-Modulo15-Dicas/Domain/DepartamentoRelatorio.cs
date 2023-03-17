using System;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo15_Dicas.Domain
{
    public class DepartamentoRelatorio
    {
        public string Departamento { get; set; }
        public int Colaboradores { get; set; }
    }
}