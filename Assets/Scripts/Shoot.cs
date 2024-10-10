using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1.0f;

    private AI _enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
        {
            Fire();
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray rayOrigin = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit))
        {
            if(hit.transform.tag == "Enemy")
            {
                _enemy = hit.transform.GetComponent<AI>();
                _enemy.HideStateChange();
            }
        }
    }

    private void Fire()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.transform.rotation);
        GameObject muzzleFlashInstantiated = Instantiate(_muzzleFlash, _firePoint.position, _firePoint.transform.rotation);
        Destroy(muzzleFlashInstantiated, 1.0f);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_firePoint.transform.position, Vector3.forward, Color.red);
    }



}
