using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _canFire = -1.0f;

    [SerializeField]
    private LayerMask _column;

    [SerializeField]
    private LayerMask _enemy;

    private int _scoreCount;

    private int _enemyCount;

    [SerializeField]
    private AudioSource _fireSource;

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
        if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, _enemy | _column))
        {
            GameObject hitObject = hit.collider.gameObject;
           
            if (hitObject != null && hitObject.transform.tag == "Enemy")
            {
                AI = hitObject.GetComponent<AI>();
                PlayerScore(10);
                EnemyCount(1);
                AI.Death();
            }
            else if(hitObject != null && hitObject.transform.tag == "Column")
            {
                Debug.Log("hit Column!");
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
