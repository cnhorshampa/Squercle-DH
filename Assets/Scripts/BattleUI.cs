using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleUI : MonoBehaviour {
    public Text PlayerName;
    public Text PlayerHP;
    public Image PlayerImage;
    public Text EnemyName;
    public Text EnemyHP;
    public Text PlayerAP;
    public Text EnemyAP;
    public Image EnemyImage;
    public GameObject CombatButtons;
    public GameObject FightButtons;
    public GameObject BagButtons;
    public GameObject Transition;
    public Button[] actions;
    public Text log;
    private List<string> Messages = new List<string>();
    public PlayerMovement movement;
    public ParticleSystem enemyParticles;
    public ParticleSystem PlayerParticles;
    // Start is called before the first frame update
    void Start() {
        //Messages = new List<string>();
    }

    // Update is called once per frame
    void Update() {

    }
    public static BattleUI instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        BattleManager.instance.onBattleStart += handleBattleStart;
        BattleManager.instance.onBattleEnd += handleBattleEnd;
        movement.enabled = false;
        movement.stopMove();

    }

    private void OnDisable() {
        if(movement) {
            movement.enabled = true;
        }
    }

    private void Instance_onBattleEnd() {
        throw new System.NotImplementedException();
    }

    private void handleBattleStart() {
        Instantiate(Transition);
        Clearmessages();
        //
    }

    private void handleBattleEnd(bool isescaping) {
        gameObject.SetActive(false);
    }


    public void animationFinished() {
        gameObject.SetActive(true);

    }

    public void UpdateHP(Monsterinstance player, Monsterinstance enemy) {
        PlayerHP.text = $"{player.CurrentHP}/{player.MaxHP}";
        EnemyHP.text = $"{enemy.CurrentHP}/{enemy.MaxHP}";
    }
    public void UpdateAP(Monsterinstance player, Monsterinstance enemy) {
        PlayerAP.text = $"{player.AP}/{player.MaxAP}";
        EnemyAP.text = $"{enemy.AP}/{enemy.MaxAP}";
    }




    public void SwitchToCombat() {
        CombatButtons.SetActive(true);
        FightButtons.SetActive(false);
        BagButtons.SetActive(false);
    }
    public void SwitchToFight() {
        CombatButtons.SetActive(false);
        FightButtons.SetActive(true);
        BagButtons.SetActive(false);
    }

    public void SwitchToBag() {
        BagButtons.SetActive(true);
        CombatButtons.SetActive(false);
        FightButtons.SetActive(false);
    }

    public void OnCatchPressed() {
        BattleManager.instance.attemptCatch();
    }

    public void SetMoveButtons(List<MoveBase> Moves) {
        actions[0].GetComponentInChildren<Text>().text = Moves?[0]?.MoveName;
        if (Moves.Count >= 2) {
            actions[1].GetComponentInChildren<Text>().text = Moves?[1]?.MoveName;
            actions[1].interactable = true;
        }
        else {
            actions[1].interactable= false;
        }
        if (Moves.Count >= 3) {
            actions[2].GetComponentInChildren<Text>().text = Moves?[2]?.MoveName;
            actions[2].interactable = true;
        }
        else {
            actions[2].interactable= false;
        }
        if (Moves.Count >= 4) {
            actions[3].GetComponentInChildren<Text>().text = Moves?[3]?.MoveName;
            actions[3].interactable = true;
        }
        else {
            actions[3].interactable = false;
        }



    }

    public void Clearmessages() {
        Messages.Clear();
        log.text = "";
    }

    public void LogMessage(string message) {
        Messages.Add(message);
        if (Messages.Count > 10) {
            Messages.RemoveAt(0);
        }
        //log.text += message + "\n";
        log.text = string.Join("\n", Messages.ToArray());
    }
    public void PlayEnemyParticles() {
        var e = enemyParticles.emission;
        e.enabled = true;
        enemyParticles.Play();

    }
    public void PlayPlayerParticles() {
        var e = PlayerParticles.emission;
            e.enabled = true;
        PlayerParticles.Play();


    }
}
