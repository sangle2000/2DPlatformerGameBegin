using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private bool _hit;
    private float _direction;
    private float _lifeTime;
    
    private BoxCollider2D _boxCollider;
    private Animator _anim;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_hit) return;

        float movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);

        _lifeTime += Time.deltaTime;
        if (_lifeTime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _hit = true;
        _boxCollider.enabled = false;
        _anim.SetTrigger("explode");
    }

    public void SetDirection(float direction)
    {
        _lifeTime = 0;
        _direction = direction;
        gameObject.SetActive(true);
        _hit = false;
        _boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
