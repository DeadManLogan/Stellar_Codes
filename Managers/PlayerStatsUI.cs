using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    // all these can change. it depends on how you made the player portrait. dont forget to assign the objects on the portraits
    // and also assign the sprite on each player(Data/Players)
    public PlayerHolder player;
    public Image playerPortrait;
    public Text health;
    public Text userName;

    public void UpdateUsername()
    {
        //userName.text = player.username;      // enable when have username for portrait
        //playerPortrait.sprite = player.portrait;      // enable when have sprite for portrait
    }

    public void UpdateHealth()
    {
        health.text = player.health.ToString();
    }

    public void UpdateAll()
    {
        UpdateUsername();
        UpdateHealth();
    }
}
