using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    private Rigidbody2D _playerRigidBody;
    private Animator _anim;
    private BoxCollider2D _boxCollider;

    private float _wallJumpCooldown;
    private float _horizontalInput;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        
        //Flip player when moving left-right
        if (_horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } 
        
        else if (_horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        //Set animator parameters
        _anim.SetBool("run", _horizontalInput != 0);
        _anim.SetBool("grounded", IsGrounded());
        
        // Wall jump logic
        if (_wallJumpCooldown > 0.2f)
        {
            _playerRigidBody.velocity = new Vector2(_horizontalInput * speed, _playerRigidBody.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                // Check if player jump on wall, he will be stuck in it and can't fall down
                _playerRigidBody.gravityScale = 0;
                _playerRigidBody.velocity = Vector2.zero;
            }
            else
            {
                // Set gravity to fall down
                _playerRigidBody.gravityScale = 7;
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, jumpPower);
        }
        else if (OnWall())
        {
            if (_horizontalInput == 0)
            {
                _playerRigidBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
            }
            else
            {
                _playerRigidBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            _wallJumpCooldown = 0;
        }
    }

    private bool IsGrounded()
    {
        // Check Raycast has been over than intersection distance by box
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return _horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}
