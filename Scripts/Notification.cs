using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Notification : MonoBehaviour
{
    public TextMeshProUGUI notificationText;
    public GameObject notification;
    public string[] notifications;
    public float notificationNum = 0f;
    public Button settingsButton;
    public EventTrigger settings;
    public TextMeshProUGUI settingsText;
    public RectTransform settingsRect;
    public void EnableNotification(){
        notification.SetActive(true);
        notificationNum++;
        if(notificationNum > notifications.Length - 1f){
            notificationNum = notifications.Length;
            settingsButton.interactable = false;
            settings.enabled = false;
            settingsText.text = "bWlzc2luZ05v";
            settingsRect.anchoredPosition = new Vector2(-850f, settingsRect.anchoredPosition.y);
            //find all mainmenu scripts, set hovered to false
            MainMenu[] mainMenus = FindObjectsOfType<MainMenu>();
            foreach(MainMenu mainMenu in mainMenus){
                mainMenu.hovered = false;
            }
        }
        notificationText.text = notifications[(int)(notificationNum - 1f)];
    }
    public void DisableNotification(){
        notification.SetActive(false);
    }
}
