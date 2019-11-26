using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Musics mu;
    public AudioClip buttonS;
    AudioSource audioS;
    void Start()
    {
        mu = GameObject.Find("Musicas").GetComponent<Musics>();
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotaoJogar()
    {
        //audioS.PlayOneShot(buttonS, 1);
        mu.PlayButtonSound();
        mu.Backgrounds();
        Invoke("loadMap", 0.3f);
    }

    void loadMap()
    {
        SceneManager.LoadScene("Map");
    }
}
