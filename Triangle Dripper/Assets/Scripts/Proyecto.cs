using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Linq;
using System;
using System.IO;

public class Proyecto : MonoBehaviour
{
    Triangulo _myTriangleData;

    VisualElement _bodyRoot;
    VisualElement _hatRoot;
    VisualElement _accesoriesRoot;
    VisualElement _faceRoot;

    VisualElement _bodyMenuRoot;
    VisualElement _hatMenuRoot;
    VisualElement _accesoriesMenuRoot;
    VisualElement _faceMenuRoot;

    VisualElement _dripButton;

    private void OnEnable() 
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _dripButton = root.Q<VisualElement>();

        VisualElement triangleElementsRoot = root.Q<VisualElement>("baseMenu").Q<VisualElement>("leftArea").Q<VisualElement>("character");

        _bodyRoot = root.Q<VisualElement>("CardsSection");
        _hatRoot = root.Q<VisualElement>("BotonCrear");
        _accesoriesRoot = root.Q<VisualElement>("BotonGuardar");
        _faceRoot = root.Q<Toggle>("ToggleModificar");
    
    /*
        contenerdorDerecha.RegisterCallback<ClickEvent>(SeleccionTarjeta);
        botonCrear.RegisterCallback<ClickEvent>(NuevaTarjeta);
        botonGuardar.RegisterCallback<ClickEvent>(GuardarIndividuos);
        inputNombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
        inputApellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);
        root.Query("MenuImages")
            .Descendents<VisualElement>()
            .ForEach(elem => elem.RegisterCallback<ClickEvent>(CambioImagen));
    */
        _myTriangleData = BaseDatos.getData();


    }

/*
    void CambioNombre(ChangeEvent<string> e) 
    {
        if (selecIndividuo != null) selecIndividuo.Nombre = e.newValue;
    }

    void CambioApellido(ChangeEvent<string> e) 
    {
        if (selecIndividuo != null) selecIndividuo.Apellido = e.newValue;
    }

    void CambioImagen(ClickEvent e)
    {
        Texture2D imagen = (e.target as VisualElement).style.backgroundImage.value.texture;
        if (selecIndividuo != null)
        {
            selecIndividuo.BackgroundTexture = imagen;
        }
    }


    void SeleccionTarjeta(ClickEvent e)
    {
        VisualElement tarjeta = e.target as VisualElement;
        selecIndividuo = tarjeta.userData as IndividuoP6;

        inputNombre.SetValueWithoutNotify(selecIndividuo.Nombre);
        inputApellido.SetValueWithoutNotify(selecIndividuo.Apellido);

        toggleModificar.value = true;

        tarjetasBordeNegro();
        tarjetaBordeBlanco(tarjeta);
    }

    void NuevaTarjeta(ClickEvent e) 
    {
        if (!toggleModificar.value && listaIndividuos.Count < 9) 
        {
            if (assetTarjeta == null) 
            {
                Debug.LogError("Plantilla no detectada, añade la plantilla de la tarjeta al campo serializado 'assetTarjeta'");
            }
            else
            {
                VisualElement nuevaTarjeta = assetTarjeta.Instantiate();
                IndividuoP6 individuo = new IndividuoP6(inputNombre.value, inputApellido.value);
                TarjetaP6 tarjetaData = new TarjetaP6(nuevaTarjeta, individuo);

                contenerdorDerecha.Add(nuevaTarjeta);
                
                selecIndividuo = individuo;

                tarjetasBordeNegro();
                tarjetaBordeBlanco(nuevaTarjeta);

                listaIndividuos.Add(individuo);
            }
        }
    }

    void GuardarIndividuos(ClickEvent e) 
    {
        StreamWriter sr = new StreamWriter("Assets/Resources/guardadoP6");

        sr.Write(JsonHelperIndividuo.ToJson(listaIndividuos, true));

        sr.Close();

    }

    void tarjetasBordeNegro() 
    {
        List<VisualElement> listaTarjetas = contenerdorDerecha.Children().ToList();
        listaTarjetas.ForEach(
            elem => 
            {
                VisualElement tarjeta = elem.Q("Card");

                tarjeta.style.borderBottomColor = Color.black;
                tarjeta.style.borderTopColor    = Color.black;
                tarjeta.style.borderRightColor  = Color.black;
                tarjeta.style.borderLeftColor   = Color.black;
            }
        );
    }

    void tarjetaBordeBlanco(VisualElement tarjetaRoot) 
    {
        VisualElement tarjeta = tarjetaRoot.Q("Card");

        tarjeta.style.borderBottomColor = Color.white;
        tarjeta.style.borderTopColor    = Color.white;
        tarjeta.style.borderRightColor  = Color.white;
        tarjeta.style.borderLeftColor   = Color.white;
    }
*/
}

