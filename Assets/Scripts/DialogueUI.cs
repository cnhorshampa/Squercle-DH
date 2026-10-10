using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUI : MonoBehaviour
{

    public PlayerMovement movement;
    private void Awake() {
        Instance = this;
        panel.SetActive(false);

    }
    public void ShowDialogue(dialogueLine[] lines) {
        this.lines = lines;
        panel.SetActive(true);
        movement.enabled = false;
        Render();
        movement.stopMove();
    }
    private void Render() {
        if (Index >= lines.Length) {
            close();
            return;
        }
        Speaker.text = lines[Index].Speaker;
        text.text = lines[Index].Text;
        text.GetComponent<typewriterUI>().run();
    }
    private void next() {
        Index++;
        Render();
    }

    private void close() {
        panel.SetActive(false);
        movement.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Speaker.font.material.mainTexture.filterMode = FilterMode.Point;
        text.font.material.mainTexture.filterMode = FilterMode.Point;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z")) {
            next();
        }
    }
    public static DialogueUI Instance;
    public GameObject panel;
    private dialogueLine[] lines;
    private int Index = 0;
    public Text Speaker;
    public Text text;   
}
