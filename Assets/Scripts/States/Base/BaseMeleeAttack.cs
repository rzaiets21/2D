using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace States.Base
{
    public class BaseMeleeAttack : BaseAttackState
    {
        protected BoxCollider2D _hitCollider;

        protected List<Collider2D> _collidersDamaged;

        protected override void OnInit()
        {
            _collidersDamaged = new List<Collider2D>();
        }

        protected override void Attack()
        {
            var collidersToDamage = new Collider2D[10];
            var filter = new ContactFilter2D
            {
                useTriggers = true
            };

            var colliderCount = Physics2D.OverlapCollider(_hitCollider, filter, collidersToDamage);
            for (int i = 0; i < colliderCount; i++)
            {
                if (_collidersDamaged.Contains(collidersToDamage[i])) continue;
                var damageable = collidersToDamage[i].GetComponentInChildren<IDamageable>();

                if (damageable == null || (_damageableTeam & damageable.Team) != damageable.Team) continue;
                _collidersDamaged.Add(collidersToDamage[i]);
            }
        } 
    }
}