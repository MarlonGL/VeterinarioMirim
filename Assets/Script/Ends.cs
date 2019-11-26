using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ends : MonoBehaviour
{
    public TextMeshProUGUI texto;
    int stars = 0;
    void Start()
    {
        for (int i = 0; i < Global.faseStar.Length; i++)
        {
            stars += Global.faseStar[i];
        }
        texto.text = "Você conseguiu " + stars;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
