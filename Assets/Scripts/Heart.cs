using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Heart : MonoBehaviour
{
    [SerializeField] private float startingHeart;

    public float CurrentHeart { get; private set; }

    private Animator _anim;
    private bool _dead;

    private void Awake()
    {
        CurrentHeart = startingHeart;
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHeart = Mathf.Clamp(CurrentHeart - damage, 0, startingHeart);
        
        if (CurrentHeart > 0)
        {
            _anim.SetTrigger("hurt");
            // iframes
        }
        else
        {
            if (!_dead)
            {
                _anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                _dead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        CurrentHeart = Mathf.Clamp(CurrentHeart + value, 0, startingHeart);
    }
}
