using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GameManager : MonoBehaviour
{
    public GameObject InfoBox;

    private void Start()
    {
        //OpenInfoBox(false);
    }

    /// <summary>
    /// Display the info in the screen to explain details about an object
    /// </summary>
    /// <param name="toOpen"></param>
    public void OpenInfoBox(bool toOpen)
    {
        //TODO : Add some animation
        //InfoBox.SetActive(toOpen);
        if (toOpen)
        {
            LeanTween.moveY(InfoBox, 0, 0.5f).setEaseOutBounce();
        }
        else
        {
            LeanTween.moveY(InfoBox, -374, 0.5f);
        }
    }

    /// <summary>
    /// Display the button that will open the InfoBox when an image is tracked
    /// </summary>
    /// <param name="toOpen"></param>
    public void DisplayInfoButton(bool toOpen)
    {
        //TODO : Display info button with animation
    }
}
