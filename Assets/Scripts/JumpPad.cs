using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float launchForce;

    private void OnTriggerEnter(Collider other)
    {
        //Checks object for CharacterController component to launch it
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
