using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    
    public Slider sliderRef;
    
    public Light lightRef;

    void OnEnable()
    {
        //Subscribe to the Slider Click event
        sliderRef.onValueChanged.AddListener(sliderCallBack);
    }

    //Will be called when Slider changes
    public void sliderCallBack(float value)
    {
        Debug.Log("Slider Value Changed: " + value);
        lightRef.intensity = sliderRef.value;
    }

    public void OnDisable()
    {
        //Un-Subscribe To Slider Event
        sliderRef.onValueChanged.RemoveAllListeners();
    }
}
