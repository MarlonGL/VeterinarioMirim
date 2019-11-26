using System.Collections;
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
        nomes.Add("Bolinha");
        nomes.Add("Spike");
        nomes.Add("Tremilico");
        nomes.Add("Bolacha");
        nomes.Add("Pitucho");
        nomes.Add("Peludo");
        nomes.Add("Mileni");
        nomes.Add("Cacau");
        nomes.Add("Corredor");
        nomes.Add("Bel");
        string n = nomes[Random.Range(0, 10)];
        dogName.text = n;
        Debug.Log("Files " + age);
        if(age == 1)
        {
            ageT.text = age + " Ano";
        }
        else if(age > 1)
        {
            ageT.text = age + " Anos";
        }
        else
        {
            //age = age % 1;
            string t = System.String.Format("{0:.0}", age) + " Meses";
            ageT.text = t;
        }
        if (s == 0)
        {
            sexT.text = "Macho";
        }
        else
        {
            sexT.text = "Fêmea";
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
