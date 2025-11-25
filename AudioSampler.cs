using UnityEngine;

public class AudioSampler : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    public Vector2 minMaxPitch = new(0.9f, 1.1f);

    private void Awake()
    {
        if(!source) source = GetComponent<AudioSource>();
    }

    public void Play(int index)
    {
        try
        {
            source.pitch = Random.Range(minMaxPitch.x, minMaxPitch.y);
            source.PlayOneShot(clips[index]);
        }
        catch
        {
            //Debug.LogWarning(name + " could not play clip from index: " + index);
        }
    }
}
