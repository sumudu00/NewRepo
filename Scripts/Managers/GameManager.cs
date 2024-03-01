using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public PlayerBehavior player;

    public ScoreManager scoreManager;

    public UIManager uiManager;

    public GameStateManager gameStateManager;

    public AudioManager audioManager;

    public EnemySpawner enemySpawner;
    
}
