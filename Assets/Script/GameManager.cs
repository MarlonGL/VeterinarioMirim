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
    bool mAtivos = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.SHOWER && dogState.checkState("Sujo"))
        {
            water.SetActive(true);
            float __alpha = dogState._mudStain.color.a;
            __alpha -= Time.deltaTime;
            dogState._mudStain.color = new Color(dogState._mudStain.color.r, dogState._mudStain.color.g, dogState._mudStain.color.b, __alpha);
            if(__alpha <= 0)
            {
                dogState.SetState("Sujo");
                water.SetActive(false);
            }
        }
        else
        {
            water.SetActive(false);
        }
        if (onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.HAND && dogState.checkState("Triste"))
        {
            dogState.happines += Time.deltaTime;
            
            if (dogState.happines >= 1)
            {
                dogState.SetState("Triste");
            }
        }
        if (onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.SPRAY && dogState.checkState("Pulga"))
        {
            sprayCloud.SetActive(true);
            float __alpha = dogState._fleaStain.color.a;
            __alpha -= Time.deltaTime;
            dogState._fleaStain.color = new Color(dogState._fleaStain.color.r, dogState._fleaStain.color.g, dogState._fleaStain.color.b, __alpha);
            if (__alpha <= 0)
            {
                dogState.SetState("Pulga");
                sprayCloud.SetActive(false);
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
        mAtivos = !mAtivos;
        vacB.SetActive(mAtivos);
        castB.SetActive(mAtivos);
        remB.SetActive(mAtivos);
        vetB.SetActive(mAtivos);
    }

    public void BotaoVacina()
    {
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
        SceneManager.LoadScene("Map");
    }
    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
