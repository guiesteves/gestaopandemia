using System.ComponentModel.DataAnnotations;

namespace CVC19.ViewModel
{
    public class TipoVacinaViewModel
    {
        [Display(Name = "#")]
        public string TipoVacinaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 240 caracteres")]
        [MaxLength(240, ErrorMessage = "Este campo deve conter entre 3 e 240 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
