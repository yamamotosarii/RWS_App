using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour {
    public AudioClip audioClip1;
    private AudioSource audioSource;
    public int startingPitch = 1;
    public int timeToDecrease = 5;

    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        //audioSource.pitch = startingPitch;
        audioSource.Play ();
    }
    void Update()
    {
        //While the pitch is over 0, decrease it as time passes.
        /*
        if (audioSource.pitch > 0)
        {
            audioSource.pitch -= Time.deltaTime * startingPitch / timeToDecrease;
        }
        */
    }

}