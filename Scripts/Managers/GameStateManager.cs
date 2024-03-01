using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates {Menu, Playing, GameOver, Paused}

public class GameStateManager : MonoBehaviour
{
    
    private GameStates currentGameState = GameStates.Menu;

    public void SetState(GameStates newState)
    {
        currentGameState = newState;
    }

    public GameStates GetState()
    {
        return currentGameState;
    }
}
