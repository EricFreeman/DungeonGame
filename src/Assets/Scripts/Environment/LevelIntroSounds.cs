using UnityEngine;
using System.Collections;

public class LevelIntroSounds : MonoBehaviour
{
    public AudioClip Level1IntroSound;

    void PlayLevel1Intro()
    {
        AudioSource.PlayClipAtPoint(Level1IntroSound, transform.position);
    }
}
