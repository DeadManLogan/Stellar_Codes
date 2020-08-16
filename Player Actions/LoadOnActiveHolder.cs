using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Player Actions/Load On Active Holder")]
public class LoadOnActiveHolder : PlayerAction
{
    public override void Execute(PlayerHolder player)
    {
        GameManager.singleton.LoadPlayerOnActive(player);
    }
}
