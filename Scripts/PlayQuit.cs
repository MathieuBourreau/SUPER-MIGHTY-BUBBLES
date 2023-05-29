using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayQuit : MonoBehaviour
{
    public GameObject ButtonPlayAgain;
    public VideoPlayer GameOver;

    // Start is called before the first frame update
    void Start()
    {
        ButtonPlayAgain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver.isPrepared)
        {
            ButtonPlayAgain.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
