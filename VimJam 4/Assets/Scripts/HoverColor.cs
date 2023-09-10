using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HoverColor : MonoBehaviour
{
    public Button button;
    public Color desiredColor;
    private Color originalColor;
    private ColorBlock colorBlock;
    // Start is called before the first frame update
    void Start()
    {
        colorBlock = button.colors;
        originalColor = colorBlock.selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeWhenHover()
    {
        colorBlock.selectedColor = desiredColor;
        button.colors = colorBlock;
    }
    public void changeWhenLeft()
    {
        colorBlock.selectedColor = originalColor;
        button.colors = colorBlock;
    }
}
