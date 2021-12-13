using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource source;

    float musicVolumeFactor = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        source = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float vol = GameObject.Find("GameSettings").GetComponent<SettingsController>().musicVolume;
        source.volume = vol * musicVolumeFactor;
    }
}
