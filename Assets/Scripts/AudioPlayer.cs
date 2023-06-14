using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")] 
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] private float shootingVolume = 1f;

    [Header("Damage")] 
    [SerializeField] private AudioClip damageClip;

    [SerializeField] [Range(0f, 1f)] private float damageVolume = 1f;

    private static AudioPlayer instance;

    /*
    void Awake()
    {
        ManageSingleton();
    }
    */

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    // for if we wanted to play the same music across all scenes without stop
    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}
