using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        public static List<GameItem> _standardGameItems;


        static ItemFactory()
        {

            _standardGameItems = new List<GameItem>();
            _standardGameItems.Add(new Weapons(1001, "Wooden Stick",100, 1, 2));
            _standardGameItems.Add(new Weapons(1002, "Wooden Sword", 250, 2, 4));
        }
        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if(standardItem != null) {
            return standardItem.Clone();
            
            }
            return null;

        }
    }
}
