using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public Button[] fases;
    public GameObject star;
    int faseAtual;
    Vector3 s1 = new Vector3(-55, 73, 0);
    Vector3 s2 = new Vector3(10, 93, 0);
    Vector3 s3 = new Vector3(75, 73, 0);
    Musics mu;
    void Start()
    {
        mu = GameObject.Find("Musicas").GetComponent<Musics>();
        faseAtual = Global.faseAtual;
        for (int i = 0; i < fases.Length; i++)
        {
            if(i != faseAtual)
            {
                fases[i].interactable = false;
            }
        }
        for (int i = 0; i < Global.faseStar.Length; i++)
        {
            if(Global.faseStar[i] == 1)
            {
                Instantiate(star, fases[i].transform.position + s1, new Quaternion(0,0,0,0), fases[i].transform);
            }
            if (Global.faseStar[i] == 2)
            {
                Instantiate(star, fases[i].transform.position + s1, new Quaternion(0, 0, 0, 0), fases[i].transform);
                Instantiate(star, fases[i].transform.position + s2, new Quaternion(0, 0, 0, 0), fases[i].transform);
            }
            if (Global.faseStar[i] == 3)
            {
                Instantiate(star, fases[i].transform.position + s1, new Quaternion(0, 0, 0, 0), fases[i].transform);
                Instantiate(star, fases[i].transform.position + s2, new Quaternion(0, 0, 0, 0), fases[i].transform);
                Instantiate(star, fases[i].transform.position + s3, new Quaternion(0, 0, 0, 0), fases[i].transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotaoFase()
    {
        mu.PlayButtonSound();
        SceneManager.LoadScene("DogScene");
    }

    public void BotarVoltar()
    {
        mu.PlayButtonSound();
        mu.MainMenuSound();
        SceneManager.LoadScene("Menu");
    }
}
