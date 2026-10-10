using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
    public Slider XPBar;
    public Slider XPBar1;
    public Slider XPBar2;
    public Slider XPBar3;
    public Slider XPBar4;
    public Slider XPBar5;
    public Button[] healButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        if (Input.GetKeyDown(KeyCode.B)) {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        }
    }
    public Text Party;
    public Text Items;

    public void UpdateText() {
        Party.text = "Catches\n";
        foreach (Monsterinstance monster in PlayerParty.instance.team) {
            Party.text += $"{monster.BaseData.name} HP:{monster.CurrentHP}/{monster.MaxHP} LVL:{monster.LVL}\n";

        }
        Items.text = "Items\n";
        Items.text += $"Capsules:{InvintorySystem.instance.Catch}\n";
        Items.text += $"Defendse Buffs:{InvintorySystem.instance.DefenseBuff}\n";
        Items.text += $"AttackBuff:{InvintorySystem.instance.AttackBuff}\n";
        Items.text += $"Heals:{InvintorySystem.instance.Heals}\n";
        Items.text += $"Money:{InvintorySystem.instance.Money}\n";

        XPBar.value = Mathf.InverseLerp(0, PlayerParty.instance.team[0].XPToNextLVL(), PlayerParty.instance.team[0].CurrentXP);

        if (PlayerParty.instance.team.Count > 1) {
            XPBar1.enabled = true;
            healButtons[0].enabled = true;
        }
        if (PlayerParty.instance.team.Count > 2) {
            XPBar2.enabled = true;
            healButtons[1].enabled = true;
        }
        if (PlayerParty.instance.team.Count > 3) {
            XPBar3.enabled = true;
            healButtons[2].enabled = true;
        }
        if (PlayerParty.instance.team.Count > 4) {
            XPBar4.enabled = true;
            healButtons[3].enabled = true;
        }
        if (PlayerParty.instance.team.Count > 5) {
            XPBar5.enabled = true;
            healButtons[4].enabled = true;
        }
    }
}
