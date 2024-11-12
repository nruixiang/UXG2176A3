using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{
    [SerializeField] GameObject player;
    public LayerMask layerMask;
    private bool inLineOfSight;
    [SerializeField] float range;
    // Start is called before the first frame update
    void Start()
    {
        inLineOfSight = false;
        range = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        LineOfSightCheck();
    }
    public void LineOfSightCheck(){
        Debug.Log("Script Running");
        RaycastHit hit;
        Vector3 playerDirection = player.transform.position - transform.position;

        if(Physics.Raycast(transform.position, playerDirection, out hit, range)){
            Debug.Log("If Check");
            if (hit.collider.gameObject.tag == "Player")
            {
                inLineOfSight = true;
                Debug.Log("I SEE YOU");
            }
            else
            {
                inLineOfSight = false;
                Debug.Log("I DONT SEE YOU");
            }
        }
        Debug.DrawRay(transform.position, playerDirection.normalized * range, Color.red);
        
    }
}
