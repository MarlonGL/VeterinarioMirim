using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer bowl;
    public DogState dogState;
    public GameObject[] botoes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
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
