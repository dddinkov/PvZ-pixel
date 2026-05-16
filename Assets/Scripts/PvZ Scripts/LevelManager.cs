using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToDeactivateAfterCardSelection;
    [SerializeField]
    private GameObject[] objectsToActivateAfterCardSelection;
    [SerializeField]
    private Text centralText;
    [SerializeField]
    private string[] textStrings;
    [SerializeField]
    private float interval;

    public void StartLevel()
    { 
        ShowText();
        foreach(GameObject gameObject in objectsToDeactivateAfterCardSelection)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in objectsToActivateAfterCardSelection)
        {
            gameObject.SetActive(true);
        }
    }

    async void ShowText()
    {
        centralText.gameObject.SetActive(true);
        foreach(string text in textStrings)
        {
            centralText.text = text;
            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(interval));
        }
        centralText.gameObject.SetActive(false);
    }
}
