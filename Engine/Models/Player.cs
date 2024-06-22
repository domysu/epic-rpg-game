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
    public class Player : LivingEntity
    {
        private string _characterClass;
        private int _experiencePoints;
        private int _level;


        
        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
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
      
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public Player()
        {
           
            Quests = new ObservableCollection<QuestStatus>();
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
            Inventory.Clear(); // todo: maybe not whole inventory?
            AddItemToInventory(ItemFactory.CreateGameItem(1001)); // so player has something to fight with


        }
    }
}