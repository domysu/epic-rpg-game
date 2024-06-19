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

        public ObservableCollection<ItemQuantity> Inventory { get; set; }
        public Trader( string name, string description)
        {

        Name = name;
        Description = description;
        Inventory = new ObservableCollection<ItemQuantity>();
        
        }
    }
}
