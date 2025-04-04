using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject PauseMenuUI;

    void Awake()
    {
        PauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (CollectibleCheck.score < 3) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
