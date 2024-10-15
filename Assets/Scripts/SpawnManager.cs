using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnLocation;

    private float _spawnTimer = 3.0f;

    [SerializeField]
    private int _totalEnemies = 0;

    [SerializeField]
    private int _enemiesInScene;

    private void Start()
    {
        
        StartCoroutine(SpawnEnemy());
        EnemyCountInitialUIUpdate();
        
    }

    private void Update()
    {
        if(_enemiesInScene <= 0 && _totalEnemies <= 0)
        {
            UIManager.Instance.Win();
        }
       
    }

    IEnumerator SpawnEnemy()
    {
       while(_totalEnemies >= 0)
        {
            yield return new WaitForSeconds(_spawnTimer);
            GameObject enemy = EnemyPool.Instance.RequestEnemy();
            enemy.transform.position = _spawnLocation.transform.position;
            _spawnTimer = Random.Range(1, 3);
            _totalEnemies--;
            _enemiesInScene++;
            
        }
    }

    public void EnemyCountInitialUIUpdate()
    {
        
        UIManager.Instance.SetEnemyCount(_totalEnemies);
    }

    public void ReduceEnemiesInScene()
    {
        _enemiesInScene--;
    }

}
