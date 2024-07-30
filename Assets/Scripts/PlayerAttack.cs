using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

    private Animator _anim;
    private PlayerMovement _playerMovement;
    private float _coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _coolDownTimer > attackCoolDown && _playerMovement.CanAttack())
        {
            Attack();
        }

        _coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _anim.SetTrigger("attack");
        _coolDownTimer = 0;
        
        // Pooling FireBall
        fireBalls[FindFireBall()].transform.position = firePoint.position;
        fireBalls[FindFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
