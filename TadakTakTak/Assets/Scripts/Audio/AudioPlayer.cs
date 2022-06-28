using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    protected AudioSource _audioSource;

    [SerializeField]
    protected float _pitchRandomness;
    protected float _pitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }
    private void Start()
    {
        _pitch = _audioSource.pitch; 
    }
    public void PlayClipWithValuable(AudioClip audioClip)
    {
        float randomPitch = Random.Range(-_pitchRandomness, _pitchRandomness);
        _audioSource.pitch = randomPitch;
        PlayClip(audioClip);
    }

    public void PlayClip(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
