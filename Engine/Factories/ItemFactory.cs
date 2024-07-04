using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();
        static ItemFactory()
        {

            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 5, 1, 3);
            BuildMisc(9001, "Snake fang", 1);
            BuildMisc(9002, "Snakeskin", 2);
            BuildMisc(9003, "Rat tail", 1);
            BuildMisc(9004, "Rat fur", 2);
            BuildMisc(9005, "Spider fang", 1);
            BuildMisc(9006, "Spider silk", 2);
        }
        public static GameItem CreateGameItem(int itemTypeID)
        {
            return  _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
          
            
        }
        public static void BuildWeapon(int id, string name, int price, int minimumDamage, int maximumDamage)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemType.Weapon, id, name, price, minimumDamage, maximumDamage, true));   
        }
        public static void BuildMisc(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemType.Misc, id, name, price));
        }
    }
}