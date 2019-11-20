using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogState : MonoBehaviour
{    
    public Animator _myAnimator;
    
    public GameObject _myUrgentBaloon;

    public SpriteRenderer _mudStain;

    public SpriteRenderer _fleaStain;

    public SpriteRenderer _chain;

    public FileSettings fileSettings;

    public float happines = 0;

    public Button chainB;


    //GameObject _myBaloon;
    bool wrongMove = false;
    int erros = 0;
    public GameObject baloonSprite;

    List<State> _medicalStates;
    List<State> _badStates;
    List<State> _dogStates;
    List<State> _dogMedStates;
    List<State> _dogFixedMstates;

    int _happiness = 0; //progressão do player

    State sad = new State(2, "Triste");
    State hungry = new State(3, "Fome");
    State dirty = new State(1, "Sujo");
    State pulga = new State(1, "Pulga");
    State acorrentado = new State(1, "Acorrentado");


    //estados médicos
    State castracao = new State(0, "Castração");
    //nao pode castrar sem resolvido os os estados ruins do cachorro
    State vacina = new State(0, "Vacinação");
    //se nunca teve dono, sempre na rua
    State veterinario = new State(0, "Veterinário");
    //se machucado
    State remedios = new State(0, "Remédios");
    //se doente


    void Start()
    {
       

        InicializarStatus();

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
                case "Fome":
                    showHungry();
                    PlayAnimation("Angry");
                    break;
                case "Triste":
                    showSad();
                    PlayAnimation("Angry");
                    break;
                case "Sujo":
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
    }
   
    public void InicializarStatus()
    {
        _myAnimator.SetBool("IdleSad", false);
        _mudStain.enabled = false;
        _fleaStain.enabled = false;
        _chain.enabled = false;
        
        _badStates = new List<State>();
        _dogStates = new List<State>();
        
        _badStates.Add(hungry);
        _badStates.Add(sad);
        _badStates.Add(dirty);
        _badStates.Add(pulga);
        _badStates.Add(acorrentado);

        int numBadStates = 3;
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
            Debug.Log(_dogStates[i].nome + " " + _dogStates[i].peso);
        }
        

        if (checkState("Acorrentado"))
        {
            _chain.enabled = true;
            chainB.enabled = true;
        }
        if (checkState("Sujo"))
        {
            _mudStain.enabled = true;
        }
        if (checkState("Pulga"))
        {
            _fleaStain.enabled = true;
        }
        if (checkState("Triste"))
        {
            PlayAnimation("Sad");
            _myAnimator.SetBool("IdleSad", true);
            Debug.Log("Checou triste");
        }
        else
        {
            PlayAnimation("Idle");

        }
        if (checkState("Fome"))
        {
            //play hmmmmm sound - som de fome
        }
        StartMedicalStats();
    }

    void StartMedicalStats()
    {
        //listas
        _medicalStates = new List<State>();
        _dogMedStates = new List<State>();
        _dogFixedMstates = new List<State>();
        //stados adicionados
        _medicalStates.Add(castracao);
        _medicalStates.Add(veterinario);
        _medicalStates.Add(vacina);
        _medicalStates.Add(remedios);

        int numBadStates = 2;
        int r;

        for (int i = 0; i < numBadStates; i++)
        {
            r = Random.Range(0, _medicalStates.Count);
            _dogMedStates.Add(_medicalStates[r]);
            _dogFixedMstates.Add(_medicalStates[r]);
            _medicalStates.RemoveAt(r);
        }
        for (int i = 0; i < _dogMedStates.Count; i++)
        {
            Debug.Log(_dogMedStates[i].nome + " medicStats");
        }
        fileSettings.resetFile();
        fileSettings.InitializeFile(_dogMedStates);
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
    public bool checkState(string _state)
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

    public bool checkMedicalState(string _state)
    {
        for (int i = 0; i < _dogMedStates.Count; i++)
        {
            if (_dogMedStates[i].nome == _state)
            {
                return true;
            }
        }
        return false;
    }
    int checkMstatePos(string _state)
    {
        for (int i = 0; i < _dogFixedMstates.Count; i++)
        {
            if (_dogFixedMstates[i].nome == _state)
            {
                //Debug.Log(_dogMedStates[i].nome + " vs " + _state + " i = " + i);
                return i;
            }
        }
        return 100;
    }
    void RemoveState(string _state)
    {
        for (int i = 0; i < _dogStates.Count; i++)
        {
            if (_dogStates[i].nome == _state)
            {
                //fileSettings.SetToggled(i);
                _dogStates.RemoveAt(i);
            }
        }
    }
    void RemoveMedicalState(string _state)
    {
        for (int i = 0; i < _dogMedStates.Count; i++)
        {
            if (_dogMedStates[i].nome == _state)
            {
                _dogMedStates.RemoveAt(i);
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
            RemoveState(p_state);
            if (p_state == "Sujo")
            {
                _mudStain.enabled = false;
            }
            else if(p_state == "Pulga")
            {
                _fleaStain.enabled = false;
            }
            else if(p_state == "Triste")
            {
                _myAnimator.SetBool("IdleSad", false);
            }
            else if(p_state == "Acorrentado")
            {
                _chain.enabled = false;
                chainB.enabled = false;
            }
        }
    }
    public void SetMedicalState(string p_state)
    {
        if (!checkMedicalState(p_state))
        {
            //erros++
            //desabilitar botao da acao
            Debug.Log("Nao tem essa necessidade");
        }
        else if(checkMedicalState(p_state))
        {
            PlayAnimation("Happy");
            int p = checkMstatePos(p_state);
            fileSettings.SetLine(p);
            RemoveMedicalState(p_state);
            
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

    void PlayAnimation(string p_name)
    {
        _myAnimator.Play(p_name);
    }
}
