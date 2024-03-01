using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    //reference to player
    [SerializeField]Transform player;
    //AI behavior
    [SerializeField]NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameStateManager.GetState() == GameStates.GameOver) return;
        //follow the player
        agent.SetDestination(player.position);
    }

    void OnEnable()
    {
        player = GameManager.Instance.player.transform;
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
        GameManager.Instance.scoreManager.AddScore(1); 
        int x = GameManager.Instance.scoreManager.GetScore();
        GameManager.Instance.uiManager.SetScoreText(x);
        GameManager.Instance.uiManager.StartScoreAnimation();
        CameraBehavior.Instance.StartCameraShake();
    }
}
