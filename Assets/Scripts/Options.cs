using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    bool muted = false;
    // Start is called before the first frame update
    void Start()
    {
        if(AudioListener.volume > 0)
        {
            muted = false;
        }
        else
        {
            muted = true;
        }
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Mute()
    {
        muted = !muted;
        if (muted == true)
        {
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = 1f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
