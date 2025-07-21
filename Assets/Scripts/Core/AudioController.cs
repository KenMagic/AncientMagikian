using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer")]
    public AudioMixer audioMixer; // Gắn MainAudioMixer ở đây

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM Clips")]
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    public AudioClip boss1Theme;
    public AudioClip boss2Theme;
    public AudioClip victoryTheme;
    public AudioClip loseTheme;

    [Header("SFX Clips")]
    public AudioClip attackClip;
    public AudioClip meleeClip;
    public AudioClip hitClip;
    public AudioClip curseClip;
    public AudioClip clickClip;
    public AudioClip blastClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Volume từ 0–1 chuyển thành dB (-80 đến 0)
    private float ToDecibel(float linear)
    {
        return linear <= 0.0001f ? -80f : Mathf.Log10(linear) * 20f;
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", ToDecibel(value));
    }

    public void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", ToDecibel(value));
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", ToDecibel(value));
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Shortcut methods
    public void PlayAttack() => PlaySFX(attackClip);
    public void PlayMelee() => PlaySFX(meleeClip);
    public void PlayHit() => PlaySFX(hitClip);
    public void PlayCurse() => PlaySFX(curseClip);
    public void PlayClick() => PlaySFX(clickClip);
    public void PlayBlast() => PlaySFX(blastClip);
}
