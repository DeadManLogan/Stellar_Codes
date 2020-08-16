using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Actions/On Mouse Click")]
public class OnMouseClick : Action
{
    public override void Execute(float d)       //d is for delta time
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = Settings.GetUIObjs();        // that is a list that has every object we hit

            foreach (RaycastResult r in results)
            {
                IClickable c = r.gameObject.GetComponentInParent<IClickable>();
                if(c != null)
                {
                    c.OnClick();
                    break;
                }
            }
        }
    }
}
