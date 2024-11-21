using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public FPSController player;
    [SerializeField] Transform bar;
    public static float progress;
    public float progressReq;


    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "game")
        {
            pauseMenu = GameObject.Find("Pause");
            pauseMenu.SetActive(false);
            SoundManager.instance.PlayMusic();
        }
        if(SceneManager.GetActiveScene().name == "Gameover" || SceneManager.GetActiveScene().name == "LevelComplete"){
            Cursor.visible = true;
            SoundManager.instance.StopLoopSound();
            Cursor.lockState = CursorLockMode.None;
        }
        progress = 0;
        progressReq = 5;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SoundManager.instance.StopMusic();

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pause Menu
        if (SceneManager.GetActiveScene().name == "game")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; //unlock the cursor
                SoundManager.instance.StopLoopSound();

                player.enabled = false;
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                player.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked; //Lock the cursor
            }

        }
        SetProgressBarState(progress, progressReq);
        if(progress >= progressReq){
            SceneManager.LoadScene("LevelComplete");
        }

   

    }
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Gameover()
    {
        SceneManager.LoadScene("Gameover");
        SoundManager.instance.StopLoopSound();

    }
    public void LevelComplete()
    {
        SceneManager.LoadScene("LevelComplete");
        SoundManager.instance.StopLoopSound();
    }

    public void BacktoStart()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void QuitGame(){
        Application.Quit();
    }
    public void SetProgressBarState(float currentProg, float maxProg){
        if(bar != null){
            float state = (float)currentProg;
            state /= maxProg;
            if(state < 0){
                state = 0f;
            }
            bar.transform.localScale = new Vector3(state, bar.localScale.y, 1f);
        } else {
            return;
        }
        
    }
}
