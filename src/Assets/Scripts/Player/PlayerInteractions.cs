using Assets.Scripts.Environment.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        public Text InteractionText;
        public float InteractionDistance = 1;

        private void FixedUpdate()
        {
            InteractionText.text = "";

            RaycastHit hit;
            var cam = UnityEngine.Camera.main.transform;
            var forward = cam.forward;
            var position = cam.position;

            var ray = new Ray(position, forward);

            if (Physics.Raycast(ray, out hit))
            {
                var interaction = hit.collider.GetComponent<IInteractable>();
                if (interaction != null && Vector3.Distance(transform.position, hit.collider.transform.position) < InteractionDistance)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interaction.Interact();
                    }
                    else
                    {
                        InteractionText.text = "Press E to interact";
                    }
                }
            }
        }
    }
}