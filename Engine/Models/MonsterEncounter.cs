﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class MonsterEncounter
    {
        public int MonsterID { get; set; }
        public int EncounterChance { get; set; }

        public MonsterEncounter(int monsterID, int encounterChance) {
        
        
        MonsterID = monsterID;
            EncounterChance = encounterChance;

        
        }
    }
}
