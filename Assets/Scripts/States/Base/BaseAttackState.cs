using System;
using AttackAnimations;
using Interfaces;
using Model;
using UnityEngine;

namespace States.Base
{
    public abstract class BaseAttackState : State
    {
        protected IAttacker _attacker;
        protected CharacterAnimator _animator;
        
        protected AnimationSettings _animationSettings;
        
        protected float _attackPressedTimer;
        protected bool _shouldCombo;

        protected UnitTeam _damageableTeam;

        protected bool IsInitialized;
        
        private BaseAttackState _comboState;
        
        protected virtual int AttackTriggerId => CharacterAnimatorParams.AttackTrigger;
        
        public BaseAttackState Init(IAttacker attacker, CharacterAnimator characterAnimator)
        {
            _animator = characterAnimator;
            _attacker = attacker;
            
            OnInit();

            IsInitialized = true;
            return this;
        }

        public BaseAttackState SetComboState(BaseAttackState attackState)
        {
            _comboState = attackState;
            Debug.Log($"Set combo state {attackState.GetType()}");
            return this;
        }
        
        protected virtual void OnInit() { }
        
        public BaseAttackState SetAnimationSettings(AnimationSettings settings)
        {
            _animationSettings = settings;
            return this;
        }
        
        public BaseAttackState SetDamageableTeam(UnitTeam damageableTeam)
        {
            _damageableTeam = damageableTeam;
            return this;
        }

        protected override void OnStateReset()
        {
            _shouldCombo = false;
            _attackPressedTimer = 0;
        }

        protected override void OnEnter()
        {
            if (!IsInitialized)
                throw new NullReferenceException("State is not initialized!");

            _attacker.OnAttack += OnAttack;
            
            _animator.SetTrigger(AttackTriggerId);
        }

        protected override void OnUpdate()
        {
            _attackPressedTimer -= Time.deltaTime;

            if (_animator.GetFloat(CharacterAnimatorParams.AttackFrames) > 0f)
            {
                Attack();
            }

            if (_animator.GetFloat(CharacterAnimatorParams.ComboFrames) > 0f && _attackPressedTimer > 0)
            {
                _shouldCombo = true;
            }
            
            if (!(_updateTime > _animationSettings.Duration)) return;
            
            if (_shouldCombo && _animationSettings.ShouldCombo && _comboState != null)
            {
                _stateMachine.SetNextState(_comboState);
                return;
            }
                
            ToTransitionState();
        }

        protected abstract void Attack();

        protected override void OnExit()
        {
            _attacker.OnAttack -= OnAttack;
            
            _animator.ResetTrigger(AttackTriggerId);
        }

        private void OnAttack()
        {
            _attackPressedTimer = 0.15f;
        }
    }
}