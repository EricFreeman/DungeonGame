using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGun : MonoBehaviour
    {
        private BaseGun _gun;

        void Start()
        {
            _gun = GetComponent<BaseGun>();
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _gun.Fire();
            }
        }
    }
}
