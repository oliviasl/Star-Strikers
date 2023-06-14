using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float firingRate = 0.2f;
    
    [Header("AI")]
    [SerializeField] private float firingRateVariance = 0.5f;
    [SerializeField] private float minFiringRate = 0.5f;
    [SerializeField] private bool useAI;

    private Coroutine firingCoroutine;
    private AudioPlayer audioPlayer;
    
    [HideInInspector] public bool isFiring = false;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    public float GetRandomFireRate()
    {
        float randomFiringRate = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
        return Mathf.Max(randomFiringRate, minFiringRate);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, 
                                                        transform.position,
                                                        Quaternion.identity);
            // upward movement of projectile
            Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            
            Destroy(projectileInstance, projectileLifetime);
            
            audioPlayer.PlayShootingClip();
            
            if (useAI)
            {
                yield return new WaitForSeconds(GetRandomFireRate());
            }
            else
            {
                yield return new WaitForSeconds(firingRate);
            }
        }
    }
}
