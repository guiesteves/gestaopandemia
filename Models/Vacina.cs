using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVC19.Models
{
    [Table("Vacina")]
    public class Vacina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "#")]
        public int VacinaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter entre 3 e 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Laboratório")]
        public int LaboratorioId { get; set; }

        public Laboratorio Laboratorio { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Tipo de Vacina")]
        public int TipoVacinaId { get; set; }

        public TipoVacina TipoVacina { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Agente Patogênico")]
        public int AgentePatogenicoId { get; set; }
        public AgentePatogenico AgentePatogenico { get; set; }

    }
}
