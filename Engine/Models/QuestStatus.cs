namespace Engine.Models
{
    public class QuestStatus : BaseNotificationClass
    {
        public bool _isCompleted;
        public Quest PlayerQuest { get; set; }
        public bool IsCompleted { get 
            { return _isCompleted; } 
            set 
            { _isCompleted = value;
               OnPropertyChanged(nameof(IsCompleted));

            } 
        }
        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}