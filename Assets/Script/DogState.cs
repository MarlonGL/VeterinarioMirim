using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState : MonoBehaviour
{
    bool _hungry, _sad, _dirty, _pulga;
    [SerializeField]
    Animator _myAnimator;
    [SerializeField]
    GameObject _myUrgentBaloon;
    [SerializeField]
    //GameObject _myBaloon;
    bool wrongMove = false;

    public GameObject baloonSprite;

    List<State> badStates = new List<State>();

    int _happiness = 0; //progressão do player

    // Start is called before the first frame update
    void Start()
    {
        _hungry = true;
        _sad = false;
        _dirty = false;
        _pulga = true;
        
        State sad = new State(2);
        State hungry = new State(3);

        badStates.Add(hungry);
        badStates.Add(sad);
        int highest;
        for (int i = 0; i < badStates.Count; i++)
        {

        }
        



        /*states[0] = _hungry;
        states[1] = _sad;
        states[2] = _dirty;
        states[3] = _pulga;*/
        //aleatoriedade
    }

    //animacoes
    //happyAnim - happy
    void Update()
    {
        
        if(_hungry)
        {
            //_myUrgentBaloon.GetComponent<Baloon>().NoSprite();
            //_myUrgentBaloon.SetActive(false);
            BaloonOn();
            _myUrgentBaloon.GetComponent<Baloon>().setSpriteHungry();
            //_myAnimator.Play("Happy");
            //_myAnimator.SetTrigger("happyAnim");

        }
        
        /*if(wrongMove)
        {
            _myAnimator.Play("Angry");
            //_myUrgentBaloon.SetActive(true);
            BaloonOn();
            _myUrgentBaloon.GetComponent<Baloon>().setSpriteAngry();
            Invoke("ResertWrongMove", 3);
        }*/
    }

    void WrongMove()
    {
        PlayAnimation("Angry");
        BaloonOn();
        _myUrgentBaloon.GetComponent<Baloon>().setSpriteAngry();
        Invoke("ResertWrongMove", 1.2f);
    }

    public void SetHungry(bool p_hugry)
    {
        if (_hungry)
        {
            ResetBaloon();
        }
        _hungry = p_hugry;
        if (!_hungry)
        {
            PlayAnimation("Happy");
        }
    }

    public void SetSad(bool p_sad)
    {
        _sad = p_sad;
        if (!_sad)
        {
            PlayAnimation("Happy");
        }
    }

    public void SetDirty(bool p_dirty)
    {
        if (p_dirty == _dirty)
        {
            wrongMove = true;
            WrongMove();

        }
        else
        {
            ResetBaloon();
        }
        _dirty = p_dirty;
       
    }

    public void SetPulga(bool p_pulga)
    {
        if(p_pulga == _pulga)
        {
            wrongMove = true;
            WrongMove();
        }
        else
        {
            //_myUrgentBaloon.GetComponent<Baloon>().NoSprite();
            //_myUrgentBaloon.SetActive(false);
            ResetBaloon();
            PlayAnimation("Happy");
        }
        _pulga = p_pulga;
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
        ResetBaloon();
    }

    void PlayAnimation(string name)
    {
        _myAnimator.Play(name);
    }
}
