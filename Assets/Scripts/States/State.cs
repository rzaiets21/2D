using UnityEngine;

namespace States
{
    public abstract class State
    {
        protected float _updateTime;
        protected float _fixedUpdateTime;
        protected float _lateUpdateTime;

        protected StateMachine _stateMachine;

        public void Enter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            Debug.LogError($"Enter => {this}");
            OnEnter();
        }
        
        protected virtual void OnEnter() { }
        
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
            OnExit();
        }
        
        protected virtual void OnExit() { }

        protected T GetComponent<T>() where T : Component => _stateMachine.GetComponent<T>();
    }
}