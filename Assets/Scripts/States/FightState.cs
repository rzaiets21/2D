namespace States
{
    public class FightState : State
    {
        private float _exitTime = 3.5f;

        private CharacterAnimator _characterAnimator;

        public void Init(CharacterAnimator characterAnimator)
        {
            _characterAnimator = characterAnimator;
        }
        
        protected override void OnEnter()
        {
            _exitTime = 0;
            
            _characterAnimator.UpdateState(CharacterAnimatorParams.IsFighting, true);
        }

        protected override void OnUpdate()
        {
            if(_updateTime < _exitTime)
                return;
            
            _characterAnimator.UpdateState(CharacterAnimatorParams.IsFighting, false);
        }
    }
}