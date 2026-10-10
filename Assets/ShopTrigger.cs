using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{

    public GameObject ShopUI;
    public PlayerMovement movement;
    private void OnTriggerEnter2D(Collider2D collision) {
        ShopUI.SetActive(true);
        movement.stopMove();
        movement.enabled = false;   
    }
}
