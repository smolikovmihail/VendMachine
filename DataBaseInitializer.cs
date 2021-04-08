using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendMach
{
    class DataBaseInitializer:DropCreateDatabaseAlways<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            base.Seed(context);
            context.VM_Money_.AddRange(new VM_Money[] {
            new VM_Money{coinValue=1,restCoins=100,coinsId=1 },
            new VM_Money{coinValue=2,restCoins=100,coinsId=2 },
            new VM_Money{coinValue=5,restCoins=100,coinsId=3},
            new VM_Money{coinValue=10,restCoins=100,coinsId=4},
            });

            context.User_Money_.AddRange(new User_Money[] {
            new User_Money{coinValue=1,restCoins=10,coinsId=1},
            new User_Money{coinValue=2,restCoins=30,coinsId=2 },
            new User_Money{coinValue=5,restCoins=20,coinsId=3 },
            new User_Money{coinValue=10,restCoins=15,coinsId=4}
                });

            context.Drinks.AddRange(new Drink[] {
                 new Drink{DrinkName="Чай/Tea", DrinkId=1,DrinkCost=13,DrinkRest=10},
                 new Drink{DrinkName="Кофе/Coffee", DrinkId=2,DrinkCost=18,DrinkRest=20},
                 new Drink{DrinkName="Кофе с молоком/Coffe with milk", DrinkId=3,DrinkCost=21,DrinkRest=20},
                 new Drink{DrinkName="Сок/Juice", DrinkId=4,DrinkCost=35,DrinkRest=15}
            });
        }
    }
}
