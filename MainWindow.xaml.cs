using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace VendMach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       ObservableCollection<User_Money> uMoney;
       ObservableCollection<Drink> drinkCol;
       ObservableCollection<VM_Money> vmMoney;
        Queue<User_Money> enteredCoins;
        EntityContext context;
        
        public MainWindow()
        {
                       
            context = new EntityContext("VMDataCon");
            InitializeComponent();
            dgVM.Items.Clear();
            dgUser.Items.Clear();
            dgDrinks.Items.Clear();
            InitLists();
            uMoney = new ObservableCollection<User_Money>();
            uMoney = context.User_Money_.Local;
            drinkCol = new ObservableCollection<Drink>();
            drinkCol = context.Drinks.Local;
            vmMoney = new ObservableCollection<VM_Money>();
            vmMoney = context.VM_Money_.Local;
            enteredCoins = new Queue<User_Money>();
        }

       

        private void InitLists()
        {
            
            context.User_Money_.Load();
            dgUser.DataContext = context.User_Money_.Local;
            context.VM_Money_.Load();
            dgVM.DataContext = context.VM_Money_.Local;
            context.Drinks.Load();
            dgDrinks.DataContext = context.Drinks.Local;
        }

        public void bt_User_Drop_Coin_Click(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string coinDroped = b.Content.ToString();
            int coinDropedValue=0;
            if (coinDroped == "Бросить монету 1 рубль")
                {
                    coinDropedValue = 1;
                }
            else if (coinDroped == "Бросить монету 2 рубля")
            {
                coinDropedValue = 2;
            }
            else if(coinDroped == "Бросить монету 5 рублей")
            {
                coinDropedValue = 5;
            }
            else if (coinDroped == "Бросить монету 10 рублей")
            {
                coinDropedValue = 10;
            }
            User_Money _Money = new User_Money
            {
                coinValue = coinDropedValue,
                restCoins = 1,
                coinsId = 0
            };
            enteredCoins.Enqueue(_Money);
            /*context.User_Money_.Load();
            List<User_Money> user_Money = context.User_Money_.Local;*/ 
            foreach(User_Money money in uMoney)
            {
                if (money.coinValue == coinDropedValue&&money.restCoins>=1)
                {
                    money.restCoins -= 1;
                    int currentEnteredSumm = int.Parse(tbEntered.Text);
                    currentEnteredSumm += coinDropedValue;
                    tbEntered.Text = (currentEnteredSumm.ToString());
                    context.SaveChanges();
                    dgUser.DataContext = null;
                    dgUser.DataContext = context.User_Money_.Local;
                }
            }
        }

        public void bt_Drink_Choice_Click(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string choosenDrink = b.Content.ToString();
            //string cDrink = b.Name.ToString();
            int enteredSumm = int.Parse(tbEntered.Text);
            foreach(Drink drink in drinkCol)
            {
                if(choosenDrink==drink.DrinkName&&drink.DrinkCost<=enteredSumm&&drink.DrinkRest>=1)
                {
                    drink.DrinkRest -= 1;
                    if (drink.DrinkRest == 0) b.IsEnabled = false;
                    enteredSumm -= drink.DrinkCost;
                    VM_Get_Money(drink.DrinkCost);
                    tbEntered.Text = enteredSumm.ToString();
                    context.SaveChanges();
                    dgDrinks.DataContext = null;
                    dgDrinks.DataContext = context.Drinks.Local;
                    MessageBox.Show("Спасибо!","Спасибо!");
                    break;
                }
                else if (choosenDrink == drink.DrinkName && drink.DrinkCost > enteredSumm)
                {
                    MessageBox.Show("Недостаточно средств!", "Внимание!");
                    break;
                }
                
            }
           
        }

        public void VM_Get_Money(int drinkCost)
        {
            while(enteredCoins.Count>0)
            {
                User_Money coin = enteredCoins.Dequeue();
                foreach (VM_Money vM_Money in vmMoney)
                {
                    if(vM_Money.coinValue==coin.coinValue)
                    {
                        vM_Money.restCoins++;
                        context.SaveChanges();
                        dgVM.DataContext = null;
                        dgVM.DataContext = context.VM_Money_.Local;
                    }
                }
            }
        }

        public void bt_Get_Rest_Click(object sender,RoutedEventArgs e)
        {
            int rest = int.Parse(tbEntered.Text);
            int coinNumber = 0;
            int tmp = rest;
            int[] rests=new int [4];
           
            if(enteredCoins.Count!=0)
            {
                VM_Get_Money(0);
            }

            int[] coinsValue = { 10, 5, 2, 1 };
            
                for (int i = 0; i < coinsValue.Length; i++)
                {
                    foreach (VM_Money money in vmMoney)
                    { 
                        if (money.coinValue == coinsValue[i])
                        {            
                            coinNumber = rest / coinsValue[i];
                            MessageBox.Show("Монет по " + coinsValue[i] + "даю" + coinNumber,"Debug info");
                            if (coinNumber <= money.restCoins)
                             {
                                money.restCoins -= coinNumber;
                                rest -= coinNumber * coinsValue[i];
                                rests[i] = coinNumber;
                                break;
                            }
                            else
                            {
                            rests[i] = money.restCoins;
                            money.restCoins = 0;
                            rest -= rests[i] * coinsValue[i];
                            break;
                            }
                        }
                }
                
                context.SaveChanges();
                dgVM.DataContext = null;
                dgVM.DataContext = context.VM_Money_.Local;
            }
            
            foreach(User_Money money in uMoney)
            {
                for (int i = 0; i < coinsValue.Length; i++)
                {
                    if (money.coinValue == coinsValue[i])
                    {
                        money.restCoins += rests[i];
                        break;
                    }
                }
                
                context.SaveChanges();
                dgUser.DataContext = null;
                dgUser.DataContext = context.User_Money_.Local;
            }
            tbEntered.Text = "0";
            
            
        }
    }
}
