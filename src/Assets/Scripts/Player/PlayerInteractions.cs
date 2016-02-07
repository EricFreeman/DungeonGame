using Assets.Scripts.Environment.Interactable;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        private void FixedUpdate()
        {
            RaycastHit hit;
            var cam = UnityEngine.Camera.main.transform;
            var forward = cam.forward;
            var position = cam.position;

            var ray = new Ray(position, forward);

            if (Physics.Raycast(ray, out hit))
            {
                var interaction = hit.collider.GetComponent<IInteractable>();
                if (interaction != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interaction.Interact();
                    }
                    else
                    {
                        // TODO: Display to use that something is interactable here or something
                    }
                }
            }
        }
    }
}