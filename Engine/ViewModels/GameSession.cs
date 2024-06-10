using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public World CurrentWorld { get; set; }
       public Player CurrentPlayer {  get; set; }
        public Location CurrentLocation { get; set; }
        public GameSession() { 
        
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Domis";
            CurrentPlayer.CharacterClass = "Wolf";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.Level = 1;
            
            CurrentPlayer.Gold = 123123;

            

            
            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);

        
        }

    }
}
