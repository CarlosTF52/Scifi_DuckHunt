using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _gunSpark;

    [SerializeField]
    private GameObject _bloodBurst;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1.0f;

    [SerializeField]
    private LayerMask _environment;

    [SerializeField]
    private LayerMask _enemy;

    [SerializeField]
    private LayerMask _explosives;

    private int _scoreCount;

    private int _enemyCount;

    [SerializeField]
    private AudioSource _fireSource;

    [SerializeField]
    private AudioSource _bulletRicochet;

    [SerializeField]
    private ExplosiveBarrel _explosiveBarrels;

    private AI AI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
        {
            Fire();
        }

        
    }

    private void Fire()
    {
        _canFire = Time.time + _fireRate;
        _fireSource.Play();
        Vector2 _crosshairPos = Mouse.current.position.ReadValue();
        Ray rayOrigin = Camera.main.ScreenPointToRay(_crosshairPos);
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, _enemy | _environment | _explosives))
        {
            GameObject hitObject = hit.collider.gameObject;
           
            if (hitObject != null && hitObject.layer == 7)
            {
                AI = hitObject.GetComponent<AI>();
                PlayerScore(10);
                EnemyCount(1);
                AI.Death();
                GameObject bloodBurstInstantiated = Instantiate(_bloodBurst, hit.point, Quaternion.identity);
                Destroy(bloodBurstInstantiated, 0.5f);
            }
            else if(hitObject != null && hitObject.layer == 8)
            {
                GameObject gunsparksInstantiated = Instantiate(_gunSpark, hit.point, Quaternion.identity);
                Destroy(gunsparksInstantiated, 0.4f);
                _bulletRicochet.Play();
            }
            else if (hitObject != null && hitObject.layer == 9)
            {
               _explosiveBarrels = hitObject.GetComponent<ExplosiveBarrel>();
               _explosiveBarrels.BlowUp();
            }


        }
       
        GameObject muzzleFlashInstantiated = Instantiate(_muzzleFlash, _firePoint.position, _firePoint.transform.rotation);
        Destroy(muzzleFlashInstantiated, 1.0f);
    }

    private void PlayerScore(int score)
    {
        _scoreCount = _scoreCount + score;
        UIManager.Instance.UpdateScore(_scoreCount);
    }

    private void EnemyCount(int enemyCount)
    {
        _enemyCount = _enemyCount - enemyCount;
        UIManager.Instance.UpdateEnemyCount(_enemyCount);
    }



}
