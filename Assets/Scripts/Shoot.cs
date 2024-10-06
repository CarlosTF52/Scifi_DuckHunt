using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private Transform _aimLaser;
    [SerializeField]
    private LayerMask _aimColliderLayerMask = new LayerMask();

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _firePoint;

    private Vector3 _mouseWorldPosition;

    private void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2); 
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
        {
            _mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    private void Fire()
    {

      Vector3 aimDirection = (_mouseWorldPosition - _firePoint.position).normalized;
      Instantiate(_bulletPrefab, _firePoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));

    }

}
