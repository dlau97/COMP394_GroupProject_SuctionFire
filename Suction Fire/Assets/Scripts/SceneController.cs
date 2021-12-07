using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(SceneManager.GetActiveScene().name == "MenuScene"){
                QuitApplication();
            }
            else if(SceneManager.GetActiveScene().name == "DemoScene" || SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3"){
                SceneManager.LoadScene("MenuScene");
            }

        }
    }

    public void OnClickLoadDemoScene(){
        SceneManager.LoadScene("DemoScene");
    }
    public void OnClickLoadLevel1Scene(){
        SceneManager.LoadScene("Level 1");
    }
    public void OnClickLoadLevel2Scene(){
        SceneManager.LoadScene("Level 2");
    }
    public void OnClickLoadLevel3Scene(){
        SceneManager.LoadScene("Level 3");
    }
    public void OnClickLoadMenuScene(){
        SceneManager.LoadScene("MenuScene");
    }
    public void OnClickLoadOptionsScene()
    {
        SceneManager.LoadScene("OptionsScene");
    }
    public void LoadGameOverScene(){
        SceneManager.LoadScene("GameOverScene");
    }
    public void QuitApplication(){
        Application.Quit();
    }
}
