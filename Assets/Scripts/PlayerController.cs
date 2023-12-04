using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private bool onGround;
    [SerializeField] private bool doubleJump;
    [SerializeField] private Transform attackArea;
    [SerializeField] private float attackRadius;
    [SerializeField] private bool isAttacking;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float recoveryTime;
    private float recoveryTimeCount = 0;
    public bool isDead {get; private set;}

    private PlayerAudio playerAudio;
    private HealthBar heatlhBar;
    private bool recovery;

    private static PlayerController player;

    private void Awake() {
        if(player == null){
            player = this;
            DontDestroyOnLoad(this);
        }else if(player != this){
            Destroy(player.gameObject);
            player = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<PlayerAudio>();
        heatlhBar = GetComponent<HealthBar>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }
    
    private void FixedUpdate() {
        Move();
    }

    private void Move(){
        float direction = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(direction * speed, _rigidbody.velocity.y);
        if(direction < 0){
            transform.eulerAngles = new Vector3(0,180,0);
            if(onGround && !isAttacking){
                _animator.SetInteger("Transition", 1);
            }
        }
        if(direction > 0 && !isAttacking){
            transform.eulerAngles = new Vector3(0,0,0);
            if(onGround){
                _animator.SetInteger("Transition", 1);
            }
        }
        if(direction == 0 && onGround && !isAttacking){
            _animator.SetInteger("Transition", 0);
        }
    }

    private void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(onGround){
            playerAudio.PlaySFX(playerAudio.jumpSound);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            onGround = false;
            _animator.SetInteger("Transition", 2);
            }else if(doubleJump){
                playerAudio.PlaySFX(playerAudio.jumpSound);
                doubleJump = false;
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _animator.SetInteger("Transition", 2);
            }
        }
    }

    private void Attack(){

        if(!isAttacking && Input.GetButtonDown("Fire1")){
            isAttacking = true;
            playerAudio.PlaySFX(playerAudio.attackSound);
            Collider2D hit = Physics2D.OverlapCircle(attackArea.position, attackRadius, enemyLayer);
            _animator.SetInteger("Transition", 3);
            if(hit != null){
                hit.GetComponent<Enemy>().TakeDamage(damage);
            }
            StartCoroutine("OnAttack");
        }
    }

    private int TakeDamage(int value){
        _animator.SetTrigger("Hit");
        heatlhBar.health -= value;
        return heatlhBar.health;
    }

    IEnumerator OnAttack(){
        yield return new WaitForSeconds(0.33333f);
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 3){
            onGround = true;
            doubleJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 7){
            EnemyHit(other.GetComponent<Enemy>().damage);
        }
        if(other.CompareTag("Coin")){
            playerAudio.PlaySFX(playerAudio.coinSound);
            GameManager.INSTANCE.GetCoin();
            other.GetComponent<Animator>().SetTrigger("Collected");
            Destroy(other.gameObject, 0.5f);
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }

    public void EnemyHit(int value){
       

        if(!recovery){
            int life = TakeDamage(value);
            if(life <= 0 && !isDead){
                isDead = true;
                recovery = true;
                _animator.SetTrigger("Dead");
                GameManager.INSTANCE.ShowGameOver();
                // Destroy(gameObject, 1f);
            }
        }else{
            StartCoroutine(Recovery());
        }
    }

    IEnumerator Recovery(){
        recovery = true;
        yield return new WaitForSeconds(3f);
        recovery = false;
    }
}
