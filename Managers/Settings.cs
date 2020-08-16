using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Settings
{
    public static GameManager gameManager;
    private static ResourcesManager _resourcesManager;
    private static ConsoleHook _consoleManager;

    public static void RegisterEvent(string s, Color color)
    {
        if (_consoleManager == null)
        {
            _consoleManager = Resources.Load("ConsoleHook") as ConsoleHook;
        }

        _consoleManager.RegisterEvent(s, color);
    }

    public static ResourcesManager GetResourcesManager()
    {
        if(_resourcesManager == null)
        {
            _resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
            _resourcesManager.Init();
        }

        return _resourcesManager;
    }

    public static List<RaycastResult> GetUIObjs()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition      // the mouse's position
        };

        List<RaycastResult> results = new List<RaycastResult>();        // that is a list that has every object we hit
        EventSystem.current.RaycastAll(pointerData, results);   // shoots beam to detect all objects under the cursor

        return results;
    }

    public static void DropCreatureCard(Transform c, Transform p, Card/*Instance*/ cardInst)
    {
        SetParentForCard(c, p);
        
        //gameManager.currentPlayer.DropCard(cardInst);
    }

    public static void SetParentForCard(Transform c, Transform p)
    {
        c.SetParent(p);
        c.localPosition = Vector3.zero;     // we zero the z axis when the card is dropped
        c.localEulerAngles = Vector3.zero;     // we zero the rotation
        c.localScale = Vector3.one;      // we change the card's scale to 1 when dropped so it wont crush the game
    }

    public static void SetCardForBlock(Transform c, Transform p, int count)
    {
        Vector3 blockPosition = Vector3.zero;
        blockPosition.x += 150 * count;
        blockPosition.y -= 150 * count;
        SetParentForCard(c, p, blockPosition, Vector3.zero);
    }

    public static void SetParentForCard(Transform c, Transform p, Vector3 localPosition, Vector3 euler)
    {
        c.SetParent(p);
        c.localPosition = localPosition;     // we zero the z axis when the card is dropped
        c.localEulerAngles = euler;     // we zero the rotation
        c.localScale = Vector3.one;      // we change the card's scale to 1 when dropped so it wont crush the game
    }
}
