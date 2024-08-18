using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
   
        public string Description { get; }
        public string ImageName { get; }
      
        public int RewardExperiencePoints { get; }



        public Monster(int hitPoints, int maximumHitpoints, string name, string description, string imageName, int rewardExperiencePoints,
            int gold) : base(name,hitPoints, maximumHitpoints, hitPoints, -1, -1)
        {

            HitPoints = hitPoints;
            MaximumHitpoints = maximumHitpoints;
            Name = name;
            Description = description;
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}";
            RewardExperiencePoints = rewardExperiencePoints;
            Gold = gold;
            


        }

    }
}
    