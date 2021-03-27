using System.ComponentModel.DataAnnotations;

namespace CVC19.ViewModel
{
    public class VarianteAgentePatogenicoViewModel
    {

        [Display(Name = "#")]
        public string VarianteAgentePatogenicoId { get; set; }

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

        [Display(Name = "País")]
        public string PaisId { get; set; }
        public string PaisNome { get; set; }

        public string AgentePatogenicoId { get; set; }
    }
}
