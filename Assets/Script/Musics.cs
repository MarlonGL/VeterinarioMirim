using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Musics : MonoBehaviour
{
    public AudioClip[] musicas = new AudioClip[3];
    public AudioClip musiMenu;
    public AudioClip buttonS;
    public AudioClip sad, angry, happy;
    AudioSource audioS;
    Scene s;
    void Start()
    {
        /*musicas[0] = Resources.Load<AudioClip>("Sounds/Background-1");
        musicas[1] = Resources.Load<AudioClip>("Sounds/Background-2");
        musicas[2] = Resources.Load<AudioClip>("Sounds/Background-3");
        musiMenu = Resources.Load<AudioClip>("Sounds/Main_Menu");
        buttonS = Resources.Load<AudioClip>("Sounds/buttonSound");*/
    }
    private static Musics instance = null;
    public static Musics Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        audioS = gameObject.GetComponent<AudioSource>();
        s = SceneManager.GetActiveScene();

        audioS.loop = true;
        audioS.clip = musiMenu;
        audioS.Play();

    }
    void Update()
    {
        Scene s = SceneManager.GetActiveScene();
        if (!audioS.isPlaying && s.name != "Menu")
        {
            Backgrounds();
        }
    }
    /*private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            //audioS.loop = true;
            audioS.clip = musiMenu;
            audioS.Play();
        }
        if(level > 0)
        {
            //audioS.loop = false;
            audioS.clip = musicas[Random.Range(0, 3)];
            audioS.Play();
        }
    }*/

    public void PlayButtonSound()
    {
        audioS.PlayOneShot(Resources.Load<AudioClip>("Sounds/buttonSound"), 1);
    }

    public void Backgrounds()
    {
        audioS.clip = musicas[Random.Range(0, 3)];
        audioS.Play();
    }
    public void MainMenuSound()
    {
        audioS.clip = musiMenu;
        audioS.Play();
    }
    public void PlaySad()
    {
        audioS.PlayOneShot(sad, 1);
    }
    public void PlayAngry()
    {
        audioS.PlayOneShot(angry, 1);
    }
    public void PlayHappy()
    {
        audioS.PlayOneShot(happy, 1);
    }
}
