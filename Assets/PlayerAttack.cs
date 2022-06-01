using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    // Debug.Log("HIT");
    if (other.CompareTag("Player"))
    {
      Debug.Log("HIT PLAYER");
    }
  }

}
