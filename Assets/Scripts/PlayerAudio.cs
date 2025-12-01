using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("SFX names")]
    [SerializeField] private string runAudio;
    [SerializeField] private string jumpAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSfx()
    {
        AudioManager.instance.PlaySFX(jumpAudio, audioSource);
    }
}
