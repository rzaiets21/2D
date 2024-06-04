using System;
using System.Collections.Generic;
using Attack;
using AttackAnimations;
using Interfaces;
using States;
using States.Base;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour, ICharacterControl, IAttacker
    {
        [SerializeField] private ComboSettings comboSettings;
        [SerializeField] private StateMachine characterStateMachine;
        
        [SerializeField] private CharacterInput characterInput;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private CharacterAnimator characterAnimator;

        public Vector2 MovementVector => characterInput.GetMovementVector();

        private State IdleState = new IdleState();
        private State MovementState;
        private State FightingState;
        private State AttackState => _attackStates[0];

        private List<State> _attackStates;
        
        public event Action OnAttack;
        
        private void Awake()
        {
            characterStateMachine.InitDefaultState(IdleState);

            MovementState = new MovementState().Init(this, characterController, characterAnimator);
            FightingState = new FightingState().Init(this, characterController, characterAnimator);
            BaseAttackState comboAttack = null;
            _attackStates = new List<State>();
            for (int i = comboSettings.AttacksCount - 1; i >= 0; i--)
            {
                AttackSettings attackSettings = comboSettings[i];
                AnimationSettings animationSettings = attackSettings.AnimationSettings;
                var attackState = new BaseMeleeAttack();
                attackState.Init(this, characterAnimator)
                    .SetAnimationSettings(animationSettings)
                    .AddTransition(StateEvent.Exit, FightingState);

                if (comboAttack != null && animationSettings.ShouldCombo)
                    attackState.SetComboState(comboAttack);

                _attackStates.Insert(0, attackState);
                comboAttack = attackState;
            }
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
            if (characterStateMachine.CurrentState.GetType() != typeof(BaseAttackState))
            {
                characterStateMachine.SetNextState(AttackState);
            }
            OnAttack?.Invoke();
        }
    }
}