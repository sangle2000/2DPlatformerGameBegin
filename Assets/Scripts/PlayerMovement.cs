using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _playerRigidBody;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _playerRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _playerRigidBody.velocity.y);

        if (Input.GetKey(KeyCode.Space))
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, speed);
        }
    }
}
