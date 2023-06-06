using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Update : MonoBehaviour
{
    public GameObject settings;
    public GameObject updating;
    public GameObject otherSettings;
    public TextMeshProUGUI updatingText;
    public TextMeshProUGUI secondaryText;
    public Image updatingImage;
    public string[] updateEffects;
    public string[] updateFinishDialogs;
    string currentUpdate;
    public float typeSpeed = 24f;
    public float updatingLoop = 0f;
    float currentUpdateNum = 0f;
    public AiPaddle ap;
    public DialogText dt;
    public void UpdateGame(float updateNum){
        settings.SetActive(true);
        updating.SetActive(true);
        otherSettings.SetActive(false);
        currentUpdate = updateEffects[(int)updateNum - 1];
        currentUpdateNum = updateNum;
        StartCoroutine(UpdateGameCoroutine());
    }

    IEnumerator UpdateGameCoroutine(){
        //using a for loop, type out secondaryText
        secondaryText.text = "";
        updatingText.color = new Color(updatingText.color.r, updatingText.color.g, updatingText.color.b, 1f);
            secondaryText.color = new Color(secondaryText.color.r, secondaryText.color.g, secondaryText.color.b, 1f);
            updatingImage.color = new Color(updatingImage.color.r, updatingImage.color.g, updatingImage.color.b, 1f);
        for(int i = 0; i < currentUpdate.Length; i++){
            updatingLoop++;
            if(updatingLoop <= 10f){
                updatingText.text = "Please wait, changes are being made to the scripts.";
            }
            else if(updatingLoop <= 20f){
                updatingText.text = "Please wait, changes are being made to the scripts..";
            }
            else if(updatingLoop <= 30f){
                updatingText.text = "Please wait, changes are being made to the scripts...";
            }
            else if(updatingLoop > 30f){
                updatingLoop = 1f;
            }
            secondaryText.text += currentUpdate[i];
            yield return new WaitForSeconds(1f / typeSpeed);
        }
        if(currentUpdateNum == 1f){
            ap.SpeedIncrease();
            dt.TypeText(updateFinishDialogs[0]);
        }
        yield return new WaitForSeconds(1.75f);
        for(int i = 0; i < 100; i++){
            updatingText.color = new Color(updatingText.color.r, updatingText.color.g, updatingText.color.b, updatingText.color.a - 0.01f);
            secondaryText.color = new Color(secondaryText.color.r, secondaryText.color.g, secondaryText.color.b, secondaryText.color.a - 0.01f);
            updatingImage.color = new Color(updatingImage.color.r, updatingImage.color.g, updatingImage.color.b, updatingImage.color.a - 0.01f);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0.25f);
        settings.SetActive(false);
        updating.SetActive(false);
        otherSettings.SetActive(true);
    }
}
