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
            UpdateFogSpeed(speedSlider.value); //pour set la value de base 
        }
    }

    void UpdateFogSpeed(float value)
    {
       

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
