using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
   

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private List<GameObject> _enemyPool;

    private static EnemyPool _instance;

    [SerializeField]
    GameObject _enemyContainer;

    public static EnemyPool Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Spawn Manager Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;

    }

    List<GameObject> SpawnEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
            
        }
        return _enemyPool;
    }

    private void Start()
    {
        _enemyPool = SpawnEnemies(10);
    }

    public GameObject RequestEnemy()
    {
        foreach (var enemy in _enemyPool)
        {
            if(enemy.activeInHierarchy == false)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }

}
