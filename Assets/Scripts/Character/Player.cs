using System;
using System.Collections.Generic;
using Interfaces;
using States;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour, ICharacterControl
    {
        [SerializeField] private StateMachine characterStateMachine;
        
        [SerializeField] private CharacterInput characterInput;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private CharacterAnimator characterAnimator;

        public Vector2 MovementVector => characterInput.GetMovementVector();

        private State IdleState = new IdleState();
        private State MovementState;
        private State EntryAttackState;

        private void Awake()
        {
            characterStateMachine.InitDefaultState(IdleState);

            MovementState = new MovementState().Init(this, characterController, characterAnimator);
        }

        private void Start()
        {
            characterStateMachine.SetDefaultState();
        }

        private void OnEnable()
        {
            characterInput.onClickAttack += OnClickAttack;
        }

        private void OnDisable()
        {
            characterInput.onClickAttack -= OnClickAttack;
        }

        private void Update()
        {
            if (MovementVector.magnitude > 0 && characterStateMachine.CurrentState.GetType() == typeof(IdleState))
            {
                characterStateMachine.SetNextState(MovementState);
            }
        }

        private void OnClickAttack()
        {
            
        }
    }
}