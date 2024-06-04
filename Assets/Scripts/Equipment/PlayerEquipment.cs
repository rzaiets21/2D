using UnityEngine;

namespace Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        [SerializeField] private GameObject unequippedTwoHanded;
        [SerializeField] private GameObject equippedTwoHanded;
        
        public void Equip()
        {
            unequippedTwoHanded.SetActive(false);
            equippedTwoHanded.SetActive(true);
        }
        
        public void UnEquip()
        {
            unequippedTwoHanded.SetActive(true);
            equippedTwoHanded.SetActive(false);
        }
    }
}