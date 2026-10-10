using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform exit;
   public void OnTriggerEnter2D(Collider2D collider) {
        collider.gameObject.transform.position = exit.position;
    }
  
}
