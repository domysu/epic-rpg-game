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

        public Trader(string name, string description) : base(name, 99999, 999999, 999999)
        {

            Name = name;
            Description = description;
            Inventory = new ObservableCollection<GameItem>();

        }

    }
}
