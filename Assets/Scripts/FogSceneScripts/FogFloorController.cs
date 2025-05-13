using UnityEngine;
using UnityEngine.UI;

public class FogFloorController : MonoBehaviour
{
    public Slider grayScaleSlider;
    public Renderer floorRenderer; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(grayScaleSlider!=null)
        {
            grayScaleSlider.onValueChanged.AddListener(UpdateFloorColor); 
        }
        
    }

    private void UpdateFloorColor(float value)
    {
        float grayscale = 1.0f - value; // inverting: 0 = black, 1 = white
        Color grayColor = new Color(grayscale, grayscale, grayscale, 1f);
        if(floorRenderer!=null)
        {
            floorRenderer.material.color = grayColor; 
        }
    }

}
