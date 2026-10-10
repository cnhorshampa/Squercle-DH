using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMonster", menuName = "Monster/Species")]
public class MonsterBase : ScriptableObject {
    public string SpeciesName;
    public int HP;
    public int Defense;
    public int Spd;
    public float Dmg=1.0f;
    public List<MoveBase> moves;
    public Sprite image;
}

