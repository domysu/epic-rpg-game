using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
       public Player CurrentPlayer {  get; set; }
        public GameSession() { 
        
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Domis";
            CurrentPlayer.CharacterClass = "Wolf";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.Level = 1;
            
            CurrentPlayer.Gold = 123123;
        
        }

    }
}
