using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hungry;
    public GameObject angry;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpriteHungry()
    {
        hungry.SetActive(true);
    }
    public void setSpriteAngry()
    {
        angry.SetActive(true);
    }

    public void NoSprite()
    {
        hungry.SetActive(false);
        angry.SetActive(false);
    }
}
