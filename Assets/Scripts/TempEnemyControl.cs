using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TempEnemyControl : MonoBehaviour
{
    // Basic data
    private AircraftData data;
    // Control
    public bool isControlEnabled;
    public float currentSpeed{get; private set;}
    // Graphics & Info
    [SerializeField] private ParticleSystem[] jetParticleSystem;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    private ParticleSystem.MainModule[] jetParticleFX;
    [SerializeField] private string ModelName;
    // Gameplay
    [SerializeField] private GameObject player;
    public int health{get; private set;}
    protected virtual void Start()
    {
        // Fetch data
        data = GetComponent<AircraftData>();

        // Update Lighting Color
        transform.Find("Lighting").GetComponent<Light2D>().color = GetComponent<SpriteRenderer>().color;

        // Grant control
        // isControlEnabled = true;

        // Initialize jet particle color
        jetParticleFX = new ParticleSystem.MainModule[jetParticleSystem.Length];
        for (int i = 0; i < jetParticleSystem.Length; ++i)
        {
            jetParticleFX[i] = jetParticleSystem[i].main;
            jetParticleFX[i].startColor = GetComponent<SpriteRenderer>().color;
            jetParticleFX[i].startLifetime = 0;
        }
        foreach (var ps in jetParticleSystem)
        {
            ps.Play();
        }

        // Initialize health
        health = data.MaxHealth;
    }

    protected virtual void Update()
    {
        UpdateParticleFX();
        Vector3 relativePos = player.transform.position - transform.position;
        Vector3 forwardDirection = transform.up;
        float angle = Vector3.Angle(forwardDirection, relativePos);
        transform.Rotate(Vector3.forward * angle * Mathf.Deg2Rad * data.TurnSpeed * Time.deltaTime);

        // Applying acceleration
        if(currentSpeed <= data.MaxSpeed)
        {
            currentSpeed += data.Acceleration * Time.deltaTime;
        }

        // Moving
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

        // Applying friction && Determining stop
        currentSpeed -= AircraftData.Friction * Time.deltaTime;
        if(currentSpeed <= AircraftData.MinmumSpeedTolerance)
        {
            currentSpeed = 0.0f;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Friendly"))
        {
            if(other.gameObject.GetComponent<WeaponryData>().isActive)
            {
                health -= other.gameObject.GetComponent<WeaponryData>().GetDamage();
                other.gameObject.GetComponent<WeaponryData>().isActive = false;
                if(health <= 0)
                {
                    Explode();
                }
            }
        }
    }

    protected void UpdateParticleFX()
    {
        float normalizedVelocity = currentSpeed / data.MaxSpeed;
        for (int i = 0; i < jetParticleFX.Length; ++i)
        {
            jetParticleFX[i].startLifetime = normalizedVelocity * 0.5f;
        }
    }

    protected void Explode()
    {
        var explosionFX = Instantiate(explosionParticleSystem, transform.position, transform.rotation);
        explosionFX.transform.Find("Lighting").GetComponent<Light2D>().color = GetComponent<SpriteRenderer>().color;
        var main = explosionFX.main;
        main.startColor = GetComponent<SpriteRenderer>().color;
        explosionFX.Play();
        Destroy(gameObject);
    }
}
