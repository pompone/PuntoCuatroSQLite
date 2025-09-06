using System.ComponentModel.DataAnnotations;

namespace PuntoCuatroSQLite.Models
{
    public class Muestra
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(120, ErrorMessage = "Máximo {1} caracteres")]
        public string Nombre { get; set; } = string.Empty;

        public string? Matriz { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de toma")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de toma")]
        public DateTime FechaToma { get; set; } = DateTime.Now;

        public List<Ensayo> Ensayos { get; set; } = new();
    }
}


