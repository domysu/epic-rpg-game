using Engine.Factories;
using Engine.Models;

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
        
            CurrentPlayer = new Player 
            { Name = "Domis",
                CharacterClass="Fighter",
                HitPoints=10, 
                Level=1, 
                Gold=1
            };



            /*
            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();
            */

            // Without static declaration ^^

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);


            CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));
        
        }

     
        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void MoveWest()
        {

            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }
        public void MoveEast() {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }  
        
        public void MoveSouth() {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }

        public void WarpHome()
        {

            CurrentLocation = CurrentWorld.LocationAt(0, -1); // The coordinates are our homes coordinates
        }

    }
}
