using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider _bgmVolumeSlider;
    public Slider _sfxVolumeSlider;

    void Start()
    {
        _bgmVolumeSlider.value = SoundManager.Instance.GetBGMVolume;
        _sfxVolumeSlider.value = SoundManager.Instance.GetSFXVolume;
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void OnBGMVolumeChanged(float value)
    {
        SoundManager.Instance.SetBGMVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }

    public void OnTitleButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
