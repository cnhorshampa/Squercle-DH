using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class dialogueLine {
    public string Speaker;
    [TextArea]
    public string Text;
}

public class NPCDialogue : MonoBehaviour
{
    public dialogueLine[] lines;

    public void intereract() {
        DialogueUI.Instance.ShowDialogue(lines);
        
    }
    private void OnTriggerEnter2D(Collider2D Col) {
        intereract();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
