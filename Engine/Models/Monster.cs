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

       
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExperiencePoints { get; set; }



        public Monster(int hitPoints, int maximumHitpoints, string name, string description, string imageName, int rewardExperiencePoints,
            int gold, int minimumDamage, int maximumDamage) : base(name,hitPoints, maximumHitpoints, hitPoints)
        {

            HitPoints = hitPoints;
            MaximumHitpoints = maximumHitpoints;
            Name = name;
            Description = description;
            ImageName = $"pack://application:,,,/Engine;component/Images/Monsters/{imageName}";
            RewardExperiencePoints = rewardExperiencePoints;
            Gold = gold;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;


        }

    }
}
    