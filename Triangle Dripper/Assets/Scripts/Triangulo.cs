using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

[Serializable]
public class Triangulo
{
    public event Action Cambio;
    
    [SerializeField]
    private string _cuerpo;
    public string Cuerpo 
    {
        get { return _cuerpo; }
        set
        {
            if (value != _cuerpo)
            {
                _cuerpo = value;
                Cambio?.Invoke();
            }
        }
    }
    [SerializeField]
    private string _cara;
    public string Cara 
    {
        get { return _cara; }
        set
        {
            if (value != _cara)
            {
                _cara = value;
            }
            else 
            {
                _cara = "";
            }
            Cambio?.Invoke();
        }
    }

    [SerializeField]
    private string _accesorio;
    public string Accesorio 
    {
        get { return _accesorio; }
        set
        {
            if (value != _accesorio)
            {
                _accesorio = value;
            }
            else 
            {
                _accesorio = "";
            }
            Cambio?.Invoke();
        }
    }

    [SerializeField]
    private string _gorro;
    public string Gorro 
    {
        get { return _gorro; }
        set
        {
            if (value != _gorro)
            {
                _gorro = value;
            }
            else 
            {
                _gorro = "";
            }
            Cambio?.Invoke();
        }
    }

    private int _dripScore;
    public int DripScore
    {   
        get { return _dripScore;}
        set { _dripScore = value; }
    }

    public Triangulo(string cuerpo = "body_purple", string cara = "face_neutral", string accesorio = "", string gorro = "hat_mexican")
    {
        _cuerpo = cuerpo;
        _cara = cara;
        _accesorio = accesorio;
        _gorro = gorro;

        Debug.Log("" + _cuerpo + " " + _cara + " " + _accesorio + " " + _gorro);
    } 
}