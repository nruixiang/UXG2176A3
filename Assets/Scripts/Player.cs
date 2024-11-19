using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int hp;
    private bool hasCollide = false;
    public Image [] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    // Start is called before the first frame update
    void Start()
    {
        InitializeHealth();

    }

    // Update is called once per frame
    void Update()
    {
      foreach(Image img in hearts){
            img.sprite = emptyHeart;

        }
        for (int i = 0; i < hp; i++){
            hearts[i].sprite = fullHeart;
        }

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

        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("I am colliding with ground");
        }
    }

    public void TakeDamage()
    {
        hp--;
        Debug.Log(hp);

        if (hp <= 0)
        {
            SceneManager.LoadScene("Gameover");
            //Player 0 health dies
            //Trigger Gameover scene
        }
    }

    IEnumerator ResetCollisionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasCollide = false; // Reset flag after delay
    }
    public void InitializeHealth(){
        hp = 3;
    }
}
