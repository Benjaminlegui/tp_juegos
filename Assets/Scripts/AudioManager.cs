using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioDB audioDB;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource uiSfxSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void PlaySFX(string soundName, AudioSource sfxSource, string type)
    {
        var data = audioDB.Get(soundName);
        if (data == null)
        {
            Debug.Log("Attempt to play sound - " + soundName);
            return;
        }

        var clip = data.GetRandomClip();
        if (clip == null) return;

        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.clip = clip;

        if (type == "loop")
        {
            sfxSource.loop = true;
            sfxSource.Play();
            return;
        }

        sfxSource.loop = false;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayUISFX(string soundName)
    {
        var data = audioDB.Get(soundName);
        if (data == null)
        {
            Debug.Log("Attempt to play sound - " + soundName);
            return;
        }

        var clip = data.GetRandomClip();
        if (clip == null) return;

        uiSfxSource.clip = clip;

        uiSfxSource.PlayOneShot(clip);
    }

    public void StopSfx(AudioSource sfxSource)
    {
        sfxSource.loop = false;
        sfxSource.Stop();
    }

    public void PlayBGM(string musicName, float fadeTime = 0f)
    {
        var data = audioDB.Get(musicName);
        if (data == null)
        {
            Debug.LogError("BGM not found: " + musicName);
            return;
        }

        AudioClip clip = data.GetRandomClip();
        if (clip == null) return;

        if (fadeTime > 0)
        {
            StartCoroutine(FadeInBGM(clip, fadeTime));
        }
        else
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    private IEnumerator FadeInBGM(AudioClip newClip, float duration)
    {
        bgmSource.loop = true;

        float startVol = bgmSource.volume;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVol, 0f, t / duration);
            yield return null;
        }

        bgmSource.clip = newClip;
        bgmSource.Play();

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0f, startVol, t / duration);
            yield return null;
        }
    }

    public void PauseBGM() => bgmSource.Pause();
    public void ResumeBGM() => bgmSource.UnPause();
    public void StopBGM() => bgmSource.Stop();
}
