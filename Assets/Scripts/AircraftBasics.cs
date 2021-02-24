using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class AircraftBasics : MonoBehaviour
{
    // Basic data
    private AircraftData data;
    public bool isPlayerControlling;
    // Control
    public float currentSpeed{get; private set;}
    private float verticalInput;
    private float horizontalInput;
    // GUI Reference
    public TextMeshProUGUI speedText;
    // Graphics & Info
    [SerializeField] private ParticleSystem[] jetParticleSystem;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    private ParticleSystem.MainModule[] jetParticleFX;
    [SerializeField] private string ModelName;
    // Gameplay
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
        if(isPlayerControlling)
        {
            UpdateParticleFX();

            // Applying acceleration
            verticalInput = Input.GetAxis("Vertical");
            if (currentSpeed <= data.MaxSpeed && verticalInput > 0)
            {
                currentSpeed += data.Acceleration * Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                if (currentSpeed > AircraftData.MinmumSpeedTolerance)
                {
                    currentSpeed -= data.Deceleration * Time.deltaTime;
                }
            }

            // Applying rotation
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(new Vector3(0, 0, -1) * horizontalInput * data.TurnSpeed * Time.deltaTime);

            // Moving
            transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);

            // Applying friction && Determining stop
            currentSpeed -= AircraftData.Friction * Time.deltaTime;
            if(currentSpeed <= AircraftData.MinmumSpeedTolerance)
            {
                currentSpeed = 0.0f;
            }

            // Updating GUI
            speedText.SetText(((int)(currentSpeed * 100)).ToString());
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if((isPlayerControlling && other.gameObject.CompareTag("Enemy")) || !(isPlayerControlling && other.gameObject.CompareTag("Friendly")))
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