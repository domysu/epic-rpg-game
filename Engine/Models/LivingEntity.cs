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
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }
        public ObservableCollection<GameItem> Inventory { get; set; }

        public List<GameItem> Weapons => Inventory.Where(i => i is Weapons).ToList();

        protected LivingEntity() { 
         
          Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            if(item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item,1));
            }
            if(!item.IsUnique)
            {
                if (!GroupedInventory.Any(x => x.Item.ItemTypeID == item.ItemTypeID))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 1));

                }
                else
                    GroupedInventory.First(x => x.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }


            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            GroupedInventoryItem ItemToRemove = GroupedInventory.First(x => x.Item == item);
            if(ItemToRemove != null)
            {
                if(ItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(ItemToRemove);
                }
                else
                {
                    ItemToRemove.Quantity--;
                }
            }
            OnPropertyChanged(nameof(Weapons));



        }


    }
}
