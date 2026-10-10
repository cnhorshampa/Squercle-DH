using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    private List<Monsterinstance> monsters = new List<Monsterinstance>();
    private int Page = 0;
    private int MaxPages;

    public Button Collect1;
    public Button Collect2;
    public Button Collect3;

    public Button Store1;
    public Button Store2;
    public Button Store3;

    public Text Monster1;
    public Text Monster2;
    public Text Monster3;

    public Button[] Select;

    public void Collect(int Index) {
        if (PlayerParty.instance.team.Count == 6) return;
        if (monsters.Count < Index + Page * 3) return;

        PlayerParty.instance.team.Add(monsters[Index + Page* 3]);
        monsters.RemoveAt(Index + Page* 3);
        UpdateUI();
    }
    public void Store(int Index) {
        if (PlayerParty.instance.team.Count == 1) return;

        monsters.Add(PlayerParty.instance.team[Index]);
        PlayerParty.instance.team.Remove(PlayerParty.instance.team[Index]);

        UpdateUI();
    }
    public void Next() {
        Page = Mathf.Clamp(Page+1, 0, MaxPages);
        UpdateUI();
    }
    public void Previous() {
        Page = Mathf.Clamp(Page - 1, 0, MaxPages);
        UpdateUI();
    }
    private void UpdateUI() {
        MaxPages = (int)Mathf.Ceil(monsters.Count / 3);
        for (int i = 0; i < PlayerParty.instance.team.Count; i++) {
            //if(PlayerParty.instance.team.Count > i) {
            Select[i].interactable = true;
            Select[i].GetComponentInChildren<Text>().text = $"Store {PlayerParty.instance.team[i].BaseData.name}";

            //}
        }
        for (int i = PlayerParty.instance.team.Count; i < 6; i++) {
            Select[i].interactable = false;
            Select[i].GetComponentInChildren<Text>().text = $"";
        }

        //for(int i = Page * 3; i < Page * 3 + 3; i++) {
        if (Page * 3 < monsters.Count) {
            Monster1.text = monsters[Page * 3].BaseData.name;
            Collect1.interactable = true;
        }
        else {
            Monster1.text = "Empty";
            Collect1.interactable = false;
        }

        if (Page * 3+1 < monsters.Count) {
            Collect2.interactable = true;
            Monster2.text = monsters[Page * 3+1].BaseData.name;
        }
        else {
            Monster2.text = "Empty";
            Collect2.interactable = false;
        }

        if (Page * 3+2 < monsters.Count) {
            Collect3.interactable = true;
            Monster3.text = monsters[Page * 3+2].BaseData.name;
        }
        else {
            Monster3.text = "Empty";
            Collect3.interactable = false;
        }
        //}
    }
    public void OnEnable() {
        
    UpdateUI();
    }
}
