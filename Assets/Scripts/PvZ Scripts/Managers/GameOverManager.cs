using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;
    private SoundManager horrorScreamingSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        ActivateGame();
        gameOverPanel.SetActive(false);
        horrorScreamingSoundManager = GameObject.Find("HorrorScreamingSoundManager")?.GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Zombie"))
        {
            ShowGameOverPanel();
            DeactivateGame();
            horrorScreamingSoundManager?.PlaySound();
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public static void ActivateGame()
    {
        Time.timeScale = 1f;
    }

    public static void DeactivateGame()
    {
        Time.timeScale = 0f;
    }
}
