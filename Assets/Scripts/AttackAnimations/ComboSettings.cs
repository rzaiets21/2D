using System.Collections.Generic;
using Attack;
using UnityEngine;

namespace AttackAnimations
{
    [CreateAssetMenu(menuName = "Combo/Create Combo Settings", fileName = "New Combo Settings")]
    public sealed class ComboSettings : ScriptableObject
    {
        [SerializeField] private List<AttackSettings> attackSettings = new List<AttackSettings>();

        public int AttacksCount => attackSettings.Count;

        public AttackSettings this[int index] => attackSettings[index];
    }
}