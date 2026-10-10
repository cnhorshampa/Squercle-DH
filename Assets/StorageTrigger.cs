using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTrigger : MonoBehaviour
{
    public GameObject StorageUI;
    public PlayerMovement movement;

    private void OnTriggerEnter2D(Collider2D collision) {
        StorageUI.SetActive(true);
        movement.stopMove();
        movement.enabled = false;
    }

}
