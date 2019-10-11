using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer bowl;
    public DogState dogState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            bowl.sprite = Resources.Load<Sprite>("Sprite/bowl");
            dogState.SetHungry(false);
        }
    }
}
