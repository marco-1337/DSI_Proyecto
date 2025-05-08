using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;


public class DripControl : VisualElement
{
    private int _drip;
    public int drip
    {
        get { return _drip; }
        set { _drip = value; }
    }

    public new class UxmlFactory : UxmlFactory<DripControl, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription dripLevel = new UxmlIntAttributeDescription { name = "drip", defaultValue = 1 };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            ((DripControl)ve).drip = dripLevel.GetValueFromBag(bag, cc);
        }
    }

}