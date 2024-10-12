using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnLocation;

    private float _spawnTimer = 3.0f;

    [SerializeField]
    private int _totalEnemies = 0;

    private void Start()
    {
        _totalEnemies = 30;
        StartCoroutine(SpawnEnemy());
        EnemyCountInitialUIUpdate();
        
    }

    IEnumerator SpawnEnemy()
    {
       while(_totalEnemies >= 0)
        {
            yield return new WaitForSeconds(_spawnTimer);
            GameObject enemy = EnemyPool.Instance.RequestEnemy();
            enemy.transform.position = _spawnLocation.transform.position;
            _spawnTimer = Random.Range(3, 5);
            
        }
    }

    public void EnemyCountInitialUIUpdate()
    {
        
        UIManager.Instance.SetEnemyCount(_totalEnemies);
    }

}
