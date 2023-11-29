using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private float wallRadius;
    [SerializeField] private Transform wallPoint;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask layerPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(speed > 0 ){
            transform.eulerAngles = new Vector3(0,180,0);
        }
        if(speed < 0 ){
            transform.eulerAngles = new Vector3(0,0,0);
        }
        OnWallCollision();
    }

    private void FixedUpdate() {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }

    private void OnWallCollision(){
        Collider2D hit = Physics2D.OverlapCircle(wallPoint.position, wallRadius, layerGround);
        if(hit != null){
            speed *= -1;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallPoint.position, wallRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

}
