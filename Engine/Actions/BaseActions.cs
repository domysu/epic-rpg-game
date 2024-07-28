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
        protected readonly GameItem _itemUsed;
        public event EventHandler<string> OnActionPerformed;

        protected BaseActions(GameItem ItemUsed)
        {
            _itemUsed = ItemUsed;

        }
       protected void ReportResult(string message)
        {
            OnActionPerformed?.Invoke(this, message);

        }

    }
}
