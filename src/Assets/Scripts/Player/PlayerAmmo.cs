using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAmmo : MonoBehaviour
    {
        private Dictionary<AmmoType, int> _ammo = new Dictionary<AmmoType, int>();

        void Start()
        {
            _ammo = new Dictionary<AmmoType, int>
            {
                { AmmoType.Infinite, 666 },
                { AmmoType.Solution, 100 }
            };
        }

        public int GetAmmo(AmmoType ammoType)
        {
            return _ammo.ContainsKey(ammoType) ? _ammo[ammoType] : 0;
        }

        public bool HasAmmo(AmmoType ammoType, int amount)
        {
            return ammoType == AmmoType.Infinite || GetAmmo(ammoType) > amount;
        }

        public void AddAmmo(AmmoType ammoType, int amount)
        {
            if (_ammo.ContainsKey(ammoType))
            {
                _ammo[ammoType] += amount;
            }
            else
            {
                _ammo.Add(ammoType, amount);
            }
        }

        public void RemoveAmmo(AmmoType ammoType, int amount)
        {
            if (_ammo.ContainsKey(ammoType))
            {
                _ammo[ammoType] -= amount;
            }
        }
    }
}