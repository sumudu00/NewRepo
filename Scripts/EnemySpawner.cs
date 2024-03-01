using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //reference to the enemy prefab
    [SerializeField] GameObject enemyPrefab;

    //spawn with a timer
    //timer...
    [SerializeField, Min(1)] float maxTime;
    float currentTime = 0;

    List<GameObject> enemyPool;
    [SerializeField] int enemyCount;
    int enemyId;


    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new List<GameObject>();
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemyPool.Add(enemy);
            enemy.SetActive(false);

        }
        enemyId = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameStateManager.GetState() == GameStates.GameOver) return;

        //count time
        currentTime += Time.deltaTime;
        //when time is max => spawn enemy
        currentTime = currentTime > maxTime ? maxTime : currentTime;

        if(currentTime == maxTime)
        {
            SpawnEnemy(Vector3.zero);
            currentTime = 0;
        }

    }

    void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject obj = enemyPool[enemyId];
        if(obj.activeSelf == true)return;

        obj.transform.position = spawnPosition;
        obj.SetActive(true);
        enemyId += 1;
        enemyId %= enemyCount;
    }

    public void ResetAllEnemies()
    {
        foreach (var item in enemyPool)
        {
            item.SetActive(false);
        }
        enemyId = 0;
    }
}
