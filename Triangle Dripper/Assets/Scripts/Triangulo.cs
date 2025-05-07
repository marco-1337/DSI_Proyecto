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
                Cambio?.Invoke();
            }
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
                Cambio?.Invoke();
            }
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
                Cambio?.Invoke();
            }
        }
    }

    public Triangulo(string cuerpo = "a", string cara = "b", string accesorio = "c", string gorro = "d")
    {
        _cuerpo = cuerpo;
        _cara = cara;
        _accesorio = accesorio;
        _gorro = gorro;
    } 
}
