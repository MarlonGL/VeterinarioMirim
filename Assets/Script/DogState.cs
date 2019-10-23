using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState : MonoBehaviour
{
    bool _hungry, _sad, _dirty, _pulga;
    
    public Animator _myAnimator;
    
    public GameObject _myUrgentBaloon;

    public SpriteRenderer _mudStain;

    public SpriteRenderer _fleaStain;
    
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

        if (checkState("Dirty"))
        {
            _mudStain.enabled = true;
        }
        if (checkState("Pulga"))
        {
            _fleaStain.enabled = true;
        }
        if (checkState("Sad"))
        {
            PlayAnimation("Sad");
            _myAnimator.SetBool("IdleSad", true);
        }
        if (checkState("Hungry"))
        {
            //play hmmmmm sound - som de fome
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
                    PlayAnimation("Angry");
                    break;
                case "Sad":
                    showSad();
                    PlayAnimation("Angry");
                    break;
                case "Dirty":
                    showDirty();
                    PlayAnimation("Angry");
                    break;
                case "Pulga":
                    showPulga();
                    PlayAnimation("Angry");
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

    public void SetState(string p_state)
    {
        if (!checkState(p_state))
        {
            erros++;
        }
        else if (checkState(p_state))
        {
            PlayAnimation("Happy");
            removeState(p_state);
            if (p_state == "Dirty")
            {
                _mudStain.enabled = false;
            }
            else if(p_state == "Pulga")
            {
                _fleaStain.enabled = false;
            }
            else if(p_state == "Sad")
            {
                _myAnimator.SetBool("IdleSad", false);
            }
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
