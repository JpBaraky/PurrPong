using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpBasic : MonoBehaviour
{
    [Header("Pulse PowerUp")]
    public float pulseSpeed = 1f; // The speed at which the power-up pulses
    public float pulseStartScale = 0.6f; // The starting scale of the pulse effect
    public float pulseEndScale = 0.8f; // The ending scale of the pulse effect
    public float destroyTimer = 5f;
    private Vector3 originalScale; // The original scale of the power-up
    
    [Header("Sound and Text")]
    private TextMeshProUGUI powerUpName;
    public string PowerName;
    public SpriteRenderer powerUpSprite;
    public AudioClip powerUpSoundClip;
    public AudioClip voiceClip;
    private AudioSource powerUpSound;
    public float displayDuration;
    public int flashCount;
    public float flashInterval;
    public bool isInventory;
    public int IDItem;
    public bool isProp;
        // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.Find("PowerUpText");
        if(targetObject != null) {
            powerUpName = targetObject.GetComponent<TextMeshProUGUI>();
        }
        originalScale = transform.localScale;
        if(!isInventory) {
        StartCoroutine(SelfDestroy());
        }
        powerUpSprite = GetComponent<SpriteRenderer>();
        powerUpSound = GetComponent<AudioSource>();
        powerUpSound.clip = powerUpSoundClip;
    }

    // Update is called once per frame
    void Update()
    {
        Pulse();
    }
    private void Pulse() {
        // Perform pulsating effect
        float scaleFactor = Mathf.Lerp(pulseStartScale,pulseEndScale,(Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f); // Scale factor for pulsation
        transform.localScale = originalScale * scaleFactor;
    }
    private IEnumerator SelfDestroy() {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }

    public IEnumerator DisplayText() {
        // Show the text
        powerUpName.enabled = true;
        powerUpName.text= PowerName;
        if(powerUpSound != null) {
            powerUpSound.Play();
        }
        // Flash the text
        for(int i = 0; i < flashCount; i++) {
            powerUpName.enabled = true;
            yield return new WaitForSeconds(flashInterval);
            powerUpName.enabled = false;
            yield return new WaitForSeconds(flashInterval);
        }
        if(isInventory)
        {
            Destroy(gameObject);
        }

        // Wait for the remaining duration
        yield return new WaitForSeconds(displayDuration - (flashCount * 2 * flashInterval));

        // Hide the text
        powerUpName.enabled = false;
    }
    
}
