using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] Image colorDisplay;

    [Serializable]
    public enum ColorOption
    {
        Red,
        Green,
        Blue,
    }
    
    public void ChangeColorR(float value)
    {
        colorDisplay.color = new Color(value, colorDisplay.color.g, colorDisplay.color.b);
        Player.Instance.thirdColor = colorDisplay.color;
        
    }

    public void ChangeColorG(float value)
    {
        colorDisplay.color = new Color(colorDisplay.color.r, value, colorDisplay.color.b);
        Player.Instance.thirdColor = colorDisplay.color;

    }
    public void ChangeColorB(float value)
    {
        colorDisplay.color = new Color(colorDisplay.color.r, colorDisplay.color.g, value);
        Player.Instance.thirdColor = colorDisplay.color;

    }

}
