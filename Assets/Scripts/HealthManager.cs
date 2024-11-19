using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;
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
        for (int i = 0; i < health; i++){
            hearts[i].sprite = fullHeart;
        }
    }
    public void InitializeHealth(){
        health = 3;
    }
}
