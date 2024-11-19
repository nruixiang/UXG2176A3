using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public FPSController player;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "game")
        {
            pauseMenu = GameObject.Find("Pause");
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "game")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
            {
                Debug.Log("Pause");
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; //unlock the cursor
                SoundManager.instance.StopLoopSound();

                player.enabled = false;
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
            {
                Debug.Log("Unpause");

                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                player.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked; //Lock the cursor
            }

        }
       
    }
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Gameover()
    {
        SceneManager.LoadScene("Gameover");
    }
    public void LevelComplete()
    {
        SceneManager.LoadScene("LevelComplete");
    }

    public void BacktoStart()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void QuitGame(){
        Application.Quit();
    }
}
