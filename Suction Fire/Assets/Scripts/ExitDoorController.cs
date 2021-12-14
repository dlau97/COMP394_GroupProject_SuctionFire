using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            string activeScene = SceneManager.GetActiveScene().name;
            if(activeScene == "Level 1"){
                GameObject.FindObjectOfType<SettingsController>().EndLevel1Timer();
            }
            else if(activeScene == "Level 2"){
                GameObject.FindObjectOfType<SettingsController>().EndLevel2Timer();
            }
            else if(activeScene == "Level 3"){
                GameObject.FindObjectOfType<SettingsController>().EndLevel3Timer();
            }
            GameObject.Find("SceneManager").SendMessage("OnClickLoadMenuScene");
        }
    }
}
