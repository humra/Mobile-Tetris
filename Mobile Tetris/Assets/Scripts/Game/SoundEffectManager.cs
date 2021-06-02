using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioClip movePiece;
    [SerializeField]
    private AudioClip rotatePiece;
    [SerializeField]
    private AudioClip removeRow;
    [SerializeField]
    private AudioClip gameOver;
    [SerializeField]
    private AudioSource soundSrc;

    private bool sound = true;
    private bool music = true;

    private void Start()
    {
        SetMusicAndSounds();
    }

    private void SetMusicAndSounds()
    {
        if (PlayerPrefs.GetInt("MusicVolume") == 0)
        {
            ToggleMusic();
        }
        if (PlayerPrefs.GetInt("SoundVolume") == 0)
        {
            ToggleSound();
        }
    }

    public void ToggleMusic()
    {
        music = !music;

        if (music)
        {
            audioMixer.SetFloat("MusicVolume", 0f);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", -80f);
        }
    }

    public void ToggleSound()
    {
        sound = !sound;

        if (sound)
        {
            audioMixer.SetFloat("SoundVolume", 0f);
        }
        else
        {
            audioMixer.SetFloat("SoundVolume", -80f);
        }
    }
}
