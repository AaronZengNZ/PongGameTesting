using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Confirmation : MonoBehaviour
{
    public Button choice1;
    public Button choice2;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;
    public float yesChoice = 1f;
    public bool noChoice = false;
    public bool choiceHandling = false;
    public DialogText dt;
    public string threatenText = "you are not the one in control.";
    public string successText = "you enabled machine learning!";
    public string failText = "you didn't have a choice to begin with. you enabled machine learning.";
    public Toggle MachineLearningToggle;
    public GameObject confirmationWindow;
    public string DialogTextToggleString = "automatic";
    // Start is called before the first frame update
    void Start()
    {
        dt.dialogMode = DialogTextToggleString;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the buttons are hovered over

    }

    public void Choice1Hover(){
        StartCoroutine(Choice1HoverCoroutine());
    }

    public void Choice2Hover(){
        StartCoroutine(Choice2HoverCoroutine());
    }

    IEnumerator Choice1HoverCoroutine(){
        yield return new WaitForSeconds(0.3f);
        if(choiceHandling == true){}
        else if(noChoice){}
        else if(yesChoice == 2f){
            choice1Text.text = "Yes";
            choice2Text.text = "No";
            yesChoice = 1f;
        }
    }

    IEnumerator Choice2HoverCoroutine(){
        yield return new WaitForSeconds(0.3f);
        if(choiceHandling == true){}
        else if(noChoice){

        }
        else if(yesChoice == 1f){
            choice2Text.text = "Yes";
            choice1Text.text = "No";
            yesChoice = 2f;
        }
    }

    public void ChoiceClicked(float choice){
        if(noChoice){
            dt.TypeText(failText);
            StartCoroutine(WaitAndDisableConfirmationWindow());
            choiceHandling = true;
            return;
        }
        if(yesChoice != choice){
            choiceHandling = true;
            StartCoroutine(ChangeText());
        }
        else{
            dt.TypeText(successText);
            StartCoroutine(WaitAndDisableConfirmationWindow());
            choiceHandling = true;
        }
    }

    IEnumerator WaitAndDisableConfirmationWindow(){
        yield return new WaitForSeconds(0.5f);
        //make machine learning toggle uninteractable
        MachineLearningToggle.interactable = false;
        //find settings and call closeConfirmation
        GameObject settings = GameObject.Find("SettingsManager");
        settings.GetComponent<Settings>().CloseConfirmation();
        float t = 0f;
        float accel = 0.01f;
        while(t < 1f){
            t += 0.01f;
            confirmationWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Mathf.Lerp(45, 1000, t));
            yield return new WaitForSeconds(accel);
            accel *= 0.9f;
        }
    }

    IEnumerator ChangeText(){
        if(yesChoice == 2){
            dt.TypeText(threatenText);
            yield return new WaitForSeconds(1f);
            choice1Text.text = "N";
            yield return new WaitForSeconds(0.2f);
            choice1Text.text = "";
            yield return new WaitForSeconds(0.8f);
            choice1Text.text = "Y";
            yield return new WaitForSeconds(0.2f);
            choice1Text.text = "Ye";
            yield return new WaitForSeconds(0.2f);
            choice1Text.text = "Yes";
            choiceHandling = false;
            noChoice = true;
        }
        else if(yesChoice == 1){
            dt.TypeText(threatenText);
            yield return new WaitForSeconds(1f);
            choice2Text.text = "N";
            yield return new WaitForSeconds(0.2f);
            choice2Text.text = "";
            yield return new WaitForSeconds(0.8f);
            choice2Text.text = "Y";
            yield return new WaitForSeconds(0.2f);
            choice2Text.text = "Ye";
            yield return new WaitForSeconds(0.2f);
            choice2Text.text = "Yes";
            choiceHandling = false;
            noChoice = true;
        }
    }
}
