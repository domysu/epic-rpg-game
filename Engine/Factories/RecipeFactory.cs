using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = new List<Recipe>();

        static RecipeFactory() 
        {
            List<ItemQuantity> CraftingMaterials = new List<ItemQuantity>();
            
            CraftingMaterials.Add(new ItemQuantity(9001, 5));
            
            _recipes.Add(new Recipe(1, "Cookie", CraftingMaterials, 3001));

        } 
        public static Recipe GetRecipeByID(int id)
        {
            return _recipes.FirstOrDefault(recipe => recipe.Id == id);
        }
    }
}
