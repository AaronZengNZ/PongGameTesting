using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Settings : MonoBehaviour
{
    public GameObject settings;
    public GameObject confirmation;
    public bool MachineLearning = false;
    public DialogText dt;
    public string[] dialogs;
    public GameObject exitButton;

    void Start(){
        if(dialogs.Length == 0){
        }
        else if(MachineLearning == false){
            dt.TypeText(dialogs[0]);
            exitButton.SetActive(false);
        }
    }
    public void OpenSettings(){
        settings.SetActive(true);
    }
    public void CloseSettings(){
        settings.SetActive(false);
    }
    public void CloseConfirmation(){
        exitButton.SetActive(true);
    }
    public void OpenConfirmation(){
        confirmation.SetActive(true);
    }
}
