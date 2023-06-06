using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Changelog : MonoBehaviour
{
    public GameObject[] changelogs;
    public GameObject changelogHolder;
    public GameObject askForInput;
    public string[] correctPasswords;
    //public input ui
    public GameObject input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenChangelogHolder(){
        changelogHolder.SetActive(true);
        askForInput.SetActive(true);
    }
    public void CheckPassword(){
        string currentInput = input.GetComponent<TMP_InputField>().text;
        //if the currentInput matches any of the correct passwords, disable the corresponding changelog's encrypted componenet
        for(int i = 0; i < correctPasswords.Length; i++){
            if(currentInput == correctPasswords[i]){
                changelogs[i].GetComponent<TextGlitch>().encrypted = false;
                //also set encrypted to false for all children of said changelog
                foreach(TextGlitch textGlitch in changelogs[i].GetComponentsInChildren<TextGlitch>()){
                    textGlitch.encrypted = false;
                }
                askForInput.SetActive(false);
                break;
            }
        }
        askForInput.SetActive(false);
    }

    public void CloseChangelogHolder(){
        changelogHolder.SetActive(false);
    }

    public void OpenChangelog(float num){
        //enable the num changelog and disable all others
        for(int i = 0; i < changelogs.Length; i++){
            if(i == num){
                changelogs[i].SetActive(true);
            }
            else{
                changelogs[i].SetActive(false);
            }
        }
    }
}
