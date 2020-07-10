using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public GameObject player;
    public AudioSource track, track2;

    public float trackMaxVolume;


    void Awake()
    {
        Application.targetFrameRate = -1;
        current = this;
    }

    private void Start()
    {
        track.volume = 0;
        StartCoroutine(fadeTrackIn(track));
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            track.Stop();
            player.SetActive(false);
            UIManager.current.Fade();

            if (!track2.isPlaying)
            {
                track2.volume = 0;
                track2.Play();
                StartCoroutine(fadeTrackIn(track2));
            }

        }

        else if (!track.isPlaying && !track2.isPlaying)
        {
            //time to end 
            player.SetActive(false);
            UIManager.current.Fade();
            //and then play the hey september track

            track2.volume = 0;
            track2.Play();
            StartCoroutine(fadeTrackIn(track2));

        }

    }


    IEnumerator fadeTrackIn(AudioSource track)
    {
        yield return new WaitForSeconds(0.075f);
        if (track.volume < trackMaxVolume)
        {
            track.volume += 0.01f;
            StartCoroutine(fadeTrackIn(track));
        }

    }


}
