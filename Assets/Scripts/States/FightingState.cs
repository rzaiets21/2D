using UnityEngine;

namespace States
{
    public class FightingState : MovementState
    {
        private float _exitTime = 3.5f;

        protected override void OnEnter()
        {
            base.OnEnter();
            
            _characterAnimator.UpdateState(CharacterAnimatorParams.IsFighting, true);
        }

        protected override void OnUpdate()
        {
            UpdateCharacterAnimation();
            
            if(_updateTime < _exitTime)
                return;
            
            _characterAnimator.UpdateState(CharacterAnimatorParams.IsFighting, false);
            _stateMachine.SetDefaultState();
        }
    }
}