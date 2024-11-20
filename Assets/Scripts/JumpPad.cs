using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float launchForce;

    private void OnTriggerEnter(Collider other)
    {
        // First try to get CharacterController from the colliding object
        CharacterController controller = other.GetComponentInParent<CharacterController>();
        if(controller != null)
        {
            FPSController playerController = controller.GetComponent<FPSController>();
            if(playerController != null)
            {
                playerController.LaunchPlayer(launchForce);
            }
        }
    }
}
