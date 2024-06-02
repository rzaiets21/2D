using System;

namespace Interfaces
{
    public interface IAttacker
    {
        public event Action OnAttack;
    }
}