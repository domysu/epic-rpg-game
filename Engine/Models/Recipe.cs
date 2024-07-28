using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemQuantity> CraftingMaterials { get; set; }
        public List<ItemQuantity> ItemToCraft { get; set; }

        public Recipe(int id, string name, List<ItemQuantity> craftingMaterials, List<ItemQuantity> itemToCraft)
        {
            Id = id;
            Name = name;
            CraftingMaterials = craftingMaterials;
            ItemToCraft = itemToCraft;
        }
        
    }
}
