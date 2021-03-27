using System.ComponentModel.DataAnnotations;

namespace CVC19.ViewModel
{
    public class TipoAgentePatogenicoViewModel
    {

        [Display(Name = "#")]
        public string TipoAgentePatogenicoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}
