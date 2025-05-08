using UnityEngine;
using UnityEngine.UIElements;

public class TabbedMenu : MonoBehaviour
{

    #region menu personalización

    VisualElement tabs;
    VisualElement items;

    VisualElement tab_body;
    VisualElement tab_face;
    VisualElement tab_hat;
    VisualElement tab_accessory;

    VisualElement items_body;
    VisualElement items_face;
    VisualElement items_hat;
    VisualElement items_accessory;

    private void OcultarContenido()
    {
        foreach (var item in items.Children())
            item.style.display = DisplayStyle.None;
    }

    private void ResetBg()
    {
        foreach (var tab in tabs.Children()) 
        {
            tab.RemoveFromClassList("blue3Bg");
            tab.AddToClassList("blue2Bg");
        }
    }

    private void SelectedBg(VisualElement tab)
    {
        tab.RemoveFromClassList("blue2Bg");
        tab.AddToClassList("blue3Bg");
    }

    #endregion

    #region pantallas

    VisualElement baseMenu;
    VisualElement baseResult;

    Button dripButton;
    Button returnButton;

    private void showResult()
    {
        baseMenu.style.display = DisplayStyle.None;
        baseResult.style.display = DisplayStyle.Flex;
    }
    private void showMenu()
    {
        baseMenu.style.display = DisplayStyle.Flex;
        baseResult.style.display = DisplayStyle.None;
    }

    #endregion

    private void OnEnable()
    {
        UIDocument document = GetComponent<UIDocument>();
        VisualElement rootve = document.rootVisualElement;

        // MENU PERSONALIZACIÓN
        tabs = rootve.Q("tabs");
        items = rootve.Q("items");

        tab_body = tabs.Q("tab_body");
        tab_face = tabs.Q("tab_face");
        tab_hat = tabs.Q("tab_hat");
        tab_accessory = tabs.Q("tab_accessory");

        items_body = items.Q("items_body");
        items_face = items.Q("items_face");
        items_hat = items.Q("items_hat");
        items_accessory = items.Q("items_accessory");


        // CALLBACKS TABBED MENU
        tab_body.RegisterCallback<MouseDownEvent>(ev =>
        {
            OcultarContenido();
            ResetBg();
            SelectedBg(tab_body);
            items_body.style.display = DisplayStyle.Flex;
        });

        tab_face.RegisterCallback<MouseDownEvent>(ev =>
        {
            OcultarContenido();
            ResetBg();
            SelectedBg(tab_face);
            items_face.style.display = DisplayStyle.Flex;
        });

        tab_hat.RegisterCallback<MouseDownEvent>(ev =>
        {
            OcultarContenido();
            ResetBg();
            SelectedBg(tab_hat);
            items_hat.style.display = DisplayStyle.Flex;
        });

        tab_accessory.RegisterCallback<MouseDownEvent>(ev =>
        {
            OcultarContenido();
            ResetBg();
            SelectedBg(tab_accessory);
            items_accessory.style.display = DisplayStyle.Flex;
        });


        // PANTALLAS
        baseMenu = rootve.Q("baseMenu");
        baseResult = rootve.Q("baseResult");

        dripButton = baseMenu.Q<Button>("dripButton");
        returnButton = baseResult.Q<Button>("returnButton");

        // EVENTOS BOTONES PANTALLAS
        dripButton.clicked += showResult;
        returnButton.clicked += showMenu;


        showMenu();
        OcultarContenido();
        items_face.style.display = DisplayStyle.Flex;
        ResetBg();
        SelectedBg(tab_body);
    }

    private void OnDisable()
    {
        dripButton.clicked -= showResult;
        returnButton.clicked -= showMenu;

        tab_body.UnregisterCallback<MouseDownEvent>(ev => { });
        tab_face.UnregisterCallback<MouseDownEvent>(ev => { });
        tab_hat.UnregisterCallback<MouseDownEvent>(ev => { });
        tab_accessory.UnregisterCallback<MouseDownEvent>(ev => { });
    }

}
