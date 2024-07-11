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
        private int _experiencePoints;
        private int _level;
        private int _experienceToLevelUp;
        private GameItem _currentWeapon;

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
        public int Level {
            get { return _level; }
            set
            {
                _level = value;
                ExperienceToLevelUp = _level * 35;
                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(ExperienceToLevelUp));
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
        public int ExperiencePoints {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }

        public GameItem CurrentWeapon
        {
            get { return _currentWeapon; }
            set
            {
                if(_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed -= RaiseOnActionPerformedEvent;
                }
                _currentWeapon = value;
                if(_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed += RaiseOnActionPerformedEvent;
                }
                OnPropertyChanged(nameof(CurrentWeapon));

            }
        }

        
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; }
        public ObservableCollection<GameItem> Inventory { get; }

        public event EventHandler<string> OnActionPerformed;
        public List<GameItem> Weapons => Inventory.Where(i => i.Type == GameItem.ItemType.Weapon).ToList();

        public int  ExperienceToLevelUp { 
            get 
            { return _experienceToLevelUp; } 
            set
            {
                _experienceToLevelUp = value;
              
                OnPropertyChanged(nameof(ExperienceToLevelUp));
                
            }

        }   
     

        public bool IsDead => HitPoints <= 0;
        public bool IsReadyToLevelUp => ExperiencePoints >= ExperienceToLevelUp;


        public event EventHandler OnLevelUp;
        public event EventHandler OnKilled;
        protected LivingEntity(string name,int hitPoints, int maximumHitPoints, int gold, int level, int experiencePoints) {

            Name = name;
            HitPoints = hitPoints;
            MaximumHitpoints = maximumHitPoints;
            Gold = gold;
            ExperiencePoints = experiencePoints;
            Level = level;

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
        public void UseCurrentWeaponOn(LivingEntity target)
        {
            CurrentWeapon.PerformAction(this, target);
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

        public void CheckForLevelUp()
        {
            if(IsReadyToLevelUp)
            {

                Level++;
                RaiseOnLevelUpEvent();

            }
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
        private void RaiseOnLevelUpEvent()
        {
            OnLevelUp?.Invoke(this, new System.EventArgs());
        }
        private void RaiseOnActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result); 
        }

    }
}
