using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HintSettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle hintsToggle;
    [SerializeField] public Slider delaySlider;
    [SerializeField] private TMP_Text delayLabel;
    [SerializeField] private Toggle WordListToggle;

    void OnEnable()
    {
        hintsToggle.SetIsOnWithoutNotify(HintSettings.Enabled);
        delaySlider.SetValueWithoutNotify(HintSettings.Delay);
        delaySlider.interactable = HintSettings.Enabled;
        WordListToggle.SetIsOnWithoutNotify(HintSettings.showWordList);
        UpdateLabel(HintSettings.Delay);
    }
    public void OnToggleChanged(bool isOn)
    {
        HintSettings.Enabled = isOn;
        delaySlider.interactable = isOn;
    }
    public void OnDelayChanged(float value)
    {
        HintSettings.Delay = value;
        UpdateLabel(value);
    }

    public void OnWordListToggleChanged(bool isOn)
{
    HintSettings.showWordList = isOn;
}

    private void UpdateLabel(float value)
    {
        delayLabel.text = Mathf.RoundToInt(value) + " sek";
    }
}

