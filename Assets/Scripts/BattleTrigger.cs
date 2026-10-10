using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monsterinstance;

public class BattleTrigger : MonoBehaviour {
    public List<MonsterBase> Monsters;
    public List<int> Levels;
    public Respawn respawn;

    // Start is called before the first frame update
    void Start() {
        respawn = GetComponent<Respawn>();
    }

    // Update is called once per frame
    void Update() {

    }
    public void StartBattle() {
        //    movement.enabled = false;
        //movement.stopMove();
        var enemyTeam = new List<Monsterinstance>();
        for (int i = 0; i < Monsters.Count; i++) {
            enemyTeam.Add(new Monsterinstance(Levels[i], Monsters[i]));
        }
        BattleManager.instance.StartBattle(PlayerParty.instance.team, enemyTeam);

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        BattleManager.instance.onBattleEnd += onBattleEnd;
        StartBattle();

    }
    private void onBattleEnd(bool isescaping) {
        BattleManager.instance.onBattleEnd -= onBattleEnd;
        if (!isescaping) {
            if (respawn){
                respawn.despawn();
            }
            else {
                Destroy(gameObject);
            }

        }
    }
}