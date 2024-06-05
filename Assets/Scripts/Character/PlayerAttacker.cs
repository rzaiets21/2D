using System;
using Equipment;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class PlayerAttacker : MonoBehaviour, IAttacker
    {
        private CharacterInput _characterInput;
        private PlayerEquipment _playerEquipment;
        
        public event Action OnAttack;

        public void Init(CharacterInput characterInput, PlayerEquipment playerEquipment)
        {
            _characterInput = characterInput;
            _playerEquipment = playerEquipment;


            InitEvents();
        }

        private void InitEvents()
        {
            _characterInput.onClickAttack += OnClickAttack;
        }

        private void OnDestroy()
        {
            if(_characterInput == null)
                return;
            
            _characterInput.onClickAttack -= OnClickAttack;
        }
        
        private void OnClickAttack()
        {
            _playerEquipment.Equip();
            
            OnAttack?.Invoke();
        }
    }
}