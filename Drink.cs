
using System.ComponentModel.DataAnnotations;

namespace VendMach
{
    public class Drink
    {
        [Key]
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int DrinkCost { get; set; }
        public int DrinkRest { get; set; }
    }
}
