using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioClip movePieceAudio;
    [SerializeField]
    private AudioClip rotatePieceAudio;
    [SerializeField]
    private AudioClip removeRowAudio;
    [SerializeField]
    private AudioClip gameOverAudio;
    [SerializeField]
    private AudioSource soundSrc;
    [SerializeField]
    private Sprite musicOnSprite;
    [SerializeField]
    private Sprite musicOffSprite;
    [SerializeField]
    private Sprite soundOnSprite;
    [SerializeField]
    private Sprite soundOffSprite;
    [SerializeField]
    private Button musicBtn;
    [SerializeField]
    private Button soundBtn;

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
            musicBtn.GetComponent<Image>().sprite = musicOnSprite;
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", -80f);
            musicBtn.GetComponent<Image>().sprite = musicOffSprite;
        }
    }

    public void ToggleSound()
    {
        sound = !sound;

        if (sound)
        {
            audioMixer.SetFloat("SoundVolume", 0f);
            soundBtn.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            audioMixer.SetFloat("SoundVolume", -80f);
            soundBtn.GetComponent<Image>().sprite = soundOffSprite;
        }
    }

    public void PlaySoundEffect(SoundEffects soundEffect)
    {
        switch(soundEffect)
        {
            case SoundEffects.MovePiece:
                soundSrc.PlayOneShot(movePieceAudio);
                break;

            case SoundEffects.RotatePiece:
                soundSrc.PlayOneShot(rotatePieceAudio);
                break;

            case SoundEffects.RemoveRow:
                soundSrc.PlayOneShot(removeRowAudio);
                break;

            case SoundEffects.GameOver:
                soundSrc.PlayOneShot(gameOverAudio);
                break;
        }
    }
}

public enum SoundEffects
{
    MovePiece,
    RotatePiece,
    RemoveRow,
    GameOver
}