using System.Collections.Generic;
using UnityEngine;

public class ExampleInventory : MonoBehaviour
{
    private List<PowerUp> powerUpInventory = new List<PowerUp>();

    public class PowerUp
    {
        public string type;
        public float duration;

        public PowerUp(string type, float duration)
        {
            this.type = type;
            this.duration = duration;
        }
    }

    // Add a power-up to the inventory.
    public void AddPowerUp(string type, float duration)
    {
        PowerUp collectedPowerUp = new PowerUp(type, duration);
        powerUpInventory.Add(collectedPowerUp);
    }

    // Use a specific power-up from the inventory.
    public void UsePowerUp(string type)
    {
        PowerUp powerUpToRemove = null;

        foreach (PowerUp powerUp in powerUpInventory)
        {
            if (powerUp.type == type)
            {
                // Apply the effect associated with this power-up.
                // You can modify game elements like paddle size, speed, etc.
                // For this example, we'll just remove the power-up.
                powerUpToRemove = powerUp;
                break; // Exit the loop since the power-up is used.
            }
        }

        if (powerUpToRemove != null)
        {
            powerUpInventory.Remove(powerUpToRemove);
        }
    }

    // Remove expired power-ups from the inventory.
    public void RemoveExpiredPowerUps()
    {
        powerUpInventory.RemoveAll(powerUp => powerUp.duration <= 0f);
    }

    // Update is called once per frame.
    void Update()
    {
        // Decrease the duration of all active power-ups.
        foreach (PowerUp powerUp in powerUpInventory)
        {
            powerUp.duration -= Time.deltaTime;
        }
    }
}
