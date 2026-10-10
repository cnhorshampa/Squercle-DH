using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1);

    }
    public void HowToPlay() {
        SceneManager.LoadScene(2);
    }
    public void ReturnToMain() {
        SceneManager.LoadScene(0);
    }
    public void Credits() {
        SceneManager.LoadScene(3);
    }

}
