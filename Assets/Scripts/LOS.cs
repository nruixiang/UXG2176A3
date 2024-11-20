using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{
    [SerializeField] GameObject player;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //inLineOfSight = false;
        //range = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        LineOfSightCheck();
    }
    public void LineOfSightCheck(){
        RaycastHit hit;
        //Vector3 playerDirection = player.transform.position - transform.position;

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    //inLineOfSight = true;
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageFeedback());
                    enemy.enemyHealth -= 1f;
                    
                } 
            
            }
        }
        
        
    }
}
