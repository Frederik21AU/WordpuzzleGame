using UnityEngine;
using UnityEngine.UI;

public class SoundIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private const string VolumeKey = "musicVolume"; // same key SoundManager uses
    private const string BeforeMuteKey = "volumeBeforeMute";

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
    }

    void Update()
    {
        iconImage.sprite = AudioListener.volume > 0.001f ? soundOnSprite : soundOffSprite;
    }

    public void OnClick()
    {
        if (AudioListener.volume > 0.001f)
        {
            PlayerPrefs.SetFloat(BeforeMuteKey, AudioListener.volume);
            SetVolume(0f);
        }
        else
        {
            float restore = PlayerPrefs.GetFloat(BeforeMuteKey, 1f);
            SetVolume(restore > 0.001f ? restore : 1f);
        }
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}
