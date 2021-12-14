using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    private SoundController soundController;
    // Start is called before the first frame update
    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(SceneManager.GetActiveScene().name == "MenuScene"){
                soundController.PlayButtonClickSFX();
                QuitApplication();
            }
            else if(SceneManager.GetActiveScene().name == "DemoScene" || SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3"){
                soundController.PlayButtonClickSFX();
                SceneManager.LoadScene("MenuScene");
            }

        }
    }

    public void OnClickLoadDemoScene(){
        soundController.PlayButtonClickSFX();
        SceneManager.LoadScene("DemoScene");

    }
    public void OnClickLoadLevel1Scene(){
        soundController.PlayButtonClickSFX();
        SceneManager.LoadScene("Level 1");
    }
    public void OnClickLoadLevel2Scene(){
        soundController.PlayButtonClickSFX();
        SceneManager.LoadScene("Level 2");
    }
    public void OnClickLoadLevel3Scene(){
        soundController.PlayButtonClickSFX();
        SceneManager.LoadScene("Level 3");
    }
    public void OnClickLoadMenuScene(){
        soundController.PlayButtonClickSFX();
        SceneManager.LoadScene("MenuScene");
    }
    public void OnClickLoadOptionsScene()
    {
        soundController.PlayButtonClickSFX();
        SceneManager.LoadScene("OptionsScene");
    }
    public void LoadGameOverScene(){
        SceneManager.LoadScene("GameOverScene");
    }
    public void QuitApplication(){
        soundController.PlayButtonClickSFX();
        Application.Quit();
    }
}
