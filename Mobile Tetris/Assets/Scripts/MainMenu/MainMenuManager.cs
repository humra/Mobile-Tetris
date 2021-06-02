using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
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
    [SerializeField]
    private AudioMixer audioMixer;

    private bool music = true;
    private bool sound = true;

    private void Start()
    {
        SetMusicAndSound();
    }

    private void SetMusicAndSound()
    {
        if(PlayerPrefs.GetInt("MusicVolume") == 0)
        {
            ToggleMusic();
        }
        if(PlayerPrefs.GetInt("SoundVolume") == 0)
        {
            ToggleSound();
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToggleMusic()
    {
        music = !music;

        if(music)
        {
            musicBtn.GetComponent<Image>().sprite = musicOnSprite;
            PlayerPrefs.SetInt("MusicVolume", 1);
        }
        else
        {
            musicBtn.GetComponent<Image>().sprite = musicOffSprite;
            PlayerPrefs.SetInt("MusicVolume", 0);
        }
    }

    public void ToggleSound()
    {
        sound = !sound;

        if(sound)
        {
            soundBtn.GetComponent<Image>().sprite = soundOnSprite;
            audioMixer.SetFloat("SoundVolume", 0f);
            PlayerPrefs.SetInt("SoundVolume", 1);
        }
        else
        {
            soundBtn.GetComponent<Image>().sprite = soundOffSprite;
            audioMixer.SetFloat("SoundVolume", -80f);
            PlayerPrefs.SetInt("SoundVolume", 0);
        }
    }
}
