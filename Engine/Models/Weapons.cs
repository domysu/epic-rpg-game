using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    internal class Weapons : GameItem
    {

        public int MinimumDamage {  get; set; }
        public int MaximumDamage { get; set; }
        public Weapons(int itemTypeID, string name, int price, int minimumDamage, int maximumDamage) : base(itemTypeID, name, price)

        {
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
        }

     

        public new Weapons Clone()
        {
            return new Weapons(ItemTypeID, Name, Price,MinimumDamage, MaximumDamage);
        }
    }
}
