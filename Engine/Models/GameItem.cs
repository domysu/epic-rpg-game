﻿using Engine.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {

        public enum ItemType
        {
            Misc,
            Weapon,
            Consumable
        }
        public ItemType Type { get; set; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price {  get; }
        public int MinimumDamage { get; }
        public int MaximumDamage { get; }

        public int HealAmount { get; }

        public bool IsUnique { get; }
      
        public  IAction Action { get; set; }
        public GameItem(ItemType type,int itemTypeID, string name, int price, int healAmount = 0, bool isUnique = false, IAction action = null ) {

            Type = type;
            ItemTypeID = itemTypeID;
            Name = name;    
            Price = price;
            Action = action;    
            IsUnique = isUnique;
            HealAmount = healAmount;
            
        
        }
        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        public GameItem Clone() {
        return new(Type, ItemTypeID, Name, Price, HealAmount, IsUnique, Action);    
        
        }
    }
}
