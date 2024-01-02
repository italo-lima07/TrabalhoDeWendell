using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    void Start()
    {
        StartCoroutine(PlayBackgroundMusicWithDelay());
    }

    IEnumerator PlayBackgroundMusicWithDelay()
    {
        yield return new WaitForSeconds(10f);
        backgroundMusic.Play();
    }
}
