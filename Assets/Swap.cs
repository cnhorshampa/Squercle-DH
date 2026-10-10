using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swap : MonoBehaviour
{
    public Button Monster1;
    public Button Monster2;
    public Button Monster3;
    public Button Monster4;
    public Button Monster5;
    public Button Monster6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable() {
        Monster1.GetComponentInChildren<Text>().text = PlayerParty.instance.team[0]?.BaseData.name;
        if(PlayerParty.instance.team.Count >= 2) {
            Monster2.GetComponentInChildren<Text>().text = PlayerParty.instance.team[1]?.BaseData.name;
            Monster2.interactable = true;
        }
        else {
            Monster2.interactable = false;
        }
        if (PlayerParty.instance.team.Count >= 3) {
            Monster3.GetComponentInChildren<Text>().text = PlayerParty.instance.team[2]?.BaseData.name;
            Monster3.interactable = true;
        }
        else {
            Monster3.interactable = false;
        }
        if (PlayerParty.instance.team.Count >= 4) {
            Monster4.GetComponentInChildren<Text>().text = PlayerParty.instance.team[3]?.BaseData.name;
            Monster4.interactable = true;
        }
        else {
            Monster4.interactable = false;
        }
        if (PlayerParty.instance.team.Count >= 5) {
            Monster5.GetComponentInChildren<Text>().text = PlayerParty.instance.team[4]?.BaseData.name;
            Monster5.interactable = true;
        }
        else {
            Monster5.interactable = false;
        }
        if (PlayerParty.instance.team.Count >= 6) {
            Monster6.GetComponentInChildren<Text>().text = PlayerParty.instance.team[5]?.BaseData.name;
            Monster6.interactable = true;
        }
        else {
            Monster6.interactable = false;
        }

    }
}
