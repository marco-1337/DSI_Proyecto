using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonHoverManipulator : Manipulator
{
    public ButtonHoverManipulator()
    { }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseOverEvent>(OnMouseEnter);
        target.RegisterCallback<MouseOutEvent>(OnMouseExit);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseOverEvent>(OnMouseEnter);
        target.UnregisterCallback<MouseOutEvent>(OnMouseExit);
    }

    private void OnMouseEnter(MouseOverEvent e) {
        target.style.backgroundColor  = Color.gray;
        e.StopPropagation();
    }

    private void OnMouseExit(MouseOutEvent e) {
    
        target.style.borderBottomColor  = Color.white;
        e.StopPropagation();
    }
}