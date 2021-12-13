using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "OptionsScene"){
            Slider musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
            Slider sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>(); 
            musicVolume = musicSlider.value;
            sfxVolume = sfxSlider.value;
        }
    }
}
