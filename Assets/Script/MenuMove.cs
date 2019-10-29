using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMove : MonoBehaviour
{
    public int move;
    public float speed;
    bool _moved = false;
    bool _clicked = false;
    Vector3 _destiny;

    public void Update()
    {
        if (_clicked)
        {
            if (!_moved)
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, _destiny, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, _destiny) <= 1)
                {
                    _clicked = false;
                    _moved = true;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(gameObject.transform.position, _destiny, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, _destiny) <= 1)
                {
                    _clicked = false;
                    _moved = false;
                }
            }
        }
    }
    public void Activate()
    {
        if (!_clicked)
        {
            if (!_moved)
            {
                _destiny = new Vector3(gameObject.transform.position.x + move, gameObject.transform.position.y);
            }
            else if (_moved)
            {
                _destiny = new Vector3(gameObject.transform.position.x - move, gameObject.transform.position.y);
            }
            _clicked = true;
        }
    }

    public bool GetMoved()
    {
        return _moved;
    }
}
