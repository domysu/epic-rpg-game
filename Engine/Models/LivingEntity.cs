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
        private int _hitPoints;
        private int _maximumHitpoints;
        private int _gold;

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
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; }
        public ObservableCollection<GameItem> Inventory { get; }

        public List<GameItem> Weapons => Inventory.Where(i => i.Type == GameItem.ItemType.Weapon).ToList();

        public bool IsDead => HitPoints <= 0;

        public event EventHandler OnKilled;
        protected LivingEntity(string name,int hitPoints, int maximumHitPoints, int gold) {

            Name = name;
            HitPoints = hitPoints;
            MaximumHitpoints = maximumHitPoints;
            Gold = gold;

            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void TakeDamage(int HitpointsOfDamage)
        {
            HitPoints -= HitpointsOfDamage;
            if(IsDead)
            {
                HitPoints = 0;
                RaiseOnKilledEvent();   
            }
        }
        public void Heal(int HitpointsOfHeal)
        {
            HitPoints += HitpointsOfHeal;
            if(HitPoints > MaximumHitpoints)
            {
                HitPoints = MaximumHitpoints;
            }
         }
        public void FullyHeal()
        {
            HitPoints = MaximumHitpoints;
        }
        public void ReceiveGold(int GoldAmount)
        {
            Gold += GoldAmount;
        }

        public void SpendGold(int GoldAmount)
        {
            if(GoldAmount > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} Does not have enough gold!");

            }
            Gold -= GoldAmount;
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
            GroupedInventoryItem ItemToRemove = GroupedInventory.First(x => x.Item.ItemTypeID == item.ItemTypeID);
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

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

    }
}
