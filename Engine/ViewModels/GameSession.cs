﻿using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.ViewModels
{
    internal class GameSession
    {
        Player CurrentPlayer {  get; set; }
        public GameSession() { 
        
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Domis";
            CurrentPlayer.Gold = 123123;
        
        }

    }
}
