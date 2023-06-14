using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private bool applyCameraShake;

    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;
    private ScoreKeeper scoreKeeper;
    private UIDisplay uiDisplay;
    private LevelManager levelManager;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        uiDisplay = FindObjectOfType<UIDisplay>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            scoreKeeper.ModifyScore();
        }
        else if (gameObject.tag == "Player")
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effectInstance.gameObject,
                effectInstance.main.duration + effectInstance.main.startLifetime.constantMax);
        }
    }

    private void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
