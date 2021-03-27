using System.ComponentModel.DataAnnotations;

namespace CVC19.ViewModel
{
    public class VacinaViewModel
    {
        [Display(Name = "#")]
        public string VacinaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Laboratório")]
        public string LaboratorioId { get; set; }
        public string LaboratorioNome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Tipo de Vacina")]
        public string TipoVacinaId { get; set; }
        public string TipoVacinaNome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Agente Patogênico")]
        public string AgentePatogenicoId { get; set; }
        public string AgentePatogenicoNome { get; set; }
    }
}
