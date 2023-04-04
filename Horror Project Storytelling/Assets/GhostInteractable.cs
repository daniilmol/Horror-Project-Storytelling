using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractable : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip fallClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        float audioLevel = collision.relativeVelocity.magnitude / 10.0f;
        audioSource.PlayOneShot(fallClip, audioLevel);
    }
}
