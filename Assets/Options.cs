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
    [SerializeField] private Toggle nightModeToggleUI;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    // Options
    private static bool nightMode = false;
    public static bool NightMode { get { return nightMode; } }

    // CONSTANTS
    [HideInInspector] public static Color LIGHT_BACKGROUND = new Color(1, 1, 1);
    [HideInInspector] public static Color DARK_BACKGROUND = new Color(.1f, .1f, .1f);
    [HideInInspector] public static Color LIGHT_SCOREHOLDER = new Color(0.92549f, 0.92549f, 0.92549f);
    [HideInInspector] public static Color DARK_SCOREHOLDER = new Color(.2f, .2f, .2f);
    [HideInInspector] public static Color TRANSPARENT = new Color(0f, 0f, 0f, 0f);

    // Public colors
    [HideInInspector] public static Color LEFT_COLOR = new Color(0.8431372549f, 0.46666666666f, .8f);
    [HideInInspector] public static Color RIGHT_COLOR = new Color(0.25098039215f, 0.59607843137f, 0.93725490196f);
    [HideInInspector] public static Color METER_COLOR = new Color(0.92549019607f, 0.92549019607f, 0.92549019607f);

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
            currentBackground.color = DARK_BACKGROUND;
        else
            currentBackground.color = LIGHT_BACKGROUND;

        // Set ScoreHolder if in scene
        if (scoreHolder != null)
        {
            if (nightMode)
                scoreHolder.color = DARK_SCOREHOLDER;
            else
                scoreHolder.color = LIGHT_SCOREHOLDER;
        }
    }

}
