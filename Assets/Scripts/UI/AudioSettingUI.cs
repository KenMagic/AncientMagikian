using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Button closeBtn;

    public AudioMixer audioMixer; // Gắn MainAudioMixer ở đây
 

    private void Start()
    {
        audioMixer = AudioManager.Instance.audioMixer;
        UpdateSlidersFromMixer();
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        closeBtn.onClick.AddListener(() => CloseUI());
    }
        void UpdateSlidersFromMixer()
    {
        float value;

        if (audioMixer.GetFloat("MasterVolume", out value))
            masterSlider.value = DbToNormalized(value);

        if (audioMixer.GetFloat("BGMVolume", out value))
            bgmSlider.value = DbToNormalized(value);

        if (audioMixer.GetFloat("SFXVolume", out value))
            sfxSlider.value = DbToNormalized(value);
    }

    // Convert from dB to [0..1]
    float DbToNormalized(float db)
    {
        return Mathf.Pow(10, db / 20f); // inverse of log scale
    }

    private void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
