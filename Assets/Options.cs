using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [Header("References to Components")]
    [SerializeField] private SpriteRenderer currentBackground;
    [SerializeField] private SpriteRenderer scoreHolder;
    [SerializeField] private SpriteRenderer scoreMiddle;
    [SerializeField] private Image leftUpgradeMeter;
    [SerializeField] private Image leftUpgradeMeterArrow;
    [SerializeField] private Image rightUpgradeMeter;
    [SerializeField] private Image rightUpgradeMeterArrow;
    [SerializeField] private Toggle nightModeToggleUI;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    // Options
    private static bool nightMode = false;
    public static bool NightMode { get { return nightMode; } }

    // CONSTANTS
    [HideInInspector] public static Color LIGHT_BACKGROUND = new Color(1, 1, 1);
    [HideInInspector] public static Color DARK_BACKGROUND = new Color(.1f, .1f, .1f);
    [HideInInspector] public static Color LIGHT_METER = new Color(0.92549019607f, 0.92549019607f, 0.92549019607f);
    [HideInInspector] public static Color DARK_METER = new Color(.2f, .2f, .2f);
    [HideInInspector] public static Color TRANSPARENT = new Color(0f, 0f, 0f, 0f);

    // Public colors
    [HideInInspector] public static Color LEFT_COLOR = new Color(0.8431372549f, 0.46666666666f, .8f);
    [HideInInspector] public static Color RIGHT_COLOR = new Color(0.25098039215f, 0.59607843137f, 0.93725490196f);
    [HideInInspector] public static Color METER_COLOR = LIGHT_METER;
    [HideInInspector] public static Color BACKGROUND_COLOR = LIGHT_BACKGROUND;

    void Start() 
    {
        // Set night mode toggle to display correct value
        bool oldNightMode = nightMode;
        nightModeToggleUI.isOn = nightMode;
        // Make sure setting stayed the same
        if (nightMode != oldNightMode)
            toggleNightMode();

        // Set volume slider to display correct value
        float currentVolume;
        audioMixer.GetFloat("volume", out currentVolume);
        volumeSlider.value = Mathf.Pow(10, (currentVolume/20f));
    }

    public void setVolume(float volume) 
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void toggleNightMode()
    {
        // Update setting
        nightMode = !nightMode;

        // Set background
        if (nightMode)
        {
            BACKGROUND_COLOR = DARK_BACKGROUND;
            METER_COLOR = DARK_METER;
        }
        else
        {
            BACKGROUND_COLOR = LIGHT_BACKGROUND;
            METER_COLOR = LIGHT_METER;
        }
        
        currentBackground.color = BACKGROUND_COLOR;

        // Set UI elements' colors if in scene
        if (scoreHolder != null)
        {
            scoreHolder.color = METER_COLOR;
            scoreMiddle.color = METER_COLOR;
            leftUpgradeMeter.color = METER_COLOR;
            rightUpgradeMeter.color = METER_COLOR;
            leftUpgradeMeterArrow.color = METER_COLOR;
            rightUpgradeMeterArrow.color = METER_COLOR;
        }
        
    }

}
