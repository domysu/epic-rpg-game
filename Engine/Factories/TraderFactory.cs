using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class TraderFactory
    {

        public static readonly List<Trader> _traders = new List<Trader>();


        public static Trader GetTrader(int TraderID)
        {

            switch(TraderID)
            {
                case 1:
                    {
                        Trader susan = new Trader("Susan", "Trader Susan");
                        _traders.Add(susan);
                        AddItemToShop(susan, 1002);
                        return susan;
                    }

                case 2:
                    {
                        Trader ted = new Trader("Ted", "Trader Ted");
                        _traders.Add(ted);
                        AddItemToShop(ted, 1001);
                        return ted;

                    }
                 default:
                    throw new Exception(string.Format($"No trader with specified id: {0}, was found", TraderID));
                   


            }
        }

        public static void AddItemToShop(Trader trader, int itemID)
        {
        
        trader.Inventory.Add(new ItemQuantity(itemID,1));
        
        }


    }
}
