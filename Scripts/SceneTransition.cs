
using System.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneTransition : MonoBehaviour
{
    public GameObject sceneTransition;
    public TextMeshProUGUI sceneTransitionText;
    public TextMeshProUGUI sceneTranstitionSecondaryText;
    public string[] objectsToLoad;
    public float[] objectLoadTimes;
    float currentLoadTime = 0f;
    float currentObject = 0f;
    float currentMaxLoadTime = 0f;
    float loading = 0f;
    float loadSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CommenceLoadScene(){
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    IEnumerator LoadScene(){
        sceneTransition.SetActive(true);
        while(currentObject < objectsToLoad.Length){
            if(loading < 33f){
                sceneTransitionText.text = "Loading.";
            }
            else if(loading < 66f){
                sceneTransitionText.text = "Loading..";
            }
            else if(loading < 100f){
                sceneTransitionText.text = "Loading...";
            }
            else{
                loading = 0f;
            }
            if(currentLoadTime <= 0f){
                currentObject++;
                currentLoadTime = objectLoadTimes[(int)currentObject-1];
                currentMaxLoadTime = currentLoadTime;
                sceneTranstitionSecondaryText.text = objectsToLoad[(int)currentObject-1] + " (" + Mathf.Round(currentMaxLoadTime - currentLoadTime) + "/" + Mathf.Round(currentMaxLoadTime) +")";
            }
            yield return new WaitForSeconds(0.01f);
            loading += 1f;
            loadSpeed += Random.Range(0f, 0.05f) + loadSpeed * Random.Range(0f, 0.1f);
            if(loadSpeed <= 0f){
                loadSpeed = 0f;
            }
            else if(loadSpeed >= Random.Range(5f,10f)){
                loadSpeed = Random.Range(0f,1f);
            }
            currentLoadTime -= loadSpeed;
            sceneTranstitionSecondaryText.text = objectsToLoad[(int)currentObject-1] + " (" + Mathf.Round(currentMaxLoadTime - currentLoadTime) + "/" + Mathf.Round(currentMaxLoadTime) +")";
        }
        sceneTransitionText.text = "Done!";
        sceneTranstitionSecondaryText.text = "Press any key to continue";
        yield return new WaitForSeconds(0.5f);
        while(!Input.anyKeyDown){
            yield return null;
        }
        SceneManager.LoadScene("Pong");
    }
}
