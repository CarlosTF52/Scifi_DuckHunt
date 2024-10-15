using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyedBarrel;

    [SerializeField]
    private GameObject _explosion;

    public void BlowUp()
    {
       Instantiate(_destroyedBarrel, transform.position, Quaternion.identity);
       GameObject InstantiatedExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
       Destroy(InstantiatedExplosion, 5.0f); 
       Destroy(gameObject);
       
    }
}
