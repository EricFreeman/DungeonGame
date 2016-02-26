using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAmmo : MonoBehaviour
    {
        public GameObject Hand;
        public GameObject CurrentWeapon;
        public List<GameObject> WeaponList;
        public List<bool> CollectedWeapons;

        private Dictionary<AmmoType, int> _ammo = new Dictionary<AmmoType, int>();

        void Start()
        {
            _ammo = new Dictionary<AmmoType, int>
            {
                { AmmoType.Infinite, 666 }
            };

            if (CurrentWeapon == null)
            {
                CurrentWeapon = Instantiate(WeaponList.First());
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && HasWeapon(0))
            {
                Destroy(CurrentWeapon);
                CurrentWeapon = Instantiate(WeaponList[0]);
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && HasWeapon(1))
            {
                Destroy(CurrentWeapon);
                CurrentWeapon = Instantiate(WeaponList[1]);
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && HasWeapon(2))
            {
                Destroy(CurrentWeapon);
                CurrentWeapon = Instantiate(WeaponList[2]);
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && HasWeapon(3))
            {
                Destroy(CurrentWeapon);
                CurrentWeapon = Instantiate(WeaponList[3]);
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && HasWeapon(4))
            {
                Destroy(CurrentWeapon);
                CurrentWeapon = Instantiate(WeaponList[4]);
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
        }

        public void CollectWeapon(int weaponId)
        {
            // TODO: I know this is shit, but it's 2:30 am so what do you expect?
            CollectedWeapons[weaponId] = true;

            var highestId = CollectedWeapons.Select((s, i) => new {i, s})
                .Where(t => t.s)
                .Select(t => t.i)
                .Max();

            if (weaponId >= highestId)
            {
                Destroy(CurrentWeapon);
                CurrentWeapon = Instantiate(WeaponList[weaponId]);
                CurrentWeapon.transform.SetParent(Hand.transform, false);
            }
        }

        public bool HasWeapon(int weaponId)
        {
            return CollectedWeapons.Count > weaponId && CollectedWeapons[weaponId];
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