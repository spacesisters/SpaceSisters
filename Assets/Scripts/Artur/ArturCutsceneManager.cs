using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ArturCutsceneManager : MonoBehaviour
{

    public VideoPlayer vPlayer;
    public string sceneName;

    private GameObject backgroundMusic;

    private void Awake()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("backgroundmusic");
        if(backgroundMusic != null)
        {
            backgroundMusic.SetActive(false);
        }
    }

    void Start()
    { 
        vPlayer.loopPointReached += LoadScene;
    }

    private void Update()
    {
        if(Input.anyKey)
        {
            LoadScene(vPlayer);
        }
    }

    private void LoadScene(VideoPlayer vp)
    {
        backgroundMusic.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

}
