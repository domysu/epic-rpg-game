using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class MonsterFactory
    {
        private static readonly List<Monster> _monsters = new List<Monster>();


        public static Monster GetMonster(int MonsterID)
        {

            switch(MonsterID)
            {
                case 1:
                    {
                        Monster snake = new Monster(2, 4, "Snake", "A venomous snake", "Snake.png", 20, 25);
                        AddLootItem(snake, 9001, 75);
                        AddLootItem(snake, 9002, 25);
                        snake.CurrentWeapon = ItemFactory.CreateGameItem(5002);
                        
                        
                        return snake;
                     
                    }
                case 2:
                    {
                        Monster giantSpider = new Monster(4,6, "GiantSpider", "A dangerous spider", "GiantSpider.png", 25, 35);
                        AddLootItem(giantSpider, 9005, 25);
                        AddLootItem(giantSpider, 9006, 75);
                        giantSpider.CurrentWeapon = ItemFactory.CreateGameItem(5001);
                        return giantSpider;

                    }
                case 3:
                    {
                        Monster rat = new Monster(4, 5, "Rat", "Cheesy Rat", "Rat.png", 10, 10);
                        AddLootItem(rat, 9003, 25);
                        AddLootItem(rat, 9004, 75);
                        rat.CurrentWeapon = ItemFactory.CreateGameItem(5001);
                        return rat;
                    }
                case 4:
                    {
                        Monster wolf = new Monster(8, 12, "Wolf", "A lone wolf", "Wolf.png", 35, 50);
                           
                        return wolf;
                    }
                default:
                    throw new ArgumentException(string.Format("No monster with id '{0}' exists", MonsterID));
            }
        }
        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }

       private static void AddWeapon(Monster monster, int itemID)
        {
            monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
        }

    }
}
