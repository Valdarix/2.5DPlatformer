using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using System.Linq;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private float waitAtWayPointTime = 1f;

    private float _moveTime;
    private int _currentWaypoint;
    private bool _isMoving;
    // Start is called before the first frame update
    private void Start()
    {
        _moveTime = 0;
        if (waypoints.Count > 0)
        {
            _isMoving = true;
            _currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Time.time > _moveTime)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        if (!_isMoving) return;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[_currentWaypoint].transform.position,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(waypoints[_currentWaypoint].transform.position, transform.position) <= 0)
        {
            _currentWaypoint++;
            _moveTime = Time.time + waitAtWayPointTime;
        }

        if (_currentWaypoint != waypoints.Count) return;
        waypoints.Reverse();
        _currentWaypoint = 0;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
