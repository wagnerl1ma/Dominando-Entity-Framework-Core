using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DominandoEFCore_Modulo5_DataAnnotations.Domain
{
    [Table("TabelaAtributos")]
    [Index(nameof(Descricao), nameof(Id), IsUnique = true)]
    [Comment("Meu comentario de minha tabela")]
    public class Atributo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DatabaseGenerated: gera a coluna com o autoincremento //Obs: porem o entity ja faz isso de forma automatica sem precisar usar essa anotacao
        public int Id { get; set; }

        [Column("MinhaDescricao", TypeName = "VARCHAR(100)")]
        [Comment("Meu comentario para meu campo")]
        public string Descricao { get; set; } 

        //[Required]
        [MaxLength(255)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Observacao { get; set; } 
    }

    [Keyless]
    public class RelatorioFinanceiro
    {
        public string Descricao { get; set; } 
        public decimal Total { get; set; } 
        public DateTime Data { get; set; } 
    }

    public class Aeroporto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [NotMapped]
        public string PropriedadeTeste { get; set; } //NotMapped: nao será criado no banco de dados

        [InverseProperty("AeroportoPartida")] // se relaciona com AeroportoPartida da classe Voo
        public ICollection<Voo> VoosDePartida { get; set; }

        [InverseProperty("AeroportoChegada")] // se relaciona com AeroportoChegada da classe Voo
        public ICollection<Voo> VoosDeChegada { get; set; }
    }
    
    [NotMapped]
    public class Voo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Aeroporto AeroportoPartida { get; set; }
        public Aeroporto AeroportoChegada { get; set; }
    }
}