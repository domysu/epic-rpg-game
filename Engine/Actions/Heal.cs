using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class Heal : BaseActions, IAction
    {
        private readonly GameItem _item;
        private readonly int _healAmount;
        public event EventHandler<string> OnActionPerformed;
        public Heal(GameItem item, int healAmount) : base(item)
        {
            _item = item;
            _healAmount = healAmount;

        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            
            target.Heal(_healAmount);

        }
        
    }
}
