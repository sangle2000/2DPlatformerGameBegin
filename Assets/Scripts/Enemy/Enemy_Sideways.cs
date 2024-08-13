using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;

    private bool _movingLeft;
    private float _leftEdge;
    private float _rightEdge;

    private void Awake()
    {
        _leftEdge = transform.position.x - movementDistance;
        _rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (_movingLeft)
        {
            if (transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y,
                    transform.position.z);
            }
            else
            {
                _movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y,
                    transform.position.z);
            }
            else
            {
                _movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Heart>().TakeDamage(damage);
        }
    }
}
