
using System.ComponentModel.DataAnnotations;

namespace VendMach
{
   public  class VM_Money
    {
       [Key]
        public int coinsId { get; set; } //ведь должно же быть в таблице ключевое поле? 
        public int restCoins { get; set; }
        public int coinValue { get; set; }
        //public string holder { get; set; }
        
    }

    public class User_Money
    {
        [Key]
        public int coinsId { get; set; } //ведь должно же быть в таблице ключевое поле? 
        public int restCoins { get; set; }
        public int coinValue { get; set; } 

    }
}
