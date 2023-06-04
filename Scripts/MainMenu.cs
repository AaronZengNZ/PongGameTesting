using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public bool hovered = false;
    public RectTransform playButton;
    public float prevX = -750;
    public float destX = -500;
    public float lerpSpeed = 1f;
    public void Hover(){
        hovered = true;
    }

    public void UnHover(){
        hovered = false;
    }

    void FixedUpdate(){
        //if hovered, lerp playbutton to destX, else lerp playButton to prevX
        if(hovered){
            playButton.anchoredPosition = new Vector2(Mathf.Lerp(playButton.anchoredPosition.x, destX, lerpSpeed), playButton.anchoredPosition.y);
        } else {
            playButton.anchoredPosition = new Vector2(Mathf.Lerp(playButton.anchoredPosition.x, prevX, lerpSpeed), playButton.anchoredPosition.y);
        }
    }
}
