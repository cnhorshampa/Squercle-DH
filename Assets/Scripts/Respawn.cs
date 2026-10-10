using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float respawnseconds = 60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void despawn() {
        gameObject.SetActive(false);
        Invoke("respawn",respawnseconds);
    }
    public void respawn() {
        gameObject.SetActive(true);
    }
}
