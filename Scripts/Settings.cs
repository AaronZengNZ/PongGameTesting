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
    public void OpenSettings(){
        settings.SetActive(true);
    }
    public void CloseSettings(){
        settings.SetActive(false);
    }
    public void OpenConfirmation(){
        confirmation.SetActive(true);
    }
}
