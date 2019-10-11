using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState : MonoBehaviour
{
    bool _hungry;
    [SerializeField]
    Animator _myAnimator;
    [SerializeField]
    GameObject _myUrgentBaloon;
    [SerializeField]
    GameObject _myBaloon;

    // Start is called before the first frame update
    void Start()
    {
        _hungry = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_hungry)
        {
            _myUrgentBaloon.SetActive(false);
            _myAnimator.Play("Happy");
        }
        else
        {
            _myUrgentBaloon.SetActive(true);
        }
    }

    public void SetHungry(bool p_hugry)
    {
        _hungry = p_hugry;
    }
}
