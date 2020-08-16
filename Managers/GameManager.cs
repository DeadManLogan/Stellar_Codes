﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]
    public PlayerHolder[] all_players;

    public PlayerHolder GetEnemyOf(PlayerHolder p)
    {
        for (int i = 0; i < all_players.Length; i++)
        {
            if (all_players[i] != p)
            {
                return all_players[i];
            }
        }
        return null;
    }

    public PlayerHolder currentPlayer;
    public CardHolder playerOneHolder;
    public CardHolder otherPlayerHolder;
    public State currentState;
    public GameObject cardPrefab;

    public int turnIndex;
    public Turn[] turns;
    public GameEvent onTurnChanged;
    public GameEvent onPhaseChanged;
    public StringVariable turnText;

    public PlayerStatsUI[] statsUI;
    public TransformVariable graveyardVariable;     // if you dont want the graveyard to be one, and want seperate for each player
    // instead of adding it to gamemanager add it to player holder 
    List<CardInstance> graveyardCards = new List<CardInstance>();

    Dictionary<CardInstance, BlockInstance> blockInstances = new Dictionary<CardInstance, BlockInstance>();

    public Dictionary<CardInstance, BlockInstance> GetBlockInstances()
    {
        return blockInstances;
    }

    public void ClearBlockInstances()
    {
        blockInstances.Clear();
    }

    public void AddBlockInstance(CardInstance attacker, CardInstance blocker, ref int count)
    {
        BlockInstance b = null;
        b = GetBlockInstanceOfAttacker(attacker);
        if (b == null)
        {
            b = new BlockInstance();
            b.attacker = attacker;
            blockInstances.Add(attacker, b);
        }
        if (!b.blocker.Contains(blocker))
        {
            b.blocker.Add(blocker);
        }

        count = b.blocker.Count;
    }

    BlockInstance GetBlockInstanceOfAttacker(CardInstance attacker)
    {
        BlockInstance r = null;
        blockInstances.TryGetValue(attacker, out r);
        return r;
    }

    public static GameManager singleton;

    public void Awake()
    {
        singleton = this;

        all_players = new PlayerHolder[turns.Length];
        for (int i = 0; i < turns.Length; i++)
        {
            all_players[i] = turns[i].player;
        }
        currentPlayer = turns[0].player;
    }

    private void Start()
    {
        Settings.gameManager = this;

        SetupPlayers();

        turns[0].OnTurnStart();
        turnText.value = turns[turnIndex].player.username;
        onTurnChanged.Raise();
    }

    void SetupPlayers()
    {
        ResourcesManager rm = Settings.GetResourcesManager();

        for (int i = 0; i < all_players.Length; i++)
        {
            all_players[i].Init();
            if (i == 0)
            {
                all_players[i].currentHolder = playerOneHolder;
            }
            else
            {
                all_players[i].currentHolder = otherPlayerHolder;
            }
            
            all_players[i].statsUI = statsUI[i];
            all_players[i].currentHolder.LoadPlayer(all_players[i], all_players[i].statsUI);
        }
    }

    public void LoadPlayerOnActive(PlayerHolder p)
    {
        PlayerHolder prevPlayer = playerOneHolder.playerHolder;
        if (prevPlayer != p)
        {
            LoadPlayerOnHolder(prevPlayer, otherPlayerHolder, statsUI[1]);
        }
        LoadPlayerOnHolder(p, playerOneHolder, statsUI[0]);
    }

    public void PickNewCardFromDeck(PlayerHolder p)
    {
        if (p.all_cards.Count == 0)
        {
            Debug.Log("Game Over");
            return;
        }
        ResourcesManager rm = Settings.GetResourcesManager();

        string cardId = p.all_cards[0];
        p.all_cards.RemoveAt(0);
        GameObject go = Instantiate(cardPrefab) as GameObject;
        CardViz v = go.GetComponent<CardViz>();
        v.LoadCard(rm.GetCardInstance(cardId));     // loads the instance to cardviz
        CardInstance inst = go.GetComponent<CardInstance>();
        inst.owner = p;
        inst.currentLogic = p.handLogic;
        Settings.SetParentForCard(go.transform, p.currentHolder.handGrid.value);
        p.handCards.Add(inst); // this line is from ep 17 for switcing players
    }

    public void LoadPlayerOnHolder(PlayerHolder p, CardHolder h, PlayerStatsUI ui)
    {
        h.LoadPlayer(p, ui);
    }   

    private void Update()
    {
        bool isComplete = turns[turnIndex].Execute();

        if (isComplete)
        {
            turnIndex++;
            if (turnIndex > turns.Length - 1)
            {
                turnIndex = 0;
            }

            // the current player has changed here
            currentPlayer = turns[turnIndex].player;        // this line is from tutorial 20. might not be needed
            turns[turnIndex].OnTurnStart();        // this line is from tutorial 20. might not be needed
            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }

        if (currentState != null)
        {
            currentState.Tick(Time.deltaTime);
        }
        
    }

    public void SetState(State state)      //  we control which state we are going to
    {
        currentState = state;
    }

    public void EndCurrentPhase()
    {
        Settings.RegisterEvent(turns[turnIndex].name + " finished ", currentPlayer.playerColor);

        turns[turnIndex].EndCurrentPhase();
    }

    public void PutCardToGraveyard(CardInstance c)
    {
        c.owner.CardToGraveyard(c);

        graveyardCards.Add(c);
        c.transform.SetParent(graveyardVariable.value);
        Vector3 p = Vector3.zero;
        p.x = graveyardCards.Count * 10;
        p.z = graveyardCards.Count * 10;
        c.transform.localPosition = p;
        c.transform.localRotation = Quaternion.identity;
        c.transform.localScale = Vector3.one;
    }
}
