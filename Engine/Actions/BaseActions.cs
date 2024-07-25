using Engine.EventArgs;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public abstract class BaseActions
    {
        private readonly GameItem _itemUsed;
        private readonly EventHandler<string> OnActionPerformed;

        public  BaseActions(GameItem ItemUsed)
        {
            _itemUsed = ItemUsed;

        }
        private void ReportResult(string message)
        {
            OnActionPerformed?.Invoke(this, message);

        }

    }
}
