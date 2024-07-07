using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {

        public enum ItemType
        {
            Misc,
            Weapon  
        }
        public ItemType Type { get; set; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price {  get; }
        public int MinimumDamage { get; }
        public int MaximumDamage { get; }

        public bool IsUnique { get; }
     
        public GameItem(ItemType type,int itemTypeID, string name, int price, int minimumDamage = 0, int maximumDamage = 0, bool isUnique = false ) {

            Type = type;
            ItemTypeID = itemTypeID;
            Name = name;    
            Price = price;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            IsUnique = isUnique;
        
            
        
        }
        public GameItem Clone() {
        return new(Type, ItemTypeID, Name, Price,MinimumDamage, MaximumDamage, IsUnique);    
        
        }
    }
}
