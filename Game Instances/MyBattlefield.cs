using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Objects/My battlefield")]
public class MyBattlefield : ObjectLogic
{
    public override void OnClick(CardInstance inst)
    {
        Debug.Log("This card is in my battlefield.");       // if the card which is being clicked is in my battlefield this shows up.
    }

    public override void OnHighlight(CardInstance inst)
    {

    }
}
