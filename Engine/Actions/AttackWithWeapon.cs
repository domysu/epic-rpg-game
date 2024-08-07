﻿using System;
using Engine.Models;
namespace Engine.Actions
{
    public class AttackWithWeapon : BaseActions, IAction
    {
        private readonly int _maximumDamage;
        private readonly int _minimumDamage;
        public AttackWithWeapon(GameItem weapon, int minimumDamage, int maximumDamage) : base(weapon)
        {
            if (weapon.Type != GameItem.ItemType.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }
            if (_minimumDamage < 0)
            {
                throw new ArgumentException("minimumDamage must be 0 or larger");
            }
            if (_maximumDamage < _minimumDamage)
            {
                throw new ArgumentException("maximumDamage must be >= minimumDamage");
            }
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);
            if (damage == 0)
            {
                ReportResult($"You missed the {target.Name.ToLower()}.");
            }
            else
            {
                ReportResult($"{actor.Name} hit {target.Name} for {damage} points.");
                target.TakeDamage(damage);
            }
        }
        
    }
}