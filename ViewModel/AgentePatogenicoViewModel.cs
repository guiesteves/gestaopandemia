using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVC19.ViewModel
{

    public class AgentePatogenicoViewModel
    {

        public AgentePatogenicoViewModel()
        {
            ListaVarianteAgentePatogenicoViewModel = new List<VarianteAgentePatogenicoViewModel>();
        }

        [Display(Name = "#")]
        public string AgentePatogenicoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter entre 3 e 20 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Tipo do Agente Patogênico")]
        public string TipoAgentePatogenicoId { get; set; }
        public string TipoAgentePatogenicoNome { get; set; }

        [BindProperty]
        public List<VarianteAgentePatogenicoViewModel> ListaVarianteAgentePatogenicoViewModel { get; set; }
    }
}
