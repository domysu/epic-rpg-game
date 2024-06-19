using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Shop 
    {

        public ObservableCollection<GameItem> AvailableItemsToBuy { get; set; }

        public Shop() {
        AvailableItemsToBuy = new ObservableCollection<GameItem>();
        
        }



        public void AddItemToShop(GameItem item)
        {
            AvailableItemsToBuy.Add(item);
           
        }

        
        

        
    }
}
