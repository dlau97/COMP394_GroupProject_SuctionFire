using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public float bestTimeLvl1, bestTimeLvl2, bestTimeLvl3;
    public float startTime, currTime, endTime;
    private string bestTimeKeyLvl1 = "BestTimeLvl1";
    private string bestTimeKeyLvl2 = "BestTimeLvl2";
    private string bestTimeKeyLvl3 = "BestTimeLvl3";

    public float musicVolume = 1f;
    public float sfxVolume = 1f;
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);

        int numSettingsControllers = FindObjectsOfType<SettingsController>().Length;
        if (numSettingsControllers != 1)
        {
            Destroy(this.gameObject);
        }
        // if more than one settings controller is in the scene
        //destroy ourselves
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        bestTimeLvl1 = PlayerPrefs.GetFloat(bestTimeKeyLvl1, 0f);
        bestTimeLvl2 = PlayerPrefs.GetFloat(bestTimeKeyLvl2, 0f);
        bestTimeLvl3 = PlayerPrefs.GetFloat(bestTimeKeyLvl3, 0f);
        currTime = 0.0f;
        startTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if(activeScene == "MenuScene"){
            GameObject.Find("txt_BestTime1").GetComponent<TMP_Text>().text = "Best Time: "+(Mathf.Round(bestTimeLvl1 * 100.0f) / 100.0f).ToString() + " seconds" ;
            GameObject.Find("txt_BestTime2").GetComponent<TMP_Text>().text = "Best Time: "+(Mathf.Round(bestTimeLvl2 * 100.0f) / 100.0f).ToString() + " seconds" ;
            GameObject.Find("txt_BestTime3").GetComponent<TMP_Text>().text = "Best Time: "+(Mathf.Round(bestTimeLvl3 * 100.0f) / 100.0f).ToString() + " seconds" ;
        }
        else if(activeScene == "Level 1" || activeScene == "Level 2" || activeScene == "Level 3"){
            GameObject.Find("txt_Timer").GetComponent<TMP_Text>().text = (Mathf.Round(GetCurrentTime() * 100.0f) / 100.0f).ToString();
        }
        if(activeScene == "OptionsScene"){
            Slider musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
            Slider sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>(); 
            musicVolume = musicSlider.value;
            sfxVolume = sfxSlider.value;
        }
    }
    public void StartTimer(){
        Debug.Log("Timer Started");
        startTime = Time.time;
    }
    public float GetCurrentTime(){
        currTime = Time.time - startTime;
        return currTime;
    }

    public void EndLevel1Timer(){
        endTime = Time.time;
        Debug.Log("Exit Door Reached");
        if(endTime - startTime < bestTimeLvl1 || bestTimeLvl1 == 0f){
            Debug.Log("Saving Best Level 1 Score");
            bestTimeLvl1 = endTime - startTime;
            bestTimeLvl1 =  (Mathf.Round(bestTimeLvl1 * 100.0f) / 100.0f);
            PlayerPrefs.SetFloat(bestTimeKeyLvl1, bestTimeLvl1);
            PlayerPrefs.Save();
        }
    }

    public void EndLevel2Timer(){
        endTime = Time.time;
        Debug.Log("Exit Door Reached");
        if(endTime - startTime < bestTimeLvl2 || bestTimeLvl2 == 0f){
             Debug.Log("Saving Best Level 2 Score");
            bestTimeLvl2 = endTime - startTime;
            bestTimeLvl2 =  (Mathf.Round(bestTimeLvl2 * 100.0f) / 100.0f);
            PlayerPrefs.SetFloat(bestTimeKeyLvl2, bestTimeLvl2);
            PlayerPrefs.Save();
        }
    }

    public void EndLevel3Timer(){
        endTime = Time.time;
        Debug.Log("Exit Door Reached");
        if(endTime - startTime < bestTimeLvl3 || bestTimeLvl3 == 0f){
             Debug.Log("Saving Best Level 3 Score");
            bestTimeLvl3 = endTime - startTime;
            bestTimeLvl3 =  (Mathf.Round(bestTimeLvl3 * 100.0f) / 100.0f);
            PlayerPrefs.SetFloat(bestTimeKeyLvl3, bestTimeLvl3);
            PlayerPrefs.Save();
        }
    }
}
