﻿using Interfaces;
using UnityEngine;

namespace States
{
    public class MovementState : State
    {
        private ICharacterControl _characterControl;
        private IMovable _movable;
        protected CharacterAnimator _characterAnimator;

        private Vector2 _lastMovementVector;
        
        public MovementState Init(ICharacterControl characterControl, IMovable movable, CharacterAnimator characterAnimator)
        {
            _characterControl = characterControl;
            _movable = movable;
            _characterAnimator = characterAnimator;

            return this;
        }

        protected override void OnUpdate()
        {
            UpdateCharacterAnimation();
            
            var normalizedSpeed = _movable.GetNormalizedSpeed();
            if(normalizedSpeed != 0 || _characterControl.MovementVector.sqrMagnitude != 0)
                return;
            
            _stateMachine.SetDefaultState();
        }

        protected void UpdateCharacterAnimation()
        {
            var normalizedSpeed = _movable.GetNormalizedSpeed();
            _characterAnimator.UpdateState(CharacterAnimatorParams.HorizontalSpeed, normalizedSpeed);
        }
        
        protected override void OnFixedUpdate()
        {
            var movementVector = _characterControl.MovementVector;
            movementVector.y = 0;
            
            if(movementVector.sqrMagnitude > 0)
            {
                _lastMovementVector = movementVector;
            }
            
            if(_lastMovementVector.sqrMagnitude > 0)
            {
                var targetRotation = Quaternion.LookRotation(_lastMovementVector, Vector3.up);
                _movable.Rotate(targetRotation);
            }
            
            _movable.Move(movementVector, 1);
        }

        protected override void OnExit()
        {
            _characterAnimator.UpdateState(CharacterAnimatorParams.HorizontalSpeed, 0);
            _movable.ForceStop();
        }
    }
}