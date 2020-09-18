using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField, Range(1f, 2f)] private float maxPitch = 1.5f;
    [SerializeField, Range(0f, 1f)] private float minPitch = 0.5f;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip() {
        float pitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
