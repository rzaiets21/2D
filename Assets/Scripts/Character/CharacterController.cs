using System;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class CharacterController : MonoBehaviour, IMovable
    {
	    [SerializeField, Header("Settings")] private CharacterControllerSettings settings; 
        [SerializeField] private Rigidbody2D rigidbody2D;

        private float _lastGroundTime;
        
        private void OnValidate()
        {
	        settings ??= new CharacterControllerSettings();
	        settings.Init();
        }

        public float GetNormalizedSpeed()
        {
	        var horizontalVelocity = new Vector2(rigidbody2D.velocity.x, 0);
	        return horizontalVelocity.magnitude / settings._maxSpeed;
        }
        
        public void Move(Vector2 direction, float lerp)
        {
	        var targetSpeed = direction.x * settings._maxSpeed;
	        targetSpeed = Mathf.Lerp(rigidbody2D.velocity.x, targetSpeed, lerp);

	        float accelRate;
	        if (_lastGroundTime > 0)
		        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? settings._runAccelerateAmount : settings._runDeccelerateAmount;
	        else
		        accelRate = (Mathf.Abs(targetSpeed) > 0.01f)
			        ? settings._runAccelerateAmount * settings._accelerateInAir
			        : settings._runDeccelerateAmount * settings._deccelerateInAir;

	        var speedDif = targetSpeed - rigidbody2D.velocity.x;

	        var movement = speedDif * accelRate;

	        rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
        
        public void Rotate(Quaternion targetRotation)
        {
	        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, settings._rotatingSpeed * Time.deltaTime);
        }

        public void ForceStop()
        {
	        rigidbody2D.velocity = Vector2.zero;
        }
    }

    [Serializable]
    internal class CharacterControllerSettings
    {
        [SerializeField] internal float _maxSpeed;
        [SerializeField] internal float _rotatingSpeed;
        [SerializeField] internal float _accelerateInAir;
        [SerializeField] internal float _deccelerateInAir;
        [SerializeField] internal float _runAccelerate;
        [SerializeField] internal float _runDeccelerate;
        
        internal float _runAccelerateAmount;
        internal float _runDeccelerateAmount;

        internal void Init()
        {
	        _runAccelerateAmount = (50 * _runAccelerate) / _maxSpeed;
	        _runDeccelerateAmount = (50 * _runDeccelerate) / _maxSpeed;
        }
    }
}