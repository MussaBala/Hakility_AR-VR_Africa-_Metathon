using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject WelcomeMessageCanvas;
    public GameObject GameOverMessageCanvas;
    public GameObject HintsCanvas;
    public TextMeshProUGUI HintsText;

    [Space]

    public NarratorManager Narrator;

    [Space]

    public Animator TreasureChestAnimatorController;

    bool hasGameStarted;

    private void Awake()
    {
        instance = this;

        WelcomeMessageCanvas.SetActive(false);
        HintsCanvas.SetActive(false);
    }

    public void StartGame()
    {
        if (!hasGameStarted)
        {
            //Display welcome message
            WelcomeMessageCanvas.SetActive(true);
            hasGameStarted = true;
        }
    }

    public void DisplayHints(string hint, bool display)
    {
        if(display)
        {
            HintsCanvas.SetActive(true);
            HintsText.text = hint;
        }
    }

    public void StartNarrator()
    {
        WelcomeMessageCanvas.SetActive(false);
        Narrator.StartNarration();
    }

    public void DisplayGameOverMessage()
    {
        GameOverMessageCanvas.SetActive(true);
        HintsCanvas.SetActive(false);

        Destroy(GameOverMessageCanvas, 3);

        Invoke("startChestAnimation", 3);
    }

    void startChestAnimation()
    {
        TreasureChestAnimatorController.SetTrigger("Reveal");
    }
}