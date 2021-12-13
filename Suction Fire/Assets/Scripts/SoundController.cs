using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public AudioClip jumpSFX, deathSFX, shootSFX, ammoCollectSFX, launchPadSFX, enemyHitSFX, enemyDeathSFX, checkpointSFX, collisionSFX;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        float vol = GameObject.Find("GameSettings").GetComponent<SettingsController>().sfxVolume;
        source.volume = vol;
    }

    public void PlayJumpSFX(){
        source.PlayOneShot(jumpSFX,0.35f);
    }
    public void PlayDeathSFX(){
        source.PlayOneShot(deathSFX,0.5f);
    }
    public void PlayShootSFX(){
        source.PlayOneShot(shootSFX,0.4f);
    }
    public void PlayAmmoCollectSFX(){
        source.PlayOneShot(ammoCollectSFX,1f);
    }
    public void PlayLaunchPadSFX(){
        source.PlayOneShot(launchPadSFX,0.7f);
    }
    public void PlayEnemyHitSFX(){
        source.PlayOneShot(enemyHitSFX,2f);
    }
    public void PlayEnemyDeathSFX(){
        source.PlayOneShot(enemyDeathSFX,0.4f);
    }
    public void PlayCollisionSFX(){
        source.PlayOneShot(collisionSFX,0.5f);
    }

    public void setVolume(float vol){
        source.volume = vol;
    }
}
