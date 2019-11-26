using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class GameManager : MonoBehaviour
{
    public SpriteRenderer bowl;
    public DogState dogState;
    public GameObject[] botoes;
    public OnMouseOverObject onMouseOverShower;
    public OnMouseOverObject onMouseoverFood;
  
    public GameObject sprayCloud;
    public GameObject water;
    int nGotas;


    //medical
    public GameObject vacB, castB, remB, vetB;
    public GameObject medTab;
    bool mAtivos = false;

    bool showerWrong = false;
    bool playWrong = false;
    bool sprayWrong = false;

    Musics mu;
    void Start()
    {
        mu = GameObject.Find("Musicas").GetComponent<Musics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dogState.terminou)
        {
            medTab.SetActive(false);
        }
        if(onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.SHOWER && !showerWrong)
        {
            if (dogState.checkState("Sujo"))
            {
                water.SetActive(true);
                float __alpha = dogState._mudStain.color.a;
                __alpha -= Time.deltaTime;
                dogState._mudStain.color = new Color(dogState._mudStain.color.r, dogState._mudStain.color.g, dogState._mudStain.color.b, __alpha);
                if (__alpha <= 0)
                {
                    dogState.SetState("Sujo");
                    water.SetActive(false);
                    showerWrong = true;
                }
            }
            else
            {
                dogState.WrongMove();
                showerWrong = true;
            }
            
        }
        else
        {
            water.SetActive(false);
        }
        if (onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.HAND && !playWrong)
        {
            if (dogState.checkState("Triste"))
            {
                dogState.happines += Time.deltaTime;

                if (dogState.happines >= 1)
                {
                    dogState.SetState("Triste");
                    playWrong = true;
                }
            }
            else
            {
                dogState.WrongMove();
                playWrong = true;
            }
            
        }
        if (onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.SPRAY && !sprayWrong)
        {
            if (dogState.checkState("Pulga"))
            {
                sprayCloud.SetActive(true);
                float __alpha = dogState._fleaStain.color.a;
                __alpha -= Time.deltaTime;
                dogState._fleaStain.color = new Color(dogState._fleaStain.color.r, dogState._fleaStain.color.g, dogState._fleaStain.color.b, __alpha);
                if (__alpha <= 0)
                {
                    dogState.SetState("Pulga");
                    sprayCloud.SetActive(false);
                    sprayWrong = true;
                }
            }
            else
            {
                dogState.WrongMove();
                sprayWrong = true;
            }
            
        }
        else
        {
            sprayCloud.SetActive(false);
        }

        if (onMouseoverFood.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.FOOD)
        {
            if (dogState.checkState("Fome"))
            {
                BotaoComida();
            }
            else
            {
                bowl.sprite = Resources.Load<Sprite>("Sprite/bowl");
            }
        }
       
    }

    public void BotaoComida()
    {
        bowl.sprite = Resources.Load<Sprite>("Sprite/bowl");
        //dogState.SetHungry(false);
        dogState.SetState("Fome");
    }

    public void BotaoBanho()
    {
        //dogState.SetDirty(false);
        dogState.SetState("Sujo");

    }
    public void BotarBrincar()
    {
        //dogState.SetSad(false);
        dogState.SetState("Triste");

    }
    public void BotaoPulga()
    {
        //dogState.SetPulga(false);
        dogState.SetState("Pulga");
    }

    public void BotaoMedicalTab()
    {
        mu.PlayButtonSound();
        mAtivos = !mAtivos;
        vacB.SetActive(mAtivos);
        castB.SetActive(mAtivos);
        remB.SetActive(mAtivos);
        vetB.SetActive(mAtivos);
    }

    public void BotaoVacina()
    {
        mu.PlayButtonSound();
        if (dogState.checkMedicalState("Vacinação"))
        {
            dogState.SetMedicalState("Vacinação");
        }
        else
        {
            vacB.SetActive(false);
        }
        

    }

    public void BotaoVeterinario()
    {
        mu.PlayButtonSound();
        if (dogState.checkMedicalState("Veterinário"))
        {
            dogState.SetMedicalState("Veterinário");
        }
        else
        {
            vetB.SetActive(false);
        }
    }

    public void BotaoCastracao()
    {
        mu.PlayButtonSound();
        if (dogState.checkMedicalState("Castração"))
        {
            dogState.SetMedicalState("Castração");
        }
        else
        {
            castB.SetActive(false);
        }
    }

    public void BotaoRemedios()
    {
        mu.PlayButtonSound();
        if (dogState.checkMedicalState("Remédios"))
        {
            dogState.SetMedicalState("Remédios");
        }
        else
        {
            remB.SetActive(false);
        }
    }

    public void BotaoCorrente()
    {
        Debug.Log("botao corrente");
        dogState.SetState("Acorrentado");
    }
    public void BotaoContinuar()
    {
        mu.PlayButtonSound();
        Invoke("loadMap", 0.3f);
    }
    void loadMap()
    {
        SceneManager.LoadScene("Map");
    }
    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
