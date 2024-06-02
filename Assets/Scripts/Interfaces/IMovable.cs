using UnityEngine;

namespace Interfaces
{
    public interface IMovable
    {
        public float GetNormalizedSpeed();
        
        public void Move(Vector2 vector, float lerp);
        public void Rotate(Quaternion quaternion);
    }
}