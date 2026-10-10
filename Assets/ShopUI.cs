using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public void OnHealsPressed() {
        if(InvintorySystem.instance.Money >= 50) {
            InvintorySystem.instance.Heals += 1;
            InvintorySystem.instance.Money-=50;
        }
        
    }
    public void OnAttackBuffPressed() {
        if (InvintorySystem.instance.Money >= 75) {
            InvintorySystem.instance.AttackBuff += 1;
            InvintorySystem.instance.Money -= 75;
        }

    }
    public void OnDefenseBuffPressed() {
        if (InvintorySystem.instance.Money >= 50) {
            InvintorySystem.instance.DefenseBuff += 1;
            InvintorySystem.instance.Money -= 50;
        }

    }
    public void OnCatcherCasePressed() {
        if (InvintorySystem.instance.Money >= 25) {
            InvintorySystem.instance.Catch += 1;
            InvintorySystem.instance.Money -= 25;
        }

    }
}
