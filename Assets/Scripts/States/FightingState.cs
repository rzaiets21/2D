using Equipment;
using Interfaces;

namespace States
{
    public class FightingState : MovementState
    {
        private float _exitTime = 3.5f;
        private PlayerEquipment _playerEquipment;

        public MovementState Init(ICharacterControl characterControl, IMovable movable, CharacterAnimator characterAnimator, PlayerEquipment playerEquipment)
        {
            _playerEquipment = playerEquipment;
            return base.Init(characterControl, movable, characterAnimator);
        }

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