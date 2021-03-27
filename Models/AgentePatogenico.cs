using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVC19.Models
{

    [Table("AgentePatogenico")]
    public class AgentePatogenico
    {

        public AgentePatogenico()
        {
            ListaVarianteAgentePatogenico = new List<VarianteAgentePatogenico>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "#")]
        public int AgentePatogenicoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Tipo do Agente Patogênico")]
        public int TipoAgentePatogenicoId { get; set; }

        [Display(Name = "Tipo do Agente Patogênico")]
        public TipoAgentePatogenico TipoAgentePatogenico { get; set; }

        [BindProperty]
        public List<VarianteAgentePatogenico> ListaVarianteAgentePatogenico { get; set; }
    }
}
