using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer hungry;
    public SpriteRenderer angry;

    void Start()
    {
        hungry.enabled = false;
        angry.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpriteHungry()
    {
        hungry.enabled = true;
    }
    public void setSpriteAngry()
    {
        angry.enabled = true;
    }

    public void NoSprite()
    {
        hungry.enabled = false;
        angry.enabled = false;
    }
}
