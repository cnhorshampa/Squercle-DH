using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StatusEffects
{
    public int Duration = 2;
    public virtual void apply(Monsterinstance monster) {
        decreaseduration();
    }

    public void decreaseduration() {
        Duration -= 1;
    }

}
public class RegenEffect : StatusEffects {

    public override void apply(Monsterinstance monster) {
        monster.CurrentHP += 3;
        decreaseduration();
        BattleUI.instance.LogMessage($"{monster.BaseData.name} was healed for 3 dmg!");
    }
}
public class DmgBoost : StatusEffects {
    public DmgBoost() {
        Duration = 2;
    }
    public override void apply(Monsterinstance monster) {
        decreaseduration();
    }
}

public class BurnEffect : StatusEffects {
    public override void apply(Monsterinstance monster) {
        monster.CurrentHP -= 3;
        decreaseduration();
        BattleUI.instance.LogMessage($"{monster.BaseData.name} burned for 3 dmg!");
    }
}
public class StunEffect : StatusEffects {
    public override void apply(Monsterinstance monster) {
        decreaseduration();
    }
}
public class PoisonEffect : StatusEffects {
    public PoisonEffect() {
        Duration = 4;
    }

    public override void apply(Monsterinstance monster) {
        monster.CurrentHP -= 2;
        decreaseduration();
        BattleUI.instance.LogMessage($"{monster.BaseData.name} Poisoned for 2 dmg!");
    }
}
public class ConfuseEffect : StatusEffects {
    public ConfuseEffect() {
        Duration = 2;
    }
    
}