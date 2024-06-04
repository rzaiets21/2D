using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateMachine : MonoBehaviour
    {
        protected State _defaultState;
        protected State _nextState;

        public State CurrentState;
        
        public void InitDefaultState(State state)
        {
            _defaultState = state;
        }
        
        public void SetDefaultState()
        {
            SetNextState(_defaultState);
        }
        
        private void Update()
        {
            if (_nextState != null)
            {
                SetState(_nextState);
            }

            CurrentState?.Update();
        }

        private void SetState(State newState, bool checkTransition = true)
        {
            _nextState = null;
            
            CurrentState?.Exit();
            
            if(checkTransition && !newState.TryEnter(this))
                return;
            
            CurrentState = newState;
            CurrentState.Enter(this);
        }

        public void SetNextState(State newState)
        {
            if (newState == null)
                return;
            
            _nextState = newState;
        }

        private void LateUpdate()
        {
            CurrentState?.LateUpdate();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}