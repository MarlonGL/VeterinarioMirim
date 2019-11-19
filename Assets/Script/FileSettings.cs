using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileSettings : MonoBehaviour
{
    public Text dogName;
    public Text[] stateText;
    public List<Toggle> stateToggle;
    public Image[] lines;
    List<State> stateList;
    int p;
    public void InitializeFile(List<State> p_stateList)
    {
        stateList = p_stateList;
        /*for(int i = 0; i < stateToggle.Count; i++)
        {
            stateToggle[i].interactable = false;
        }*/
        dogName.text = "Doggo";
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
