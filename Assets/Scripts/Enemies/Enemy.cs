using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    [SerializeField] private int health;
    [SerializeField] protected float speed;
    public int damage;

    public void TakeDamage(int value){
        _animator.SetTrigger("Hit");
        health -= value;

        if(health <= 0){
            _animator.SetTrigger("Dead");
            _rigidbody.bodyType = RigidbodyType2D.Static;
            Destroy(gameObject, 0.8f);
        }
    }
}
