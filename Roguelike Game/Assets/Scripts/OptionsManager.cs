using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [Header("Room Size UI")]
    public Slider widthSlider;
    public Slider heightSlider;
    public TMP_Text widthText;
    public TMP_Text heightText;

    void Start()
    {
        // Initialize the sliders with current GameSettings values
        widthSlider.value = GameSettings.levelWidth;
        heightSlider.value = GameSettings.levelHeight;

        // Update the text displays
        widthText.text = $"Width: {GameSettings.levelWidth}";
        heightText.text = $"Height: {GameSettings.levelHeight}";

        // If not already assigned via Inspector:
        widthSlider.onValueChanged.AddListener(OnWidthSliderChanged);
        heightSlider.onValueChanged.AddListener(OnHeightSliderChanged);
    }

    public void OnWidthSliderChanged(float newValue)
    {
        int widthValue = Mathf.RoundToInt(newValue);
        GameSettings.levelWidth = widthValue;
        widthText.text = $"Width: {widthValue}";
    }

    public void OnHeightSliderChanged(float newValue)
    {
        int heightValue = Mathf.RoundToInt(newValue);
        GameSettings.levelHeight = heightValue;
        heightText.text = $"Height: {heightValue}";
    }
}
