using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{
    //Hitscan "Weapon" for the player using Raycast
    void Update()
    {
        LineOfSightCheck();
    }
    public void LineOfSightCheck(){
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    StartCoroutine(enemy.DamageFeedback());
                    enemy.enemyHealth -= 1f;
                    
                }
            }
        }
        
    }
}
