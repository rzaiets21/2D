using System;
using AttackAnimations;

namespace Attack
{
    [Serializable]
    public struct AttackSettings
    {
        public AttackType AttackType;
        public AnimationSettings AnimationSettings;
    }
}