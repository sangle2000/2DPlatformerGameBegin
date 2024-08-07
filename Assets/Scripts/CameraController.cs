using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room Camera
    [SerializeField] private float speed;
    
    private float _currentPosX;
    private Vector3 _velocity = Vector3.zero;
    
    // Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float _lookAhead;
    

    private void Update()
    {
        // Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position,
        //     new Vector3(_currentPosX, transform.position.y, transform.position.z), ref _velocity, speed);
        
        // Follow player
        transform.position = new Vector3(player.position.x + _lookAhead, transform.position.y, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        _currentPosX = newRoom.position.x;
    }
}
