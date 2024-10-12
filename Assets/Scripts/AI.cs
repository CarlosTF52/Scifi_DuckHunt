using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _waypoints;

    [SerializeField]
    private int _currentWaypoint;

    [SerializeField]
    private bool _inReverse = false;

    private NavMeshAgent _agent;

    [SerializeField]
    private List<GameObject> hidingSpotsList = new List<GameObject>();

    [SerializeField]
    private Transform _nearestHidingSpot;

    [SerializeField]
    private Transform _cachedWaypoint;

    [SerializeField]
    private float _seekHidespot;

    [SerializeField]
    private float _hideTime = 3.0f;

    [SerializeField]
    private bool _wasHiding;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int _enemyCount;

    private enum AIState
    {
        Walking,
        Hiding,
        Death
    }

    [SerializeField]
    private AIState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _waypoints[0] = GameObject.Find("StartingPoint").GetComponent<Transform>();
        _waypoints[1] = GameObject.Find("EndPoint").GetComponent<Transform>();
        _enemyCount = _enemyCount + 1;
        _seekHidespot = Random.Range(3, 5);


        _agent = GetComponent<NavMeshAgent>();
        var _randomWaypoint = Random.Range(0, _waypoints.Count);

        if (_agent != null)
        {
            _agent.destination = _waypoints[_currentWaypoint].position;
        }

    }



    // Update is called once per frame
    void Update()
    {
        FillHideSpots(18);

        
       
        switch (_currentState)
        {
            case AIState.Walking:
                CalculateAIMovenent();
                break;
            case AIState.Hiding:
                Hiding();
                break;
            case AIState.Death:
                           
                break;
        }
    }
    private void CalculateAIMovenent()
    {
        _animator.SetBool("Walking", true);
        _animator.SetBool("Hiding", false);
        _agent.speed = 3.0f;
        _agent.acceleration = 8.0f;
        _seekHidespot -= Time.deltaTime;
        if (_agent.remainingDistance < 0.5f)
        {
            if (_inReverse)
            {
                Reverse();
            }
            else
            {
                Forward();
            }
            if (!_wasHiding)
            {
                _agent.SetDestination(_waypoints[_currentWaypoint].position);
                _cachedWaypoint = _waypoints[_currentWaypoint].transform;
            }
            else
            {
                _agent.SetDestination(_cachedWaypoint.position);
                _wasHiding = false;
            }           
            
        }

        if(_seekHidespot <= 0)
        {
            _currentState = AIState.Hiding;
            _seekHidespot = Random.Range(3, 5);
        }

    }
    private void Forward()
    {
        if (_currentWaypoint == _waypoints.Count - 1)
        {
            _inReverse = true;
            _currentWaypoint--;
        }
        else
        {
            _currentWaypoint++;
        }

    }

    private void Reverse()
    {
        if (_currentWaypoint == 0)
        {
            _inReverse = false;
            _currentWaypoint++;
        }
        else
        {
            _currentWaypoint--;
        }
    }

    private void FillHideSpots(int _hideSpots)
    {
        hidingSpotsList.Clear();

        for (int i = 0; i < _hideSpots; i++)
        {
            GameObject spot = GameObject.Find("HidingSpot" + i);
            if (spot != null)
            {
                hidingSpotsList.Add(spot);
            }
        }

        

    }

   
    
    private void Hiding()
    {
        _agent.SetDestination(_nearestHidingSpot.position);
        _agent.speed = 7.0f;
        _agent.acceleration = 16.0f;
        _hideTime -= Time.deltaTime;
        _wasHiding = true;
        _animator.SetBool("Walking", false);
        _animator.SetBool("Hiding", true);
              
        if (_hideTime <= 0)
        {
            _currentState = AIState.Walking;
            _hideTime = Random.Range(3f, 6f);
        }
        
    }

    public void Death()
    {
        _animator.SetTrigger("Die");
        _agent.isStopped = true;
        Invoke("Recycle", 3);       
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Bullet")
        {
            
        }

        if(other.transform.tag == "HideSpot")
        {
            _nearestHidingSpot = other.transform;
        }
    }

    void Recycle()
    {
        gameObject.SetActive(false);
        _currentState = AIState.Walking;
       gameObject.GetComponent<Collider>().enabled = true;
    }

}
