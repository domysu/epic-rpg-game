using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : BaseNotificationClass
    {

        public int ID;
        public int _hitpoints;
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

        public int HitPoints {
            get
            {
                return _hitpoints;
            }

            set
            {
                _hitpoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }

        }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get;set; }

        public ObservableCollection<ItemQuantity> Inventory { get; set; }


        public Monster(int hitPoints, string name, string description, string imageName, int rewardExperiencePoints, int rewardGold)
        {

            HitPoints = hitPoints;
            Name = name;
            Description = description;
            ImageName = string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;


        }

    }
}
