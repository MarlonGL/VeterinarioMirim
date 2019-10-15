using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState : MonoBehaviour
{
    bool _hungry, _happy, _dirty;
    [SerializeField]
    Animator _myAnimator;
    [SerializeField]
    GameObject _myUrgentBaloon;
    [SerializeField]
    //GameObject _myBaloon;
    bool wrongMove = false;

    public GameObject baloonSprite;

    // Start is called before the first frame update
    void Start()
    {
        //aleatoriedade
        _hungry = true;
        _happy = false;
        _dirty = false;
    }

    // Update is called once per frame
    //animacoes
    //happyAnim - happy
    void Update()
    {
        if(!_hungry)
        {
            _myUrgentBaloon.GetComponent<Baloon>().NoSprite();
            _myUrgentBaloon.SetActive(false);
            //_myAnimator.Play("Happy");
            //_myAnimator.SetTrigger("happyAnim");

        }
        else
        {
            _myUrgentBaloon.SetActive(true);
            _myUrgentBaloon.GetComponent<Baloon>().setSpriteHungry();

        }

        if(wrongMove)
        {
            _myAnimator.Play("Angry");
            _myUrgentBaloon.SetActive(true);
            _myUrgentBaloon.GetComponent<Baloon>().setSpriteAngry();
            Invoke("ResertWrongMove", 3);
        }
    }

    public void SetHungry(bool p_hugry)
    {
        _hungry = p_hugry;
        _myAnimator.Play("Happy");
    }

    public void setHappy(bool p_happy)
    {
        _happy = p_happy;
    }

    public void setDirty(bool p_dirty)
    {
        if (p_dirty == _dirty)
        {
            wrongMove = true;
        }
        else
        {
            _myUrgentBaloon.GetComponent<Baloon>().NoSprite();
            _myUrgentBaloon.SetActive(false);
        }
        _dirty = p_dirty;
       
    }

    void ResertWrongMove()
    {
        wrongMove = false;
        _myUrgentBaloon.GetComponent<Baloon>().NoSprite();
        _myUrgentBaloon.SetActive(false);
    }
}
