using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseDatos
{
    public static Triangulo getData() 
    {
        Triangulo datos = new Triangulo();

        if (File.Exists("Assets/Resources/save"))
        {
            StreamReader sr = new StreamReader("Assets/Resources/save");
            datos = JsonUtility.FromJson<Triangulo>(sr.ReadToEnd());
        }

        return datos;
    }
}
