using System;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class PlayerAttacker : MonoBehaviour, IAttacker
    {
        public event Action OnAttack;
    }
}