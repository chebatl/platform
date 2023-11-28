using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private bool onGround;
    [SerializeField] private bool doubleJump;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    
    private void FixedUpdate() {
        Move();
    }

    private void Move(){
        float direction = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(direction * speed, _rigidbody.velocity.y);
        if(direction < 0){
            transform.eulerAngles = new Vector3(0,180,0);
            if(onGround)
                _animator.SetInteger("Transition", 1);
        }
        if(direction > 0 ){
            transform.eulerAngles = new Vector3(0,0,0);
            if(onGround)
                _animator.SetInteger("Transition", 1);
        }
        if(direction == 0 && onGround){
            _animator.SetInteger("Transition", 0);
        }
    }

    private void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(onGround){
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
            _animator.SetInteger("Transition", 2);
            }else if(doubleJump){
                doubleJump = false;
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _animator.SetInteger("Transition", 2);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 3){
            onGround = true;
            doubleJump = true;
        }
    }
}
