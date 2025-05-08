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

    VisualElement _resultBodyRoot;
    VisualElement _resultHatRoot;
    VisualElement _resultAccessoriesRoot;
    VisualElement _resultFaceRoot;

    VisualElement _bodyMenuRoot;
    VisualElement _hatMenuRoot;
    VisualElement _accessoriesMenuRoot;
    VisualElement _faceMenuRoot;

    ProgressBar _dripBar;

    private void OnEnable() 
    {
        VisualElement root      = GetComponent<UIDocument>().rootVisualElement;
        VisualElement baseMenu  = root.Q<VisualElement>("baseMenu");
        VisualElement baseResult = root.Q("baseResult");
        VisualElement rightArea = baseMenu.Q<VisualElement>("rightArea");

        VisualElement triangleElementsRoot = baseMenu.Q<VisualElement>("leftArea").Q<VisualElement>("character");
        VisualElement resultTriangleElementsRoot = baseResult.Q<VisualElement>("character");

        _bodyRoot = triangleElementsRoot.Q<VisualElement>("bodies");
        _hatRoot = triangleElementsRoot.Q<VisualElement>("hats");
        _accessoriesRoot = triangleElementsRoot.Q<VisualElement>("accessories");
        _faceRoot = triangleElementsRoot.Q<VisualElement>("faces");

        _resultBodyRoot = resultTriangleElementsRoot.Q<VisualElement>("bodies");
        _resultHatRoot = resultTriangleElementsRoot.Q<VisualElement>("hats");
        _resultAccessoriesRoot = resultTriangleElementsRoot.Q<VisualElement>("accessories");
        _resultFaceRoot = resultTriangleElementsRoot.Q<VisualElement>("faces");

        _dripBar = baseResult.Q<VisualElement>("level").Q<ProgressBar>("dripBar");

        VisualElement menuRoot = rightArea.Q<VisualElement>("itemSelector").Q<VisualElement>("items");

        _bodyMenuRoot          = menuRoot.Q<VisualElement>("items_body");
        _hatMenuRoot           = menuRoot.Q<VisualElement>("items_hat");
        _accessoriesMenuRoot   = menuRoot.Q<VisualElement>("items_accessory");
        _faceMenuRoot          = menuRoot.Q<VisualElement>("items_face");

        menuRoot.Query("items_body")
            .Descendents<VisualElement>()
            .ForEach(
                elem => {
                    elem.RegisterCallback<ClickEvent>(CambioCuerpo);
                    elem.AddManipulator(new MenuHoverManipulator());
                }
            );

        menuRoot.Query("items_hat")
            .Descendents<VisualElement>()
            .ForEach(
                elem => {
                    elem.RegisterCallback<ClickEvent>(CambioGorro);
                    elem.AddManipulator(new MenuHoverManipulator());
                }
            );

        menuRoot.Query("items_accessory")
            .Descendents<VisualElement>()
            .ForEach(
                elem => {
                    elem.RegisterCallback<ClickEvent>(CambioAccesorio);
                    elem.AddManipulator(new MenuHoverManipulator());
                }
            );

        menuRoot.Query("items_face")
            .Descendents<VisualElement>()
            .ForEach(
                elem => {
                    elem.RegisterCallback<ClickEvent>(CambioCara);
                    elem.AddManipulator(new MenuHoverManipulator());
                }
            );

        _myTriangleData = BaseDatos.getData();

        UpdateUI();

        _myTriangleData.Cambio += UpdateUI;
    }

    void UpdateUI() {
        _bodyRoot.      Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _hatRoot.       Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _accessoriesRoot.Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _faceRoot.      Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _resultBodyRoot.      Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _resultHatRoot.       Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _resultAccessoriesRoot.Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);
        _resultFaceRoot.      Children().ToList().ForEach(elem => elem.style.display = DisplayStyle.None);

        _bodyMenuRoot.Children().ToList().ForEach       (elem => LowlightSelected(elem));
        _hatMenuRoot.Children().ToList().ForEach        (elem => LowlightSelected(elem));
        _accessoriesMenuRoot.Children().ToList().ForEach(elem => LowlightSelected(elem));
        _faceMenuRoot.Children().ToList().ForEach       (elem => LowlightSelected(elem));

        _myTriangleData.DripScore = 0;

        _bodyRoot.Q<VisualElement>(_myTriangleData.Cuerpo).style.display = DisplayStyle.Flex;
        _resultBodyRoot.Q<VisualElement>(_myTriangleData.Cuerpo).style.display = DisplayStyle.Flex;

        VisualElement m = _bodyMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Cuerpo);
        HighlightSelected(m);
        _myTriangleData.DripScore += _bodyMenuRoot.Q<DripControl>("menu_" + _myTriangleData.Cuerpo).drip;

        if (_myTriangleData.Gorro.Length > 0) {
            _hatRoot.Q<VisualElement>(_myTriangleData.Gorro).style.display = DisplayStyle.Flex;
            _resultHatRoot.Q<VisualElement>(_myTriangleData.Gorro).style.display = DisplayStyle.Flex;

            m = _hatMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Gorro);
            HighlightSelected(m);
            _myTriangleData.DripScore += _hatMenuRoot.Q<DripControl>("menu_" + _myTriangleData.Gorro).drip;
        }

        if (_myTriangleData.Accesorio.Length > 0) {
            _accessoriesRoot.Q<VisualElement>(_myTriangleData.Accesorio).style.display = DisplayStyle.Flex;
            _resultAccessoriesRoot.Q<VisualElement>(_myTriangleData.Accesorio).style.display = DisplayStyle.Flex;

            m = _accessoriesMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Accesorio);
            HighlightSelected(m);

            _myTriangleData.DripScore += _accessoriesMenuRoot.Q<DripControl>("menu_" + _myTriangleData.Accesorio).drip;
        }

        if (_myTriangleData.Cara.Length > 0) {
            _faceRoot.Q<VisualElement>(_myTriangleData.Cara).style.display = DisplayStyle.Flex;
            _resultFaceRoot.Q<VisualElement>(_myTriangleData.Cara).style.display = DisplayStyle.Flex;
        
            m = _faceMenuRoot.Q<VisualElement>("menu_" + _myTriangleData.Cara);
            HighlightSelected(m);

            _myTriangleData.DripScore += _faceMenuRoot.Q<DripControl>("menu_" + _myTriangleData.Cara).drip;
        }

        Debug.Log(_myTriangleData.DripScore);
        _dripBar.value = _myTriangleData.DripScore;
    }

    void CambioCuerpo(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Cuerpo = n.Substring(5);

        GuardarTriangulo();
    }

    void CambioGorro(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Gorro = n.Substring(5);

        GuardarTriangulo();
    }

    void CambioAccesorio(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Accesorio = n.Substring(5);

        GuardarTriangulo();
    }

    void CambioCara(ClickEvent e) {
        string n = (e.target as VisualElement).name;
        _myTriangleData.Cara = n.Substring(5);

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
}