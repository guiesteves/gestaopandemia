using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVC19.Models
{

    [Table("AgentePatogenico")]
    public class AgentePatogenico : Entidade
    {
        public AgentePatogenico()
        {
            ListaVarianteAgentePatogenico = new List<VarianteAgentePatogenico>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentePatogenicoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int TipoAgentePatogenicoId { get; set; }

        public TipoAgentePatogenico TipoAgentePatogenico { get; set; }

        public List<VarianteAgentePatogenico> ListaVarianteAgentePatogenico { get; set; }
    }
}
