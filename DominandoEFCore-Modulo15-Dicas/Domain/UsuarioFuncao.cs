using System;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo15_Dicas.Domain
{
    [Keyless]
    public class UsuarioFuncao
    {
        public Guid UsuarioId { get; set; }
        public Guid FuncaoId { get; set; }
    }
}