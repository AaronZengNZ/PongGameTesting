using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogText : MonoBehaviour
{
    public TextMeshPro dialog1;
    public TextMeshPro dialog2;
    public TextMeshPro dialog3;
    public float typeSpeed = 10f;
    public string placeHolderText = "placeholder";
    public bool typing = false;
    public float delay = 0.5f;
    public bool Test = false;
    public float[] order = new float[3];
    public string dialogMode = "none";
    public string[] playerCatchingUp = new string[3];
    public string[] aiCatchingUp = new string[3];
    public string[] playerTies = new string[3];
    public string[] aiTies = new string[3];
    public string[] playerLeading = new string[3];
    public string[] aiLeading = new string[3];
    public string[] updateDialogs = new string[3];
    public Update updater;
    public float updateNum = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Test == true)
        {
            Test = false;
            TypeText(placeHolderText);
        }
    }

    public void FindDialog(float playerScore, float aiScore, float playerWinstreak, float aiWinstreak, string Paddle){
        if(Paddle == "player"){
            if(playerScore - aiScore >= 5f && updateNum == 0f){
                updateNum++;
                TypeText(updateDialogs[0]);
                updater.UpdateGame(updateNum);
            }
            else if(playerScore > aiScore){
                if(playerScore - aiScore > 3f){
                    TypeText(playerLeading[0]);
                }
                else if(playerWinstreak < 3f){
                    TypeText(playerLeading[1]);
                }
                else {
                    TypeText(playerLeading[2]);
                }
            }
            else if(playerScore < aiScore){
                if(playerWinstreak < 3f){
                    TypeText(playerCatchingUp[0]);
                }
                else {
                    TypeText(playerCatchingUp[1]);
                }
            }
            else{
                if(playerWinstreak < 3f){
                    TypeText(playerTies[0]);
                }
                else {
                    TypeText(playerTies[1]);
                }
            }
        }
        if(Paddle == "ai"){
            if(aiScore > playerScore){
                if(aiScore - playerScore > 3f){
                    TypeText(aiLeading[0]);
                }
                else if(aiWinstreak < 3f){
                    TypeText(aiLeading[1]);
                }
                else {
                    TypeText(aiLeading[2]);
                }
            }
            else if(aiScore < playerScore){
                if(aiWinstreak < 3f){
                    TypeText(aiCatchingUp[0]);
                }
                else {
                    TypeText(aiCatchingUp[1]);
                }
            }
            else{
                if(aiWinstreak < 3f){
                    TypeText(aiTies[0]);
                }
                else {
                    TypeText(aiTies[1]);
                }
            }
        }
    }

    public void TypeText(string text)
    {
        if (!typing)
        {
            StartCoroutine(TypeTextCoroutine(text));
        }
    }

    IEnumerator TypeTextCoroutine(string text)
    {
        if (true == true)
        {
            //shuffle text
            StartCoroutine(ShuffleText());
            typing = true;
            string currentText = "";
            for (int i = 0; i <= text.Length; i++)
            {
                currentText = text.Substring(0, i);
                if (order[0] == 1)
                {
                    dialog1.text = currentText;
                }
                else if (order[0] == 2)
                {
                    dialog2.text = currentText;
                }
                else if (order[0] == 3)
                {
                    dialog3.text = currentText;
                }
                yield return new WaitForSeconds(1 / typeSpeed);
            }
            if (0.075f - 1 / typeSpeed > 0f)
            {
                yield return new WaitForSeconds((0.075f - 1 / typeSpeed) * text.Length);
            }
            yield return new WaitForSeconds(delay);
            typing = false;
        }
    }

    IEnumerator ShuffleText()
    {
        typing = true;
        //lerp order[0]'s y to -4.5f over 1 second
        float time = 0f;
        //set the first one in order to be last
        float first = order[0];
        float second = order[1];
        float third = order[2];
        order[0] = third;
        order[1] = first;
        order[2] = second;
        Debug.Log(order[1] + " - " + order[0]);
        //set order[0]'s opacity to max
        GetText(order[0]).color = new Color(GetText(order[0]).color.r, GetText(order[0]).color.g, GetText(order[0]).color.b, 1f);
        GetText(order[0]).transform.position = new Vector3(GetText(order[0]).transform.position.x, -5f, GetText(order[0]).transform.position.z);
        while (time < 1f)
        {
            time += Time.deltaTime;
            GetText(order[1]).transform.position = new Vector3(GetText(order[1]).transform.position.x, Mathf.Lerp(GetText(order[1]).transform.position.y, -4.5f, time), GetText(order[1]).transform.position.z);
            GetText(order[2]).transform.position = new Vector3(GetText(order[2]).transform.position.x, Mathf.Lerp(GetText(order[2]).transform.position.y, -4f, time), GetText(order[2]).transform.position.z);
            GetText(order[2]).color = new Color(GetText(order[2]).color.r, GetText(order[2]).color.g, GetText(order[2]).color.b, Mathf.Lerp(GetText(order[2]).color.a, 0f, time));
            yield return null;
        }
        typing = false;
    }

    public TextMeshPro GetText(float num)
    {
        if (num == 1)
        {
            return dialog1;
        }
        else if (num == 2)
        {
            return dialog2;
        }
        else if (num == 3)
        {
            return dialog3;
        }
        else
        {
            return dialog1;
        }
    }
}
