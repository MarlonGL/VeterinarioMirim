using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer bowl;
    public DogState dogState;
    public GameObject[] botoes;
    public OnMouseOverObject onMouseOverShower;
    public OnMouseOverObject onMouseOverSpray;
    public GameObject sprayCloud;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.SHOWER && dogState.GetState("Sujo"))
        {
            float __alpha = dogState._mudStain.color.a;
            __alpha -= Time.deltaTime;
            dogState._mudStain.color = new Color(dogState._mudStain.color.r, dogState._mudStain.color.g, dogState._mudStain.color.b, __alpha);
            if(__alpha <= 0)
            {
                dogState.SetState("Sujo");
            }
        }
        if (onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.HAND && dogState.GetState("Triste"))
        {
            dogState.happines += Time.deltaTime;
            
            if (dogState.happines >= 1)
            {
                dogState.SetState("Triste");
            }
        }
        if (onMouseOverShower.onMouseOver && DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.GetComponent<DragHandler>().type == ItemType.SPRAY && dogState.GetState("Pulga"))
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
}
