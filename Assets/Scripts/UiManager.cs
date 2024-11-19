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

    public void BacktoStart()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void QuitGame(){
        Application.Quit();
    }
    public void SetProgressBarState(float currentProg, float maxProg){
        float state = (float)currentProg;
        state /= maxProg;
        if(state < 0){
            state = 0f;
        }
        bar.transform.localScale = new Vector3(state, bar.localScale.y, 1f);
    }
}
