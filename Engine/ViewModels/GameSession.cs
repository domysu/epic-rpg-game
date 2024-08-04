using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private Player _currentPlayer;
        private Player _currentConsumable;
        public World CurrentWorld { get; set; }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed -= OnCurrentPlayerActionPerformed;
                    _currentPlayer.OnLevelUp -= OnLevelingUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed += OnCurrentPlayerActionPerformed;
                    _currentPlayer.OnLevelUp += OnLevelingUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        }

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
                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
                CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                if (CurrentMonster != null)
                {
                    _currentMonster.OnActionPerformed -= OnCurrentMonsterActionPerformed;
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if (CurrentMonster != null)
                {
                    _currentMonster.OnActionPerformed += OnCurrentMonsterActionPerformed;
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;
                    RaiseMessage($"You have encountered {CurrentMonster.Name}!!");
                }

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
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

        public GameSession()
        {
            CurrentPlayer = new Player("Domis", "Fighter", 1, 0, 10, 10, 0);

            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentPlayer.AddRecipe(RecipeFactory.GetRecipeByID(1));
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
            if (CurrentLocation != CurrentWorld.LocationAt(0, -1))
            {
                RaiseMessage(" ");
                RaiseMessage("You have safely traveled home! ");
            }
            CurrentLocation = CurrentWorld.LocationAt(0, -1); // The coordinates are our home's coordinates
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsHere)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID &&
                                                             !q.IsCompleted);
                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }
                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");
                        // Give the player the quest rewards
                        CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;

                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold");
                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"You receive a {rewardItem.Name}");
                        }
                        CurrentPlayer.CheckForLevelUp();
                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
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
            if (CurrentPlayer.CurrentWeapon == null)
            {
                RaiseMessage(" ");
                RaiseMessage("Please select a weapon before attacking");
                return;
            }

            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);

            if (CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }
            else
            {
                CurrentMonster.UseCurrentWeaponOn(CurrentPlayer);
            }
        }

        public void OnConsumableUsed()
        {
            if (CurrentPlayer.Healable)
            {
                CurrentPlayer.UseConsumable();
                RaiseMessage(" ");
                RaiseMessage("You have successfully healed! ");
            }
            else
            {
                RaiseMessage(" ");
                RaiseMessage("You are already at maximum hitpoints!");
            }
        }

        public void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage(" ");
            RaiseMessage($"You have been killed by {CurrentMonster.Name}");
            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.FullyHeal();
        }

        public void OnLevelingUp(object sender, System.EventArgs eventArgs)
        {
            CurrentPlayer.CheckForLevelUp();
            RaiseMessage(" ");
            OnPropertyChanged(nameof(CurrentPlayer.ExperienceToLevelUp));
            RaiseMessage("You have leveled up!");
        }

        public void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage(" ");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");
            CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;

            RaiseMessage(" ");
            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");

            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);
            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                CurrentPlayer.AddItemToInventory(gameItem);
                RaiseMessage($"You receive one {gameItem.Name}.");
            }
            CurrentPlayer.CheckForLevelUp();
        }

        #region events

        private void OnCurrentPlayerActionPerformed(object sender, string result)
        {
            RaiseMessage(result);
        }

        private void OnCurrentMonsterActionPerformed(object sender, string result)
        {
            RaiseMessage(result);
        }

        public void BuyItem(int id)
        {
            var item = CurrentTrader.Inventory.FirstOrDefault(x => x.ItemTypeID == id);
            if (item != null)
            {
                try
                {
                    CurrentPlayer.SpendGold(item.Price);
                    CurrentPlayer.AddItemToInventory(item);
                }
                catch (ArgumentOutOfRangeException)
                {
                    RaiseMessage("Item was not bought!");
                }
            }
        }

        public void CraftItem(int id)
        {
            var recipe = CurrentPlayer.Recipes.FirstOrDefault(l => l.Id == id);
            if (recipe == null)
            {
                RaiseMessage("Recipe not found!");
                return;
            }

            if (CurrentPlayer.HasAllTheseItems(recipe.CraftingMaterials))
            {
                var item = ItemFactory.CreateGameItem(recipe.ItemToCraft);

                try
                {
                    // Remove crafting materials from inventory
                    foreach (var itemQuantity in recipe.CraftingMaterials)
                    {
                        for (int i = 0; i < itemQuantity.Quantity; i++)
                        {
                            var material = CurrentPlayer.Inventory.FirstOrDefault(it => it.ItemTypeID == itemQuantity.ItemID);
                            if (material != null)
                            {
                                CurrentPlayer.RemoveItemFromInventory(material);
                            }
                        }
                    }

                    // Add crafted item to inventory
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You crafted a {item.Name}.");
                }
                catch (Exception ex)
                {
                    RaiseMessage($"Crafting failed: {ex.Message}");
                }
            }
            else
            {
                RaiseMessage("You do not have the required materials to craft this item.");
            }
        }

        #endregion

        public void RaiseMessage(string message)
        {
            GameInformation?.Invoke(this, new GameInformationEventArgs(message));
        }
    }
}
