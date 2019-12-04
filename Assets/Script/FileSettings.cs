using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileSettings : MonoBehaviour
{
    public Text dogName;
    public Text[] stateText;
    public List<Toggle> stateToggle;
    public Image[] lines;
    List<State> stateList;
    public TextMeshProUGUI ageT, sexT;
    int p;
    List<string> nomes = new List<string>();
    public void InitializeFile(List<State> p_stateList, int s, float age)
    {
        stateList = p_stateList;
        /*for(int i = 0; i < stateToggle.Count; i++)
        {
            stateToggle[i].interactable = false;
        }*/
        Debug.Log("Files " + age);
        if(age == 1f)
        {
            ageT.text = age + " Ano";
        }
        else if(age >1f)
        {
            ageT.text = age + " Anos";
        }
        else if(age <1f)
        {
            //age = age % 1; 
            string t = age.ToString("0.#", CultureInfo.InvariantCulture);
            //Debug.Log("culture: " + t);
            string[] x = t.Split('.');
           t = x[1] + " Meses";
            ageT.text = t;
        }
        if (s == 0)
        {
            nomes.Add("Bolinha");
            nomes.Add("Spike");
            nomes.Add("Tremilico");
            nomes.Add("Bolacha");
            nomes.Add("Pitucho");
            nomes.Add("Peludo");
            nomes.Add("Corredor");
            sexT.text = "Macho";
            string n = nomes[Random.Range(0, 7)];
            dogName.text = n;
        }
        else
        {
            nomes.Add("Bolinha");
            nomes.Add("Mileni");
            nomes.Add("Bel");
            nomes.Add("Cacau");
            sexT.text = "Fêmea";
            string n = nomes[Random.Range(0, 4)];
            dogName.text = n;
        }
        for (int i = 0; i < stateList.Count; i++)
        {
            stateText[i].gameObject.SetActive(true);
            stateText[i].text = stateList[i].nome;
        }
    }

    public void SetToggled(int p_index)
    {
        /*stateToggle[p_index].isOn = true;
        stateToggle.RemoveAt(p_index);*/
    }

    public void SetLine(int p)
    {
        lines[p].enabled = true;
    }
    public void resetFile()
    {
        for (int i = 0; i < stateToggle.Count; i++)
        {
            /*stateToggle[i].interactable = false;
            stateToggle[i].isOn = false;*/
        }
    }
}
