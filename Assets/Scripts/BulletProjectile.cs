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
        _bulletRigidBody.velocity = Vector3.forward * _bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
