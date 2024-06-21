using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class LivingEntity : BaseNotificationClass
    {
        public string _name;
        public int _hitPoints;
        public int _maximumHitpoints;
        public int _gold;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
            public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }

        }

        public int Gold
        {
            get { return _gold; }
            set { 
                _gold = value; 
                OnPropertyChanged(nameof(Gold));
                }
        }
        public int MaximumHitpoints 
        { 
            get { return _maximumHitpoints; }
            set 
            { 
                _maximumHitpoints = value;
                OnPropertyChanged(nameof(MaximumHitpoints));
            } 
        }
        public ObservableCollection<GameItem> Inventory { get; set; }

        public List<GameItem> Weapons => Inventory.Where(i => i is Weapons).ToList();

        protected LivingEntity() { 
         
          Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));



        }


    }
}
