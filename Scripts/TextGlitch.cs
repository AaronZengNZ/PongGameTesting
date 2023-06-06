using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextGlitch : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    string normalText;
    public float glitchChance = 0.1f;
    public float glitchDuration = 0.1f;
    public bool encrypted = false;
    // Start is called before the first frame update
    void Start()
    {
        //set normalText to targetText's text
        normalText = targetText.text;
        StartCoroutine(GlitchText());
    }

    void OnEnable(){
        StartCoroutine(GlitchText());
    }

    IEnumerator GlitchText(){
        while(true){
            if(encrypted){
                string scrambledText = "";
                for(int i = 0; i < normalText.Length; i++){
                    scrambledText += normalText[Random.Range(0,normalText.Length)];
                }
                targetText.text = scrambledText;
            }
            else if(Random.Range(0f,1f) <= glitchChance){
                string scrambledText = "";
                for(int i = 0; i < normalText.Length; i++){
                    scrambledText += normalText[Random.Range(0,normalText.Length)];
                }
                targetText.text = scrambledText;
                yield return new WaitForSeconds(glitchDuration);
                targetText.text = normalText;
                yield return new WaitForSeconds(glitchDuration);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
