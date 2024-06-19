﻿using System.Linq;
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
        private Trader _currentTrader;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }

        public Shop CurrentShop { get; set; } = new(); // need cleanup here

  

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
                CurrentTrader = _currentLocation.TraderHere;
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

        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;
                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }


        }
        public bool HasLocationToNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        public bool HasLocationToEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        public bool HasLocationToSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
      
        public bool HasLocationToWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;


        public Weapons CurrentWeapon { get; set; }
        
        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "Domis",
                CharacterClass = "Fighter",
                HitPoints = 3,
                Gold = 0,
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
        private void DeleteMonsterAtLocation()
        {
            CurrentMonster = null;

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
                DeleteMonsterAtLocation(); // Deletes monste at current location
              
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
                // If player is killed, clear their inventory and move back to their home.
                if (CurrentPlayer.HitPoints <= 0)
                {
                    RaiseMessage("");
                    RaiseMessage($"The {CurrentMonster.Name} killed you.");
                    CurrentPlayer.Death(); // Clears Players Inventory
                    CurrentLocation = CurrentWorld.LocationAt(0, -1); // Player's home
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10; // Completely heal the player
                }
            }
        }
        
        public void BuyItem(int id)
        {
            var item = CurrentShop.AvailableItemsToBuy.FirstOrDefault(x => x.ItemTypeID == id);
            if(item != null)
            {
                if(CurrentPlayer.Gold >= item.Price) // checks if player has enough gold
                {
                    CurrentPlayer.AddItemToInventory(item); // adds item to inventory
                    CurrentPlayer.Gold -= item.Price; // subtracts currentplayers gold from price of item
                    RaiseMessage($"You have successfully bought {item.Name}");

                }
                else
                {
                    RaiseMessage("You dont have enough gold to buy this item!");
                }
                
            }

        }

        private void RaiseMessage(string message)
        {
            GameInformation?.Invoke(this, new GameInformationEventArgs(message));
        }
    }
}

  