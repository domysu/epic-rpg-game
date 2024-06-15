using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
   internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            newWorld.AddLocation(0, -1, "Home", "This is your home", "pack://application:,,,/Engine;component/Images/Home.png");

                 newWorld.AddLocation(-2, -1, "Farmer's Field", 
                "There are rows of corn growing here, with giant rats hiding between them.", 
                "pack://application:,,,/Engine;component/Images/FarmFields.png");
            newWorld.LocationAt(-2, -1).AddMonster(3, 100);
            newWorld.AddLocation(-1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "pack://application:,,,/Engine;component/Images/FarmHouse.png");
            newWorld.AddLocation(0, -1, "Home", 
                "This is your home",
                "pack://application:,,,/Engine;component/Images/Home.png");
            newWorld.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the trader.",
                "pack://application:,,,/Engine;component/Images/Trader.png");
            newWorld.AddLocation(0, 0, "Town square",
                "You see a fountain here.",
                "pack://application:,,,/Engine;component/Images/TownSquare.png");
            newWorld.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "pack://application:,,,/Engine;component/Images/TownGate.png");
            newWorld.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "pack://application:,,,/Engine;component/Images/SpiderForest.png");
            newWorld.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "pack://application:,,,/Engine;component/Images/HerbalistsHut.png");
            newWorld.LocationAt(0, 1).QuestsHere.Add(QuestFactory.GetQuestByID(1));
            newWorld.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "pack://application:,,,/Engine;component/Images/HerbalistsGarden.png");

            return newWorld;
        }
    }
}
