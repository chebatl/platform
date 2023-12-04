using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhase : MonoBehaviour
{
   public void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player")){
            GameManager.INSTANCE.NextLevel();
        }
   }

   private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player")){
            GameManager.INSTANCE.NextLevel();
        }
   }
}
