using System.ComponentModel.DataAnnotations;

namespace Numeros.Models
{
    public class SumNumber
    {
        [Key]
        public int IdNumber { get; set; }
        public  required int numero { get; set; }

        public required int resultado { get; set; }
    }
}
