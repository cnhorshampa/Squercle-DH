using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Move {
    public Type type;
    public float dmg;

}
//public class Monster {
//    public string species;
//    public int HP;
//    public int Defense;
//    public int Spd;
//    public List <Move>  moves;
//}
[Serializable]
public class Monsterinstance {
    public int LVL;
    public int CurrentHP;
    public int AP;
    public int MaxAP;
    public float CurrentDmg;
    public MonsterBase BaseData;
    public int MaxHP;
    public int CurrentXP;
    public List<StatusEffects> Effects;
    //too many things end with P
    public Monsterinstance(int lVL, MonsterBase BaseData) {
        LVL = lVL;
        this.BaseData = BaseData;
        MaxHP = BaseData.HP + 10*(LVL-1);
        CurrentDmg = BaseData.Dmg  + 0.2f *(LVL-1);
        CurrentHP = MaxHP;
        MaxAP = 10 * LVL;
        AP = MaxAP;
        Effects = new List<StatusEffects>();
    }  
    public bool isFainted() {
        return CurrentHP <= 0;
    }

    public void ensureinit() {
        //    if (moves == null) {

        //}
    }
    public void GainXP(int XP) {
        CurrentXP += XP;
        int XPneeded = XPToNextLVL();
        if (CurrentXP > XPneeded) {
            LVL += 1;
            MaxHP = BaseData.HP + 10 * (LVL - 1);
            CurrentHP = MaxHP;
            MaxAP = 10 * LVL;
            AP = MaxAP;
            CurrentDmg = BaseData.Dmg + 0.2f * (LVL - 1);
            CurrentXP -= XPneeded;
        }
    }
    public int XPToNextLVL() {
        return (int)(Math.Pow(3, LVL) - 1);
    }
}


