using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {
    public static BattleManager instance;
    public event Action onBattleStart;
    public event Action<bool> onBattleEnd;
    private List<Monsterinstance> playerTeam;
    private List<Monsterinstance> enemyTeam;
    private int PlayerIndex;
    private int EnemyIndex;
    private float Timer = 1.0F;
    private bool isBattleStarted = false;
    private enum Turn {
        PlayerTurn, EnemyTurn, PlayerAnimation, EnemyAnimation, BattleOver
    };
    private Turn turn;
    // Start is called before the first frame update

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    public void StartBattle(List<Monsterinstance> playerTeam, List<Monsterinstance> enemyTeam) {
        this.playerTeam = playerTeam; this.enemyTeam = enemyTeam;
        PlayerIndex = 0;
        EnemyIndex = 0;
        BattleUI.instance.gameObject.SetActive(true);
        UpdateBattle();
        BattleUI.instance.SwitchToFight();
        
        onBattleStart?.Invoke();
        turn = Turn.PlayerTurn;
        isBattleStarted = true;
        //StartCoroutine(Runbattle());

    }
    //private IEnumerator Runbattle() {
    //    while (!isBattleOver()) {
    //        if (turn == Turn.PlayerTurn) {
    //            yield return new WaitUntil(() => turn == Turn.EnemyTurn);
    //            yield return new WaitForSeconds(3f);
    //            CheckFaint();


    //            handleEffects(activeEnemy());


    //            if (activeEnemy() == null || activePlayer() == null) {
    //                continue;
    //            }

    //            ExecuteMove(activeEnemy(), activePlayer(), activeEnemy().BaseData.moves[UnityEngine.Random.Range(0, activeEnemy().BaseData.moves.Count)]);
    //            yield return new WaitForSeconds(2f);
    //            CheckFaint();
    //            turn = Turn.PlayerTurn;

    //            handleEffects(activePlayer());
    //        }

    //        //yield return null;
    //    }

    //    foreach(Monsterinstance Monster in playerTeam) {
    //        int XP = 0;
    //        foreach(Monsterinstance Enemy in enemyTeam) {
    //            XP += Enemy.MaxHP;
    //        }
    //        Monster.GainXP(XP);
    //    }

    //    onBattleEnd?.Invoke(false);
    //}

    private void Update() {
        if (isBattleStarted && !isBattleOver()) {
            if (turn == Turn.PlayerTurn) {
                if (activePlayer().Effects !=null && activePlayer().Effects.Exists(x => x is StunEffect)) {
                    // TODO: decrease stun effect
                    BattleUI.instance.LogMessage("Player was STUNNED!");
                    turn = Turn.PlayerAnimation;
                }
                CheckFaint();
                
            }
            else if (turn == Turn.EnemyTurn) {
                if (activeEnemy().Effects != null && activeEnemy().Effects.Exists(x => x is StunEffect)) {
                    // TODO: decrease stun effect
                    BattleUI.instance.LogMessage("Enemy was STUNNED!");
                    turn = Turn.EnemyAnimation;
                    return;
                }
                //ExecuteMove(activeEnemy(), activePlayer(), activeEnemy().BaseData.moves[UnityEngine.Random.Range(0, activeEnemy().BaseData.moves.Count)]);
                MoveBase move = FindEnemyMove(activeEnemy());
                if(move != null) {
                    if(activeEnemy().Effects.Exists(x =>x is ConfuseEffect) && UnityEngine.Random.Range(0, 1) == 0) {
                        ExecuteMove(activeEnemy(), activeEnemy(), move);
                        activeEnemy().AP -= move.cost;
                    }
                    else {
                        ExecuteMove(activeEnemy(), activePlayer(), move);
                        activeEnemy().AP -= move.cost;
                    }
                    
                }else {
                    ChargeAP(activeEnemy());
                }
                    
                CheckFaint();
                turn = Turn.EnemyAnimation;

                handleEffects(activePlayer());
            }else if(turn == Turn.EnemyAnimation){
                Timer -= Time.deltaTime;
                if (Timer < 0) {
                    turn = Turn.PlayerTurn;
                    Timer = 1.0F;
                }
            }
            else if(turn == Turn.PlayerAnimation) {
                Timer -= Time.deltaTime;
                if (Timer < 0) {
                    handleEffects(activeEnemy());
                    turn = Turn.EnemyTurn;
                    Timer = 1.0F;
                }
            }else if (turn == Turn.BattleOver){
                
            }
            
        }
        if (isBattleStarted && isBattleOver()) {
            if (turn == Turn.BattleOver) {
                Timer -= Time.deltaTime;
                if (Timer < 0) {
                    foreach (Monsterinstance Monster in playerTeam) {
                        int XP = 0;
                        foreach (Monsterinstance Enemy in enemyTeam) {
                            XP += Enemy.MaxHP;
                        }
                        Monster.GainXP(XP);
                        InvintorySystem.instance.Money += XP;
                    }

                    // TODO: decrease duration for player status effects.

                    isBattleStarted = false;
                    onBattleEnd?.Invoke(false);
                }
            }
            

            //Timer = 1.0F;
            turn = Turn.BattleOver;
        }

    }

    private MoveBase FindEnemyMove(Monsterinstance monster) {
        MoveBase randomMove = monster.BaseData.moves[UnityEngine.Random.Range(0, activeEnemy().BaseData.moves.Count)];
        if (monster.AP >= randomMove.cost) {
            return randomMove;
        }
        foreach(MoveBase move in monster.BaseData.moves) {
            if (monster.AP >= move.cost) {
                return move;
            }
        }
        return null;
    }

    private void handleEffects(Monsterinstance monster) {
        if (monster!=null && monster.Effects != null && !monster.isFainted()) {
            foreach (StatusEffects effects in monster.Effects) {

                effects.apply(monster);
                

                BattleUI.instance.UpdateHP(activePlayer(), activeEnemy());
            }
            monster.Effects= monster.Effects.Where(x => x.Duration >0).ToList();
        }
    }
    private bool isBattleOver() {
        bool PlayerAlive = playerTeam.Exists(P => !P.isFainted());
        if (!PlayerAlive) {
            // TODO reload
            //SceneManager.LoadScene(0);
            isBattleStarted = false;
            onBattleEnd?.Invoke(false);

        }
        bool EnemyAlive = enemyTeam.Exists(P => !P.isFainted());
        return !PlayerAlive || !EnemyAlive;
    }



    private Monsterinstance activePlayer() {
        if(PlayerIndex >= 0 && PlayerIndex < playerTeam.Count) {
            return playerTeam[PlayerIndex];
        }
        return null;
    }

    private Monsterinstance activeEnemy() {
        if (EnemyIndex >= 0 && EnemyIndex < enemyTeam.Count) {
            return enemyTeam[EnemyIndex];
            
        }
        return null;
    }


    public void OnFightPressed() {
        BattleUI.instance.SwitchToCombat();
    }
    public void OnEscapePressed() {
        if(UnityEngine.Random.Range(1, 3) == 1) {
            onBattleEnd?.Invoke(true);
        }
        else {
            BattleUI.instance.LogMessage("Failed to escape!");
        }
    }
    public void OnBagPressed() {

    }
    public void OnSwapPressed() {

    }

    public void OnBackPressed() {
        BattleUI.instance.SwitchToFight();
    }
    public void OnHealPressed(int index = -1) {
        Debug.Log(InvintorySystem.instance.Heals);
        
        if(index == -1) {
            Heal(activePlayer());
        }
        else {
            Heal(playerTeam[index]);
        }
        
    }

    public void Heal(Monsterinstance monster) {
        if (InvintorySystem.instance.Heals > 0) {
            monster.CurrentHP += 10;
            monster.CurrentHP = Math.Clamp(monster.CurrentHP, 0, monster.MaxHP);
            turn = Turn.EnemyTurn;
            InvintorySystem.instance.Heals -= 1;

        }
        else {
            BattleUI.instance.LogMessage("No heals left!");
        }
    }

    private void ExecuteMove(Monsterinstance attacker, Monsterinstance defender, MoveBase Move) {
        //Debug.Log(defender);
        //Debug.Log(attacker.CurrentDmg);
        int scale = attacker.Effects != null && attacker.Effects.Exists(x => x is DmgBoost)&& Move.dmg != 0 ? 10 : 0;
        int attack = Math.Max((int)(Move.dmg * attacker.CurrentDmg +scale) - defender.BaseData.Defense, 0);
        defender.CurrentHP -= attack;
        BattleUI.instance.UpdateHP(activePlayer(), activeEnemy());
        if (attacker != defender) {
            BattleUI.instance.LogMessage($"{attacker.BaseData.name} used {Move.name} dealing {attack} dmg to {defender.BaseData.name}");
        }
        else {
            BattleUI.instance.LogMessage($"{attacker.BaseData.name} used {Move.name} dealing {attack} dmg to themselves");
        }


        if (defender == activeEnemy()) {
            BattleUI.instance.PlayEnemyParticles();
        }
        if (defender == activePlayer()) { 
            BattleUI.instance.PlayPlayerParticles();
        }
        if(attacker == activePlayer()) {
            attacker.AP -= Move.cost;
        }
        if (Move.effects != StatusEffectType.None) {
            //defender.Effects.Add(Move.effects);
            if (defender.Effects == null) {
            defender.Effects = new List<StatusEffects>();
        }
        switch (Move.effects) {
                case StatusEffectType.Burn:
                    //Debug.Log(defender.Effects);
                    
                    //Debug.Log(defender.Effects.Count);
                    if (defender.Effects.Count<=2) {
                        defender.Effects.Add(new BurnEffect());
                    }
                    
                    break;
                case StatusEffectType.Stun:
                    if (defender.Effects.Count <= 2) {
                        defender.Effects.Add(new StunEffect());
                    }
                    break;
                case StatusEffectType.Poison:
                    Debug.Log(defender);
                    if (defender.Effects.Count <= 2) {
                        Debug.Log("applied");
                        defender.Effects.Add(new PoisonEffect());
                    }
                    break;
                case StatusEffectType.Regen:
                    if (attacker.Effects.Count <= 2) {
                        Debug.Log("applied");
                        attacker.Effects.Add(new RegenEffect());
                    }
                    break;
                case StatusEffectType.DmgBoost:
                    //if (Move.dmg == 0)
                    //    break;
                    if (attacker.Effects.Count <= 2) {
                        Debug.Log("applied");
                        attacker.Effects.Add(new DmgBoost());
                    }
                    break;
                    case StatusEffectType.Confused:
                    if (defender.Effects.Count <= 2) {
                        Debug.Log("applied confuse");
                        defender.Effects.Add(new ConfuseEffect());
                    }
                    break;
            }
        }
    }

    public void OnMovePressed(int Move) {
        if (turn != Turn.PlayerTurn) return;

        if (canusemove(activePlayer(), activePlayer().BaseData.moves[Move])) {
            if(activePlayer().Effects.Exists(x => x is ConfuseEffect)&& UnityEngine.Random.Range(0,1) == 0) {
                ExecuteMove(activePlayer(), activePlayer(), activePlayer().BaseData.moves[Move]);
                turn = Turn.PlayerAnimation;
                BattleUI.instance.UpdateAP(activePlayer(), activeEnemy());
            }
            else {
                ExecuteMove(activePlayer(), activeEnemy(), activePlayer().BaseData.moves[Move]);
                turn = Turn.PlayerAnimation;
                BattleUI.instance.UpdateAP(activePlayer(), activeEnemy());
            }
            
        }
        else {
            BattleUI.instance.LogMessage("Insufficent AP");
        }
       

    }
    public void CheckFaint() {
        if (activePlayer() != null && activePlayer().isFainted()) {
            BattleUI.instance.LogMessage($"{activePlayer().BaseData.name} Fainted");
            PlayerIndex = GetNextAlive(playerTeam);
            turn = Turn.PlayerTurn;
        }
        if (activeEnemy().isFainted()) {
            BattleUI.instance.LogMessage($"{activeEnemy().BaseData.name} Fainted");
            EnemyIndex = GetNextAlive(enemyTeam);
        }
        if (PlayerIndex != -1 && EnemyIndex != -1) {
            UpdateBattle();
        }
        if (gameObject) 
    }
    private void UpdateBattle() {
        BattleUI.instance.PlayerName.text = playerTeam[PlayerIndex].BaseData.SpeciesName;
        BattleUI.instance.EnemyName.text = enemyTeam[EnemyIndex].BaseData.SpeciesName;
        BattleUI.instance.PlayerImage.sprite = playerTeam[PlayerIndex].BaseData.image;
        BattleUI.instance.EnemyImage.sprite = enemyTeam[EnemyIndex].BaseData.image;
        BattleUI.instance.UpdateHP(activePlayer(), activeEnemy());
        BattleUI.instance.SetMoveButtons(activePlayer().BaseData.moves);
        BattleUI.instance.UpdateAP(activePlayer(), activeEnemy());
    }

    private int GetNextAlive(List<Monsterinstance> Team) {
        for (int I = 0; I < Team.Count; I++) {
            if (!Team[I].isFainted()) {
                return I;
            }
        }
        return -1;
    }
    private float CatchChance(float currentHP , float maxHP) {
        return(1-(currentHP/maxHP));
    }

    public void attemptCatch() {
        if (InvintorySystem.instance.Catch > 0) {
            float chance = CatchChance(activeEnemy().CurrentHP, activeEnemy().MaxHP);
            if (UnityEngine.Random.Range(0.0f, 1.0f) < chance) {
                PlayerParty.instance.AddToTeam(activeEnemy());
                BattleUI.instance.LogMessage($"Caught {activeEnemy().BaseData.name}");
                onBattleEnd?.Invoke(false);
            }
            else {
                BattleUI.instance.LogMessage("Failed to catch");
            }
                InvintorySystem.instance.Catch--;
        }

        
    }
    public void swap(int index) {
        PlayerIndex = index;
        UpdateBattle();
    }
    public bool canusemove(Monsterinstance monster, MoveBase move) {
        if(move.cost > monster.AP) {
            return false;
        }
        return true;
    }
    public void ChargeAP(Monsterinstance monster) {
        monster.AP += 10 * monster.LVL;
        monster.AP = Math.Clamp(monster.AP, 0, monster.MaxAP);
        UpdateBattle();
        BattleUI.instance.LogMessage($"{monster.BaseData.name} charged their AP!");
        //turn = Turn.EnemyTurn;
        if(turn == Turn.PlayerTurn) {
            turn = Turn.PlayerAnimation;
        }
        else if(turn == Turn.EnemyTurn) {
            turn = Turn.EnemyAnimation;
        }
    }

    public void ChargeCurrentPlayer() {
        ChargeAP(activePlayer());
    }

    }

