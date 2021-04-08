using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendMach
{
    class EntityContext : DbContext
    {
        public EntityContext(string VMDataCon) : base(VMDataCon)
        {
            Database.SetInitializer(new DataBaseInitializer());
        }
        public DbSet <VM_Money> VM_Money_{get;set;}
        public DbSet <User_Money> User_Money_ { get; set; }
        public DbSet<Drink> Drinks { get; set; }
    }
}
