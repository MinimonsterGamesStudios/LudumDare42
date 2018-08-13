using UnityEngine;

public class RandomAudioClip : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _audioClips;
    public AudioClip clip;
    void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        clip = _audioClips[Random.Range(0, _audioClips.Length)];
        audioSource.clip = clip;
        audioSource.Play();
    }
}
