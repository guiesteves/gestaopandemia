using System.ComponentModel.DataAnnotations;

namespace CVC19.ViewModel
{
    public class LaboratorioViewModel
    {
        [Display(Name = "#")]
        public string LaboratorioId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 3 e 200 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Este campo é obrigatório")]
        public string PaisId { get; set; }

        public string NomePais { get; set; }

    }
}
