using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{ 
    public class Trader : LivingEntity
    {
        public string Description { get; set; }

        public Trader(string name, string description)
        {

            Name = name;
            Description = description;
            Inventory = new ObservableCollection<GameItem>();

        }

    }
}
