using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 사운드를 관리하는 매니저 클래스
/// </summary>
public class SoundMng : Singleton<SoundMng>
{
    protected SoundMng() { }

    public AudioSource backgroundAudio;
    public AudioSource effectAudio;
    Dictionary<string, AudioClip> backgrounds;
    Dictionary<string, AudioClip> effects;

    private void Awake()
    {
        FileMng.ins.LoadFile(ref effects, "Sound/Effect/");
        FileMng.ins.LoadFile(ref backgrounds, "Sound/Background/");

        backgroundAudio = gameObject.AddComponent<AudioSource>();
        effectAudio = gameObject.AddComponent<AudioSource>();

        SetBackgroundVolume(1.0f);
        SetEffectVolume(1.0f);
    }

    public void BackGroundMute(bool muted) { backgroundAudio.mute = muted; }

    public void EffectMute(bool muted) { effectAudio.mute = muted; }

    public bool IsPlayBackGround() { return backgroundAudio.isPlaying; }

    public void PlayEffect(string name) { effectAudio.PlayOneShot(effects[name]); }

    public void SetEffectVolume(float scale) { effectAudio.volume = scale; }

    public void PlayBackground(string name)
    {
        backgroundAudio.Stop();
        backgroundAudio.loop = true;
        backgroundAudio.clip = backgrounds[name];
        backgroundAudio.Play();
    }
    public void StopBackground() { backgroundAudio.Stop(); }

    public void SetBackgroundVolume(float volume) { backgroundAudio.volume = volume; }

    public void BackGroundPause() { backgroundAudio.Pause(); }

    public void EffectPause() { effectAudio.Pause(); }

    public void BackGroundUnPause() { backgroundAudio.UnPause(); }

    public void EffectUnPause() { effectAudio.UnPause(); }
}