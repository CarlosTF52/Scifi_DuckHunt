using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    [SerializeField]
    private int _barrierHealth = 10;

    private void Update()
    {
 
    }

    public void ReduceHealth()
    {
        _barrierHealth--;
        if(_barrierHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
