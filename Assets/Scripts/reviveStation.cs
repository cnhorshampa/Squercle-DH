using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviveStation : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) {
        foreach (Monsterinstance monster in PlayerParty.instance.team) {
            monster.CurrentHP = monster.MaxHP;
        }
    }
}
