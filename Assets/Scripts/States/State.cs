using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public abstract class State
    {
        protected float _updateTime;
        protected float _fixedUpdateTime;
        protected float _lateUpdateTime;

        protected StateMachine _stateMachine;

        protected Dictionary<StateEvent, StateTransition> _transitions;

        public State AddTransition(StateEvent stateEvent, State state)
        {
            _transitions ??= new Dictionary<StateEvent, StateTransition>();
            
            var stateTransition = new StateTransition()
            {
                EventType = stateEvent,
                State = state
            };

            if (!_transitions.TryAdd(stateTransition.EventType, stateTransition))
            {
                _transitions[stateTransition.EventType] = stateTransition;
            }

            return this;
        }

        public bool TryEnter(StateMachine stateMachine)
        {
            if (_transitions != null && _transitions.TryGetValue(StateEvent.Enter, out var transition))
            {
                return stateMachine.CurrentState.GetType() == transition.State.GetType();
            }
            return true;
        }
        
        public void Enter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            ResetState();
            Debug.Log($"Enter => {this}");
            OnEnter();
        }
        
        protected virtual void OnEnter() { }

        private void ResetState()
        {
            _updateTime = 0;
            _fixedUpdateTime = 0;
            _lateUpdateTime = 0;
            
            OnStateReset();
        }
        
        protected virtual void OnStateReset() { }
        
        public void Update()
        {
            _updateTime += Time.deltaTime;
            OnUpdate();
        }
        
        public void FixedUpdate()
        {
            _fixedUpdateTime += Time.fixedDeltaTime;
            OnFixedUpdate();
        }
        
        public void LateUpdate()
        {
            _lateUpdateTime += Time.deltaTime;
            OnLateUpdate();
        }
        
        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }
        protected virtual void OnLateUpdate() { }
        
        public void Exit()
        {
            Debug.Log($"Exit => {this}");
            OnExit();
        }
        
        protected virtual void OnExit() { }

        protected void ToTransitionState()
        {
            if (_transitions != null && _transitions.TryGetValue(StateEvent.Exit, out var transition))
            {
                _stateMachine.SetNextState(transition.State);
                return;
            }
            
            _stateMachine.SetDefaultState();
        }
        
        protected T GetComponent<T>() where T : Component => _stateMachine.GetComponent<T>();
    }
}