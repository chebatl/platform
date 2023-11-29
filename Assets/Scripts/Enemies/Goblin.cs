using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField] private float maxVision;
    [SerializeField] private bool foundPlayer;
    [SerializeField] private Transform startLook;
    [SerializeField] private bool isRight;
    private Vector2 direction;
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

    // Update is called once per frame
    void Update()
    {

    }   

    private void FixedUpdate() {
        LookForPlayer();
        if(foundPlayer){
            Move();
        }
    }

    private void Move(){
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
            if(Vector2.Distance(transform.position, hit.transform.position) <= 0.5f){
                    foundPlayer = false;
                    _rigidbody.velocity = Vector2.zero;
                    Debug.Log("encostou");
                }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startLook.position, direction * maxVision);
    }
}
