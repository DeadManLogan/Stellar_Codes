using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turns/Turn")]
public class Turn : ScriptableObject
{
    public PlayerHolder player;
    [System.NonSerialized]
    public int index = 0;       // index of the current phase
    public Phase[] phases;
    public PhaseVariable currentPhase;

    public PlayerAction[] turnStartAction;

    public void OnTurnStart()
    {
        if (turnStartAction == null)
        {
            return;
        }

        for (int i = 0; i < turnStartAction.Length; i++)
        {
            turnStartAction[i].Execute(player);
        }
    }
    public bool Execute()
    {
        bool result = false;

        currentPhase.value = phases[index];
        phases[index].OnStartPhase();

        bool phaseIsComplete = phases[index].IsComplete();

        if(phaseIsComplete)
        {
            phases[index].OnEndPhase();     // end phase
            index++;
            if (index > phases.Length - 1)
            {
                index = 0;
                result = true;
            }
        }

        return result;
    }

    public void EndCurrentPhase()
    {
        phases[index].forceExit = true;
    }
}
