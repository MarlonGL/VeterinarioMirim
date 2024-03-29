﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DogState : MonoBehaviour
{    
    public Animator _myAnimator;
    
    public GameObject _myUrgentBaloon;

    public SpriteRenderer _mudStain;

    public SpriteRenderer _fleaStain;

    public SpriteRenderer _chain;

    public FileSettings fileSettings;
    
    public Button chainB;

    public GameObject results;
    public TextMeshProUGUI finalText;

    public Image progressBar;
    float maxPoints = 0f;
    float points = 0f;
    int score = 0;
    public Sprite[] sprites;
    public RuntimeAnimatorController[] anims;
    int dogSprite;

    public float happines = 0f;

    bool wrongMove = false;
    int erros = 0;
    public GameObject baloonSprite;

    List<State> _medicalStates;
    List<State> _badStates;
    List<State> _dogStates;
    List<State> _dogMedStates;
    List<State> _dogFixedMstates;
    
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

    public bool terminou = false;
    public Image[] stars;
    public Sprite star;

    public int sex = 0;
    public float age = 0f;

    Musics mu;

    void Start()
    {
        mu = GameObject.Find("Musicas").GetComponent<Musics>();
        dogSprite = Random.Range(0, 3);
        Debug.Log(dogSprite);
        GetComponent<SpriteRenderer>().sprite = sprites[dogSprite];
        GetComponent<Animator>().runtimeAnimatorController = anims[dogSprite];
        progressBar.fillAmount = points / maxPoints;
        Debug.Log(Global.numBadStates + " " + Global.numMedicalStates);
        sex = Random.Range(0, 2);
        age = Random.Range(0.2f, 4f);

        if (age >= 1f)
        {
            age = (int)age;
        }
        else if(age >=0.95f && age <1f)
        {
            age = 0.9f;
        }
        InicializarStatus();
    }

    void addPoints(float f)
    {
        points += f;
        progressBar.fillAmount = points / maxPoints;

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
        if(_dogStates.Count == 0 && terminou == false)
        {
            terminou = true;
            float f = (points * 100f) / maxPoints;
            score = (int)f;
            finalText.text = points.ToString() + " pontos";
            Invoke("ShowResults", 2.2f);
            Global.numBadStates += 1;
            Global.numMedicalStates += 1;
            //Debug.Log("terminou com " + score + "%");
            //fim da fase pontuacao etc
        }
    }
    void ShowResults()
    {
        results.SetActive(true);
        if(score >= 90)
        {
            stars[0].sprite = star;
            stars[1].sprite = star;
            stars[2].sprite = star;
            Global.faseStar[Global.faseAtual] = 3;
        }
        else if (score >= 70)
        {
            stars[0].sprite = star;
            stars[1].sprite = star;
            Global.faseStar[Global.faseAtual] = 2;
        }
        else if(score >= 50)
        {
            stars[0].sprite = star;
            Global.faseStar[Global.faseAtual] = 1;
        }
        Global.faseAtual += 1;
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

        //int numBadStates = 3;
        int numBadStates = Global.numBadStates;

        int r;
        int pesito;
        for (int i = 0; i < numBadStates; i++)
        {
            r = Random.Range(0, _badStates.Count);
            pesito = Random.Range(1, 4);
            _badStates[r].peso = pesito;
            _dogStates.Add(_badStates[r]);
            _badStates.Remove(_badStates[r]);
            maxPoints += (float)pesito * 10f;
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
            mu.PlaySad();
            //Debug.Log("Checou triste");
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
        if(age >= 0.6f)
        _medicalStates.Add(castracao);
        _medicalStates.Add(veterinario);
        _medicalStates.Add(vacina);
        _medicalStates.Add(remedios);

        //int numBadStates = 0;
        int numBadStates = Global.numMedicalStates;

        int r;

        for (int i = 0; i < numBadStates; i++)
        {
            r = Random.Range(0, _medicalStates.Count);
            _dogMedStates.Add(_medicalStates[r]);
            _dogFixedMstates.Add(_medicalStates[r]);
            _medicalStates.RemoveAt(r);
            maxPoints += 20f;
        }
        for (int i = 0; i < _dogMedStates.Count; i++)
        {
            Debug.Log(_dogMedStates[i].nome + " medicStats");
        }
        fileSettings.resetFile();
        fileSettings.InitializeFile(_dogMedStates, sex, age);
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
    public void WrongMove()
    {
        PlayAnimation("Angry");
        mu.PlayAngry();
        addPoints(maxPoints * -0.05f);
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
    public int getPesoState(string _state)
    {
        for (int i = 0; i < _dogStates.Count; i++)
        {
            if (_dogStates[i].nome == _state)
            {
                return _dogStates[i].peso;
            }
        }
        return -1;
    }

    public int getBiggestPeso()
    {
        State temp = new State(0, "temp");
        for (int i = 0; i < _dogStates.Count; i++)
        {
            if (_dogStates[i].peso > temp.peso)
            {
                temp = _dogStates[i];
            }
        }
        return temp.peso;
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
            int po = getPesoState(p_state);

            if (checkMedicalState("Veterinário"))
            {
                addPoints(po * 6f);
            }
            else if(po < getBiggestPeso())
            {
                addPoints(po * 8f);
            }
            else
            {
                addPoints(po * 10f);
            }
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
            addPoints(-10f);
            Debug.Log("Nao tem essa necessidade");
        }
        else if(checkMedicalState(p_state))
        {
            PlayAnimation("Happy");
            int p = checkMstatePos(p_state);
            fileSettings.SetLine(p);
            RemoveMedicalState(p_state);
            
            if (p_state != "Veterinário" && checkMedicalState("Veterinário"))
            {
                addPoints(10f);
            }
            else
            {
                addPoints(20f);
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

    void PlayAnimation(string p_name)
    {
        _myAnimator.Play(p_name);
        if(p_name == "Happy")
        {
            mu.PlayHappy();
        }
    }
}
