using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Location : BaseNotificationClass
    {

        public int XCoordinate { get; set; }
        public int YCoordinate {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public List<Quest> QuestsHere { get; set; } = new List<Quest> ();

        public List<MonsterEncounter> MonstersHere { get; set; } = new List<MonsterEncounter> ();

        public Trader TraderHere { get; set; }

        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if(MonstersHere.Exists(m => m.MonsterID == monsterID))
                {
                // Monster was already added here, rewrite chanceofencounter
                MonstersHere.FirstOrDefault(m => m.MonsterID == monsterID).EncounterChance = chanceOfEncountering;


                }
            else
            {
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));

            }

         


        }

     
   
        public Monster GetMonster()
        {
            if (!MonstersHere.Any())
            {
                return null;
            }
            // Total the percentages of all monsters at this location.
            int totalChances = MonstersHere.Sum(m => m.EncounterChance);
            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);
            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;
            foreach (MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.EncounterChance;
                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }
            // If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }

    }
}
