using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    public AudioClip victoryTheme;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip attackClip;
    public AudioClip blockClip;
    public AudioClip hitClip;
    public AudioClip curseClip;
    public AudioClip stunClip;

    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        ApplyVolumes();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        ApplyVolumes();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        ApplyVolumes();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        bgmSource.volume = masterVolume * bgmVolume;
        sfxSource.volume = masterVolume * sfxVolume;
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
    public void PlayBlock() => PlaySFX(blockClip);
    public void PlayHit() => PlaySFX(hitClip);
    public void PlayCurse() => PlaySFX(curseClip);
    public void PlayStun() => PlaySFX(stunClip);
}
public enum SFXType { Attack, Block, Hit, Curse, Stun }



