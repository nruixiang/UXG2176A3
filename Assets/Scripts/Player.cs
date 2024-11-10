using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    //Does not work with First Person Camera
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = controller.isGrounded;
        if(isGrounded && playerVelocity.y < 0){
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        // if(move != Vector3.zero){
        //     gameObject.transform.forward = move;
        // }
    }

}
