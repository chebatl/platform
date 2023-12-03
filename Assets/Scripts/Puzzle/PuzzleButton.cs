using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{

    private Animator _animator;
    [SerializeField] private Animator _barrierAnimator;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float radius;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        OnCollision();
    }
    private void OnCollision(){
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        Collider2D hitInteractable = Physics2D.OverlapCircle(transform.position, radius, interactableLayer);

        if(hit != null || hitInteractable != null){
            OnPressed();
            hit = null;
        }else{
            OnExit();
        }
    }

    private void OnPressed(){
        _animator.SetBool("isPressed", true);
        _barrierAnimator.SetBool("isOpen", true);
    }
    private void OnExit(){
        _animator.SetBool("isPressed", false);
        _barrierAnimator.SetBool("isOpen", false);
    }

   //private void OnCollisionStay2D(Collision2D other) {
   //    if(other.gameObject.CompareTag("PuzzleStone")){
   //        OnPressed();
   //    }
   //}

   //private void OnCollisionExit2D(Collision2D other) {
   //    if(other.gameObject.CompareTag("PuzzleStone")){
   //        OnExit();
   //    }
   //}

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
