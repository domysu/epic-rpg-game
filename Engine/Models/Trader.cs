using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{ 
    public class Trader : BaseNotificationClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public Trader(string name, string description)
        {

            Name = name;
            Description = description;
            Inventory = new ObservableCollection<GameItem>();

        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

        }

        public void RemoveItemFromInventory(GameItem item)
        {
              Inventory.Remove(item);
        }
    }
}
