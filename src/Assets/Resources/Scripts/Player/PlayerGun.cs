using Assets.Resources.Scripts.Weapons;
using UnityEngine;

namespace Assets.Resources.Scripts.Player
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
            if (Input.GetMouseButtonDown(0))
            {
                _gun.Fire();
            }
        }
    }
}
