using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;

    [SerializeField]
    private GameObject _spawnLocation;

    [SerializeField]
    private GameObject _enemyPrefab;

    private void Start()
    {
        StartSpawning();
    }

    public static SpawnManager Instance
    {
        get
        {
            if(_instance == null)
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

    public void StartSpawning()
    {
        Instantiate(_enemyPrefab, _spawnLocation.transform.position, Quaternion.identity);
    }

}
