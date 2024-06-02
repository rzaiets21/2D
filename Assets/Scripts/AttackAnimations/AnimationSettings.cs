using UnityEngine;

namespace AttackAnimations
{
    [CreateAssetMenu(menuName = "Attack animation/New animation settings", fileName = "New Animation Settings")]
    public sealed class AnimationSettings : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public bool ShouldCombo { get; private set; }
    }
}