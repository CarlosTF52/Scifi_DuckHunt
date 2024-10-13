using System.Collections;
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
        
        StartCoroutine(SpawnEnemy());
        EnemyCountInitialUIUpdate();
        
    }

    private void Update()
    {
        if(_totalEnemies <= 0)
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
            _spawnTimer = Random.Range(3, 5);
            _totalEnemies--;
            
        }
    }

    public void EnemyCountInitialUIUpdate()
    {
        
        UIManager.Instance.SetEnemyCount(_totalEnemies);
    }

}
