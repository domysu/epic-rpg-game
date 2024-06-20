using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Engine.Models
{
    public class Player : BaseNotificationClass
    {
        private string _name;
        private string _characterClass;
        private int _hitPoints;
        private int _experiencePoints;
        private int _level;
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
        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
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
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }
        public ObservableCollection<GameItem> Inventory { get; set; }

        public List<GameItem> Weapons => Inventory.Where(i => i is Weapons).ToList();
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
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

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach(ItemQuantity item in items)
            {
                Console.WriteLine($"Debugging: {0}", item.ItemID);

                if (Inventory.Count(i=>i.ItemTypeID==item.ItemID) < item.Quantity)
                {
                    Console.WriteLine($"Debugging: {0}", item.ItemID);
                    return false;
                }

            }
            return true;
        }
        public void Death()
        {
            Inventory.Clear();
            AddItemToInventory(ItemFactory.CreateGameItem(1001));


        }
    }
}