using UnityEngine;

public class RandomAudioClip : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _audioClips;

    void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
        audioSource.Play();
    }
}
