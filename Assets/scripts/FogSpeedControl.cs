using UnityEngine;
using UnityEngine.UI;

public class FogSpeedControl : MonoBehaviour
{
    public ParticleSystem[] fogParticleSystems;
    public Slider speedSlider;

    void Start()
    {
        if (speedSlider != null)
        {
            // Hook up the event
            speedSlider.onValueChanged.AddListener(UpdateFogSpeed);
        }
    }

    void UpdateFogSpeed(float value)
    {
        // Convert slider value to grayscale color (0 = white, 1 = black)
        float grayscale = 1.0f - value; // inverting: 0 = black, 1 = white
        Color grayColor = new Color(grayscale, grayscale, grayscale, 1f);

        foreach(ParticleSystem particles in fogParticleSystems)
        {
            if (particles != null)
            {
                var main = particles.main;
                main.startSpeed = value;
            }
        }
        // Apply to Particle System Start Color
        
    }
}
