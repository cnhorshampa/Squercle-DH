using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransition : MonoBehaviour
{
    public void Awake() {
        //Invoke("ShowBattle", 0.2f);
    }

    public void ShowBattle() {
        BattleUI.instance.animationFinished();
    }

    public void AnimationFinished() {   
        gameObject.SetActive(false);
    }
}
