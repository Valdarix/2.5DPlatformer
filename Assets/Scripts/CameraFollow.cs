using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private float _deadZone = 0.15f;

    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = player.transform.position;
        playerPos.z = transform.position.z;
        if (Vector3.Distance(playerPos, player.transform.position) > _deadZone)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos + _offset, _speed * Time.deltaTime);
        }
    }
}
