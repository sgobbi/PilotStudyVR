using UnityEngine;

public class LightIntensityController : MonoBehaviour
{
    public Light[] targetLights;          // Assign up to 4 lights in the Inspector
    public float intensityStep = 0.2f;    // How much to change per key press
    public float minIntensity = 0f;       // Minimum allowed intensity
    public float maxIntensity = 8f;       // Maximum allowed intensity

    void Update()
    {
        if (targetLights == null || targetLights.Length == 0)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AdjustLights(intensityStep);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AdjustLights(-intensityStep);
        }
    }

    void AdjustLights(float delta)
    {
        foreach (Light light in targetLights)
        {
            if (light != null)
            {
                light.intensity = Mathf.Clamp(light.intensity + delta, minIntensity, maxIntensity);
                Debug.Log($"{light.name} intensity: {light.intensity}");
            }
        }
    }


    public void SetLightIntensity(float sliderValue)
    {
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, sliderValue);

        foreach (Light light in targetLights)
        {
            if (light != null)
            {
                light.intensity = intensity;
            }
        }
    }
}