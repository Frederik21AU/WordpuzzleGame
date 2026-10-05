using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintSettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle hintsToggle;
    [SerializeField] public Slider delaySlider;
    [SerializeField] private TMP_Text delayLabel;

    void OnEnable()
    {
        hintsToggle.SetIsOnWithoutNotify(HintSettings.Enabled);
        delaySlider.SetValueWithoutNotify(HintSettings.Delay);
        delaySlider.interactable = HintSettings.Enabled;
        UpdateLabel(HintSettings.Delay);
    }
    public void OnToggleChanged(bool isOn)
    {
        HintSettings.Enabled = isOn;
        delaySlider.interactable = isOn;
    }
    public void OnDelayChanged(float value)
    {
        Debug.Log("OnDelayChanged called with: " + value + ", label: " + delayLabel);
        HintSettings.Delay = value;
        UpdateLabel(value);
    }

    private void UpdateLabel(float value)
    {
        delayLabel.text = Mathf.RoundToInt(value) + " sek";
    }
}

