using System;
using UnityEngine;

namespace AttackAnimations
{
    [Serializable]
    public struct AnimationSettings
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public bool ShouldCombo { get; private set; }
    }
}