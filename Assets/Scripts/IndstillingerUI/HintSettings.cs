using UnityEngine;

public static class HintSettings
{
    private const string EnabledKey = "hintsEnabled";
    private const string DelayKey = "hintDelay";

    public const float DefaultDelay = 10f;

    public static bool Enabled
    {
        get { return PlayerPrefs.GetInt(EnabledKey, 1) == 1; }
        set
        {
            PlayerPrefs.SetInt(EnabledKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static float Delay
    {
        get { return PlayerPrefs.GetFloat(DelayKey, DefaultDelay); }
        set
        {
            PlayerPrefs.SetFloat(DelayKey, value);
            PlayerPrefs.Save();
        }
    }
}
