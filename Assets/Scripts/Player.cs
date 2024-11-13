using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public int hp;
    private bool hasCollide = false;


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
      

    }


    void OnCollisionEnter(Collision collision)
    {
        if (hasCollide) return;  //Do nothing if

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hasCollide = true;
            TakeDamage();
            StartCoroutine(ResetCollisionAfterDelay(2));
        }
    }

    public void TakeDamage()
    {
        hp--;
        Debug.Log(hp);

        if (hp == 0)
        {
            //Player 0 health dies
            //Trigger Gameover scene
        }
    }

    IEnumerator ResetCollisionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasCollide = false; // Reset flag after delay
    }

}
