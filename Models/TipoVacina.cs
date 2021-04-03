using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVC19.Models
{
    [Table("TipoVacina")]
    public class TipoVacina : Entidade
    {
        [Key]
        public int TipoVacinaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 240 caracteres")]
        [MaxLength(240, ErrorMessage = "Este campo deve conter entre 3 e 240 caracteres")]
        public string Descricao { get; set; }

        public List<Vacina> ListaVacina { get; set; }
    }
}
