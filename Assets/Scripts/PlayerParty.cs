using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour
{
    public List <Monsterinstance> team = new List <Monsterinstance> ();
    public static PlayerParty instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        
    }

    public void AddToTeam(Monsterinstance monster){
        team.Add (monster);
    }

    public void healAll() {
        foreach(Monsterinstance monster in team) {
            monster.CurrentHP = monster.BaseData.HP;
        }
    }
}
