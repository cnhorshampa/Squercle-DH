using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvintorySystem : MonoBehaviour
{
    public static InvintorySystem instance;
    public int Heals = 5;
    public int Catch = 5;
    public int AttackBuff = 0;
    public int DefenseBuff = 0;
    public int Money = 0;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
