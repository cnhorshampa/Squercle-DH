using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monsterinstance;

public class BattleTrigger : MonoBehaviour {
    public List<MonsterBase> Monsters;
    public List<int> Levels;
    public Respawn respawn;
    public GameObject player;

    // Start is called before the first frame update
    void Start() {
        respawn = GetComponent<Respawn>();
        player = GameObject.FindWithTag("Player");
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
    private void onBattleEnd(int status) {
        BattleManager.instance.onBattleEnd -= onBattleEnd;
        //if (!isescaping) {
        //    if (respawn){
        //        respawn.despawn();
        //    }
        //    else {
        //        Destroy(gameObject);
        //    }

        //}
        if (status == 0) {
            if (respawn) {
                respawn.despawn();
            }
            else {
                Destroy(gameObject);
            }
        }
        else if (status == 2) {
            player.transform.position = new Vector3(517, 310, 0);
            PlayerParty.instance.healAll();
        }

     }
}