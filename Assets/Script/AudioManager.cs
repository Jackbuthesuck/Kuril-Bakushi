using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public static AudioManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayStart()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void PlayFire()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void PlayFireEnd()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void PlayReload()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
}

