using System.ComponentModel.DataAnnotations;

namespace PuntoCuatroSQLiteSQLite.Models
{
    public class Ensayo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una muestra")]
        [Display(Name = "Muestra")]
        public int MuestraId { get; set; }

        public Muestra? Muestra { get; set; }

        [Required(ErrorMessage = "Debe ingresar la determinación")]
        [StringLength(80, ErrorMessage = "Máximo {1} caracteres")]
        [Display(Name = "Determinación")]
        public string Tipo { get; set; } = string.Empty;

        [Display(Name = "Resultado")]
        public string? Resultado { get; set; }

        [Required(ErrorMessage = "Debe indicar la fecha de análisis")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de análisis")]
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
