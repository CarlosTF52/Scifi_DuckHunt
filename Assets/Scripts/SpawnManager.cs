using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnLocation;

    private float _spawnTimer = 3.0f;

    private enum AIState
    {
        Walking,
        Hide,
        Death
    }


    private void Update() 
    {
        
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
       while(true)
        {
            yield return new WaitForSeconds(_spawnTimer);
            GameObject enemy = EnemyPool.Instance.RequestEnemy();
            enemy.transform.position = _spawnLocation.transform.position;
            _spawnTimer = Random.Range(3, 5);
        }
    }

}
