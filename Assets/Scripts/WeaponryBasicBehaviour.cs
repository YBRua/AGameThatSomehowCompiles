using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WeaponryBasicBehaviour : MonoBehaviour
{
    private float timeSinceInstantiated;
    [HideInInspector] public WeaponryData data;

    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private Light2D lighting;
    protected virtual void Start()
    {
        // Initialization
        data = GetComponent<WeaponryData>();
        data.isActive = true;
        timeSinceInstantiated = 0.0f;

        // Appearance
        var main = explosionEffect.main;
        main.startColor = GetComponent<SpriteRenderer>().color;
        transform.Find("Lighting").GetComponent<Light2D>().color = GetComponent<SpriteRenderer>().color;
    }

    protected virtual void Update()
    {
        timeSinceInstantiated += Time.deltaTime;
        if(data.filghtSpeed * timeSinceInstantiated >= data.maxRange)
        {
            DestroyOutOfRange();
        }
        if(data.isActive)
        {
            transform.Translate(Vector3.up * Time.deltaTime * data.filghtSpeed);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Friendly"))
        {
            if(data.isActive)
            {
                Detonate();
            }
        }

    }

    protected virtual void DestroyOutOfRange()
    {
        data.isActive = false;
        StartCoroutine(FadeOut(0.3f));
    }

    protected virtual void Detonate()
    {
        explosionEffect.Play();
        StartCoroutine(FadeOut(0.3f));
    }

    protected virtual IEnumerator FadeOut(float duration)
    {
        Color currentC = GetComponent<SpriteRenderer>().color;
        for(float i = 0.0f; i < 1.0f; i += Time.deltaTime/duration)
        {
            Color newColor = new Color(currentC.r, currentC.g, currentC.b, Mathf.Lerp(1.0f, 0.0f, duration));
            GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}
