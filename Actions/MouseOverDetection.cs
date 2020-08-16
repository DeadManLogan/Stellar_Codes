using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Actions/Mouse Over Detection")]
public class MouseOverDetection : Action
{
    public override void Execute(float d)       //d is for delta time
    {

        List<RaycastResult> results = Settings.GetUIObjs();        // that is a list that has every object we hit


        IClickable c = null;


        foreach (RaycastResult r in results)
        {
            c = r.gameObject.GetComponentInParent<IClickable>();
            if(c != null)
            {
                c.OnHighlight();
                break;
            }
        }
    }
}
