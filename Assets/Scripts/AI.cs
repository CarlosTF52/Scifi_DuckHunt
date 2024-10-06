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

    private bool _inReverse = false;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        
        _waypoints[0] = GameObject.Find("StartingPoint").GetComponent<Transform>();
        _waypoints[1] = GameObject.Find("EndPoint").GetComponent<Transform>();
       

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
        CalculateAIMovenent();
    }
    private void CalculateAIMovenent()
    {
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
            _agent.SetDestination(_waypoints[_currentWaypoint].position);

            
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
}
