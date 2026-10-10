using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Type {
    Grass,
    Ice,
    Fire,
    Physical,
    Mental,
}

public enum StatusEffectType {
    None,
    Burn,
    Stun,
    Poison,
    Regen,
    DmgBoost,
    Confused,
}

[CreateAssetMenu(fileName = "NewMove", menuName = "Monster/Move")]
public class MoveBase : ScriptableObject {
    public string MoveName;
    public Type type;
    public int dmg;
    public int cost;
    public StatusEffectType effects;

}

