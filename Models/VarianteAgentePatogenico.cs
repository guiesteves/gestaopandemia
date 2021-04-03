using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVC19.Models
{

    [Table("VarianteAgentePatogenico")]
    public class VarianteAgentePatogenico : Entidade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "#")]
        public int VarianteAgentePatogenicoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [Display(Name = "Nome Variante")]
        public string Nome { get; set; }

        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [Display(Name = "Principais Mutações")]
        public string PrincipaisMutacoes { get; set; }

        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [Display(Name = "Característica")]
        public string Caracteristica { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int AgentePatogenicoId { get; set; }

        [Display(Name = "País")]
        public int? PaisId { get; set; }

        [Display(Name = "Agente Patogênico")]
        public AgentePatogenico AgentePatogenico { get; set; }

        [Display(Name = "País")]
        public Pais Pais { get; set; }
    }
}
