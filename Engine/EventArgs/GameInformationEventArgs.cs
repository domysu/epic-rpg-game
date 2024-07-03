using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.EventArgs
{
    public class GameInformationEventArgs : System.EventArgs
    {
        public string Message {  get; }
        public GameInformationEventArgs(string message) {
        Message = message;
        
        }
    }
}
