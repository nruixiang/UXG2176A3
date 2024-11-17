using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float launchForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision col){
        // if(col.gameObject.CompareTag("Player")){
        //     Debug.Log("Jumping");
        //     Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
        //     rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        // } else{
        //     Debug.Log("Nope");
        // }
        Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
        if(rb != null){
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        } else{
            Debug.Log("No RigidBody");
        }
    }
}
