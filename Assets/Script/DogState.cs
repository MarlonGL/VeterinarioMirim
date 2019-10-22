using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState : MonoBehaviour
{
    bool _hungry, _sad, _dirty, _pulga;
    
    public Animator _myAnimator;
    
    public GameObject _myUrgentBaloon;
    
    //GameObject _myBaloon;
    bool wrongMove = false;
    int erros = 0;
    public GameObject baloonSprite;

    List<State> _badStates = new List<State>();
    List<State> _dogStates = new List<State>();

    int _happiness = 0; //progressão do player

    State sad = new State(2, "Sad");
    State hungry = new State(3, "Hungry");
    State dirty = new State(1, "Dirty");
    State pulga = new State(1, "Pulga");
    // Start is called before the first frame update
    void Start()
    {
        _hungry = true;
        _sad = false;
        _dirty = false;
        _pulga = true;
        
        _badStates.Add(hungry);
        _badStates.Add(sad);
        _badStates.Add(dirty);
        _badStates.Add(pulga);

        int numBadStates = 2;
        int r;
        int pesito;
        for (int i = 0; i < numBadStates; i++)
        {
            r = Random.Range(0, _badStates.Count);
            pesito = Random.Range(1, 4);
            _badStates[r].peso = pesito;
            _dogStates.Add(_badStates[r]);
            _badStates.Remove(_badStates[r]);
        }
        for (int i = 0; i < _dogStates.Count; i++)
        {
            Debug.Log(_dogStates[i].nome + " "+ _dogStates[i].peso);
        }
    }
    void Update()
    {
        if(erros == 3)
        {
            State temp = new State(0,"temp");
            for (int i = 0; i < _dogStates.Count; i++)
            {
                if(_dogStates[i].peso > temp.peso)
                {
                    temp = _dogStates[i];
                }
            }
            switch (temp.nome)
            {
                case "Hungry":
                    showHungry();
                    break;
                case "Sad":
                    showSad();
                    break;
                case "Dirty":
                    showDirty();
                    break;
                case "Pulga":
                    showPulga();
                    break;
                default:
                    break;
            }
            erros = 0;
        }
        /*if(_hungry)
        {
            //_myUrgentBaloon.GetComponent<Baloon>().NoSprite();
            //_myUrgentBaloon.SetActive(false);
            BaloonOn();
            _myUrgentBaloon.GetComponent<Baloon>().setSpriteHungry();
            //_myAnimator.Play("Happy");
            //_myAnimator.SetTrigger("happyAnim");

        }*/
        
        /*if(wrongMove)
        {
            _myAnimator.Play("Angry");
            //_myUrgentBaloon.SetActive(true);
            BaloonOn();
            _myUrgentBaloon.GetComponent<Baloon>().setSpriteAngry();
            Invoke("ResertWrongMove", 3);
        }*/
    }
   
    void showHungry()
    {
        BaloonOn();
        _myUrgentBaloon.GetComponent<Baloon>().setSpriteHungry();
        Invoke("ResertWrongMove", 2f);
    }

    void showDirty()
    {
        Debug.Log("estoy sujo");
        BaloonOn();
        Invoke("ResertWrongMove", 2f);
    }

    void showSad()
    {
        Debug.Log("estoy trsti");
        BaloonOn();
        Invoke("ResertWrongMove", 2f);
    }

    void showPulga()
    {
        Debug.Log("estoy pulguento");
        BaloonOn();
        Invoke("ResertWrongMove", 2f);
    }
    void WrongMove()
    {
        PlayAnimation("Angry");
        BaloonOn();
        _myUrgentBaloon.GetComponent<Baloon>().setSpriteAngry();
        Invoke("ResertWrongMove", 1.2f);
    }
    bool checkState(string _state)
    {
        for (int i = 0; i < _dogStates.Count; i++)
        {
            if(_dogStates[i].nome == _state)
            {
                return true;
            }
        }
        return false;
    }

    void removeState(string _state)
    {
        for (int i = 0; i < _dogStates.Count; i++)
        {
            if (_dogStates[i].nome == _state)
            {
                _dogStates.RemoveAt(i);
            }
        }
    }
    public void SetHungry(bool p_hugry)
    {
        if (!checkState("Hungry"))
        {
            erros++;
        }
        else if (checkState("Hungry"))
        {
            PlayAnimation("Happy");
            removeState("Hungry");
        }
    }

    public void SetSad(bool p_sad)
    {
        if (!checkState("Sad"))
        {
            erros++;
        }
        else if (checkState("Sad"))
        {
            PlayAnimation("Happy");
            removeState("Sad");
        }
    }

    public void SetDirty(bool p_dirty)
    {
        if (!checkState("Dirty"))
        {
            erros++;
        }
        else if (checkState("Dirty"))
        {
            PlayAnimation("Happy");
            removeState("Dirty");
        }

        /*if (p_dirty == _dirty)
        {
            wrongMove = true;
            WrongMove();
        }
        else
        {
            ResetBaloon();
        }*/
    }

    public void SetPulga(bool p_pulga)
    {
        if (!checkState("Pulga"))
        {
            erros++;
        }
        else if (checkState("Pulga"))
        {
            PlayAnimation("Happy");
            removeState("Pulga");
        }
    }

    void ResetBaloon()
    {
        _myUrgentBaloon.GetComponent<Baloon>().NoSprite();
        _myUrgentBaloon.GetComponent<SpriteRenderer>().enabled = false;
    }

    void BaloonOn()
    {
        _myUrgentBaloon.GetComponent<SpriteRenderer>().enabled = true;
    }

    void ResertWrongMove()
    {
        wrongMove = false;
        //erros = 0;
        ResetBaloon();
    }

    void PlayAnimation(string name)
    {
        _myAnimator.Play(name);
    }
}
