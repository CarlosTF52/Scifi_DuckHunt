using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody _bulletRigidBody;

    [SerializeField]
    private float _bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _bulletRigidBody = GetComponent<Rigidbody>();
        _bulletRigidBody.velocity = transform.forward * _bulletSpeed;
    }

    private void Update()
    {
        Destroy(gameObject, 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Column")
        {
            Destroy(gameObject);
        }
    }

}
