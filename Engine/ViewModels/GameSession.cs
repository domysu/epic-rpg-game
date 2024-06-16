using System.Linq;
using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;
namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {

        public EventHandler<GameInformationEventArgs> GameInformation;
        private Location _currentLocation;
        private Monster _currentMonster;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
            }
        }

        public Monster CurrentMonster {
            get { return _currentMonster; }


            set {
                _currentMonster = value;
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if(CurrentMonster != null)
                {
                    RaiseMessage($"You have encountered {CurrentMonster.Name}!!");
                }
            } 
        }
        public bool HasLocationToNorth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
            }
        }
        public bool HasLocationToEast
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
            }
        }
        public bool HasLocationToSouth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
            }
        }
        public bool HasLocationToWest
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
            }
        }
        public Weapons CurrentWeapon { get; set; }
        public bool HasMonster => CurrentMonster != null;
        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "Domis",
                CharacterClass = "Fighter",
                HitPoints = 10,
                Gold = 1000000,
                ExperiencePoints = 0,
                Level = 1
            };
            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }
        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }
        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }
        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }

        public void WarpHome()
        {

            if (CurrentLocation != CurrentWorld.LocationAt(0,-1))
            {
                RaiseMessage("You have safely traveled home! ");
            }
            CurrentLocation = CurrentWorld.LocationAt(0, -1); // The coordinates are our home's coordinates
         
          
        }
        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                    RaiseMessage($"You have received a quest: {quest.Name}");
                }
            }
        }


        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();


        }
        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;
            }
            // Determine damage to monster
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);
            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}.");
            }
            else
            {
                CurrentMonster.HitPoints -= damageToMonster;
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} points.");
            }
            // If monster if killed, collect rewards and loot
            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");
                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
                CurrentPlayer.Gold += CurrentMonster.RewardGold;
                RaiseMessage($"You receive {CurrentMonster.RewardGold} gold.");
                foreach (ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    GameItem item = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You receive {itemQuantity.Quantity} {item.Name}.");
                }
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // If monster is still alive, let the monster attack
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);
                if (damageToPlayer == 0)
                {
                    RaiseMessage("The monster attacks, but misses you.");
                }
                else
                {
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
                }
                // If player is killed, move them back to their home.
                if (CurrentPlayer.HitPoints <= 0)
                {
                    RaiseMessage("");
                    RaiseMessage($"The {CurrentMonster.Name} killed you.");
                    CurrentLocation = CurrentWorld.LocationAt(0, -1); // Player's home
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10; // Completely heal the player
                }
            }
        }

        private void RaiseMessage(string message)
        {
            GameInformation?.Invoke(this, new GameInformationEventArgs(message));
        }
    }
}

  