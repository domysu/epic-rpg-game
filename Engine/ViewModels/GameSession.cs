using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using System.ComponentModel;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {


        private Location _currentLocation;
        public World CurrentWorld { get; set; }
       public Player CurrentPlayer {  get; set; }
        public Location CurrentLocation { get { return _currentLocation; } 
            set { 
              _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation)); // nameof  for if we gonna rename the object

                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));

            
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
        public GameSession() { 
        
            CurrentPlayer = new Player();
            CurrentPlayer.Name = "Domis";
            CurrentPlayer.CharacterClass = "Wolf";
            CurrentPlayer.HitPoints = 10;
            CurrentPlayer.Level = 1;
            
            CurrentPlayer.Gold = 123123;

            

            
            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);

        
        }

     
        public void MoveNorth()
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);        
        }
        public void MoveWest()
        {
            CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
        
        }
        public void MoveEast() {
        CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate+1, CurrentLocation.YCoordinate );
        
        }  
        
        public void MoveSouth() { 
        CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate-1);
        }

        public void WarpHome()
        {
            CurrentLocation = CurrentWorld.LocationAt(0, -1); // The coordinates are our homes coordinates
        }

    }
}
