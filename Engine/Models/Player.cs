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
     

    
        
        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
      
   
       
      
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public ObservableCollection<Recipe> Recipes { get; }
        public Player(string name, string characterClass, int level, int experiencePoints, int hitPoints, int maximumHitpoints, int gold) : 
            base(name, hitPoints,maximumHitpoints,gold, level, experiencePoints)
        {
           CharacterClass = characterClass;
            Quests = new ObservableCollection<QuestStatus>();
            Recipes = new ObservableCollection<Recipe>();
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach(ItemQuantity item in items)
            {

                if (Inventory.Count(i=>i.ItemTypeID==item.ItemID) < item.Quantity)
                {
                    return false;
                }

            }
            return true;
        }

        public void AddRecipe(Recipe recipe)
        {
            if (!Recipes.Any(i=>i.Id == recipe.Id))
            {


                Recipes.Add(recipe);
            }
        }
        
    }
}