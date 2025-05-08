using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Linq;
using System;
using System.IO;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

public class Proyecto : MonoBehaviour
{
    Triangulo _myTriangleData;

    VisualElement _bodyRoot;
    VisualElement _hatRoot;
    VisualElement _accessoriesRoot;
    VisualElement _faceRoot;

    VisualElement _dripButton;

    
    VisualElement _bodyMenuRoot;
    VisualElement _hatMenuRoot;
    VisualElement _accessoriesMenuRoot;
    VisualElement _faceMenuRoot;

    private void OnEnable() 
    {
        VisualElement root      = GetComponent<UIDocument>().rootVisualElement;
        VisualElement baseMenu  = root.Q<VisualElement>("baseMenu");
        VisualElement rightArea = baseMenu.Q<VisualElement>("rightArea");

        VisualElement triangleElementsRoot = baseMenu.Q<VisualElement>("leftArea").Q<VisualElement>("character");

        _bodyRoot = triangleElementsRoot.Q<VisualElement>("bodies");
        _hatRoot = triangleElementsRoot.Q<VisualElement>("hats");
        _accessoriesRoot = triangleElementsRoot.Q<VisualElement>("accessories");
        _faceRoot = triangleElementsRoot.Q<VisualElement>("faces");
    
        _dripButton = rightArea.Q<VisualElement>("buttonArea").Q<VisualElement>("dripButton");

        // To do: callback de dripar

        VisualElement menuRoot = rightArea.Q<VisualElement>("itemSelector").Q<VisualElement>("items");

        _bodyMenuRoot          = menuRoot.Q<VisualElement>("items_body");
        _hatMenuRoot           = menuRoot.Q<VisualElement>("items_hat");
        _accessoriesMenuRoot   = menuRoot.Q<VisualElement>("items_accessory");
        _faceMenuRoot          = menuRoot.Q<VisualElement>("items_face");

        menuRoot.Query("items_body")
            .Descendents<VisualElement>()
            .ForEach(elem => elem.RegisterCallback<ClickEvent>(CambioCuerpo));
        menuRoot.Query("items_hat")
            .Descendents<VisualElement>()
            .ForEach(elem => elem.RegisterCallback<ClickEvent>(CambioGorro));
        menuRoot.Query("items_accessory")
            .Descendents<VisualElement>()
            .ForEach(elem => elem.RegisterCallback<ClickEvent>(CambioAccesorio));
        menuRoot.Query("items_face")
            .Descendents<VisualElement>()
            .ForEach(elem => elem.RegisterCallback<ClickEvent>(CambioCara));

        _myTriangleData = BaseDatos.getData();

        UpdateUI();

        _myTriangleData.Cambio += UpdateUI;
    }

    void UpdateUI() {
        _bodyRoot.      Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _hatRoot.       Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _accessoriesRoot.Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _faceRoot.      Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);

        _bodyMenuRoot.Children().ToList().ForEach       (elem => LowlightSelected(elem));
        _hatMenuRoot.Children().ToList().ForEach        (elem => LowlightSelected(elem));
        _accessoriesMenuRoot.Children().ToList().ForEach(elem => LowlightSelected(elem));
        _faceMenuRoot.Children().ToList().ForEach       (elem => LowlightSelected(elem));

        Debug.Log(_myTriangleData.Cuerpo);
        Debug.Log(_bodyRoot);
        Debug.Log(_bodyRoot.Q<VisualElement>(_myTriangleData.Cuerpo));


        _bodyRoot.Q<VisualElement>(_myTriangleData.Cuerpo).style.display = DisplayStyle.Flex;

        VisualElement m = _bodyMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Cuerpo);
        HighlightSelected(m);

        if (_myTriangleData.Gorro.Length > 0) {
            Debug.Log(_hatRoot.Q<VisualElement>(_myTriangleData.Gorro));
            _hatRoot.Q<VisualElement>(_myTriangleData.Gorro).style.display = DisplayStyle.Flex;
            m = _hatMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Gorro);

            HighlightSelected(m);
        }

        if (_myTriangleData.Accesorio.Length > 0) {
            _accessoriesRoot.Q<VisualElement>(_myTriangleData.Accesorio).style.display = DisplayStyle.Flex;
            m = _accessoriesMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Accesorio);
            HighlightSelected(m);
        }

        if (_myTriangleData.Cara.Length > 0) {
            _faceRoot.Q<VisualElement>(_myTriangleData.Cara).style.display = DisplayStyle.Flex;
            m = _faceMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Cara);
            HighlightSelected(m);
        }


        // To do: falta sumar el score del drip
    }

    void CambioCuerpo(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Cuerpo = n.Substring(n.IndexOf("menu_"));

        GuardarTriangulo();
    }

    void CambioGorro(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Gorro = n.Substring(n.IndexOf("menu_"));

        GuardarTriangulo();
    }

    void CambioAccesorio(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Accesorio = n.Substring(n.IndexOf("menu_"));

        GuardarTriangulo();
    }

    void CambioCara(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Cara = n.Substring(n.IndexOf("menu_"));

        GuardarTriangulo();
    }

    void HighlightSelected(VisualElement v) {
        v.style.borderBottomColor = Color.black;
        v.style.borderLeftColor = Color.black;
        v.style.borderRightColor = Color.black;
        v.style.borderTopColor = Color.black;

        v.style.borderTopWidth = 5;
        v.style.borderBottomWidth = 5;
        v.style.borderLeftWidth = 5;
        v.style.borderRightWidth = 5;
    }

    void LowlightSelected(VisualElement v) {
        v.style.borderBottomColor = Color.clear;
        v.style.borderLeftColor = Color.clear;
        v.style.borderRightColor = Color.clear;
        v.style.borderTopColor = Color.clear;

        v.style.borderTopWidth = 0;
        v.style.borderBottomWidth = 0;
        v.style.borderLeftWidth = 0;
        v.style.borderRightWidth = 0;
    }


    void GuardarTriangulo() 
    {
        StreamWriter sr = new StreamWriter("Assets/Resources/save");
        sr.Write(JsonUtility.ToJson(_myTriangleData));
        sr.Close();

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