using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Image audioButtonImage;
    public Sprite sound_on;
    public Sprite sound_off;

    public AudioClip daoju;
    public AudioClip door;
    public AudioClip feixing;
    public AudioClip fight;
    public AudioClip shangdian;
    public AudioClip talk;

    public AudioSource gameAudio;
    public AudioSource heroAudio;

    public void playAudio(string audioname)
    {
        switch (audioname)
        {
            case "daoju":
                heroAudio.clip = daoju;
                break;
            case "door":
                heroAudio.clip = door;
                break;
            case "feixing":
                heroAudio.clip = feixing;
                break;
            case "fight":
                heroAudio.clip = fight;
                break;
            case "shangdian":
                heroAudio.clip = shangdian;
                break;
            case "talk":
                heroAudio.clip = talk;
                break;
            default:
                heroAudio.clip = null;
                break;
        }
        heroAudio.Stop();
        heroAudio.Play();
    }

    public void voice()
    {
        GameObject hero = GameObject.Find("Hero").gameObject;
        heroAudio = hero.GetComponent<AudioSource>();
        if (gameAudio.mute == true)
        {
            gameAudio.mute = false;
            heroAudio.mute = false;
            audioButtonImage.sprite = sound_on;
        }
        else
        {
            gameAudio.mute = true;
            heroAudio.mute = true;
            audioButtonImage.sprite = sound_off;
        }
    }
}
