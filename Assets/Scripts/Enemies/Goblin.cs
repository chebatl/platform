using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField] private float maxVision;
    [SerializeField] private bool foundPlayer;
    [SerializeField] private Transform startLook;
    [SerializeField] private Transform behindGoblin;
    [SerializeField] private float behindVision;
    [SerializeField] private bool isRight;
    private Vector2 direction;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        if(isRight){
            transform.eulerAngles = new Vector2(0,0);
            direction = Vector2.right;
        }else{
            transform.eulerAngles = new Vector2(0,180);
            direction = Vector2.left;
        }
    }

    private void Update() {
        if(!isAttacking && !isMoving){
            _animator.SetInteger("Transition", 0);
        }

    }
    private void FixedUpdate() {
        LookForPlayer();
        if(foundPlayer && !isAttacking){
            Move();
            _animator.SetInteger("Transition",1);
        }
    }

    private void Move(){
        isMoving = true;
        if(isRight){
            transform.eulerAngles = new Vector2(0,0);
            direction = Vector2.right;
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        }else{
            transform.eulerAngles = new Vector2(0,180);
            direction = Vector2.left;
            _rigidbody.velocity = new Vector2(-speed, _rigidbody.velocity.y);
        }
    }

    void LookForPlayer(){
        RaycastHit2D hit = Physics2D.Raycast(startLook.position, direction, maxVision);
        if(hit.collider != null){
            if(hit.transform.CompareTag("Player")){
                foundPlayer = true;
            }
            if(Vector2.Distance(transform.position, hit.transform.position) <= 0.5f)
            {
                _rigidbody.velocity = Vector2.zero;
                isMoving = false;
                if(!isAttacking){
                    Attack(hit);
                }
            }
        }

        RaycastHit2D behindHit = Physics2D.Raycast(behindGoblin.position, -direction, maxVision);
        if(behindHit.collider != null){
            if(behindHit.transform.CompareTag("Player")){
                isRight = !isRight;
                foundPlayer = true;
            }
        }
    }

    private void Attack(RaycastHit2D hit)
    {
        isAttacking = true;
        PlayerController playerController = hit.collider.GetComponent<PlayerController>();
        if (!playerController.isDead){
            foundPlayer = false;
            playerController.EnemyHit(damage);
            _animator.SetInteger("Transition", 2);
            StartCoroutine("WaitAttackAnimation");
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startLook.position, direction * maxVision);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(behindGoblin.position, direction * maxVision);
    }

    IEnumerator WaitAttackAnimation(){
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
