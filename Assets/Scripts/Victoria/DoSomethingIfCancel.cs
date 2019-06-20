using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DoSomethingIfCancel : MonoBehaviour
{
    public AudioClip sound;
    public string method;

    public void Update()
    {
        if (Input.GetButtonDown(EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().cancelButton))
        {
            StartClickSound();
            if (method.Equals("EndPause"))
            {
                GameObject.Find("ArturSceneManager").GetComponent<GameUI>().EndPause();
            }
            else
            {
                ChangeMenuScene(method);
            }
        }
    }
    private void StartClickSound()
    {
        gameObject.AddComponent<AudioSource>();
        AudioSource source = GetComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        source.PlayOneShot(sound);
    }

    private void ChangeMenuScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
