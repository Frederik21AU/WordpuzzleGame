using System;
using TMPro;
using UnityEngine;

public class WordStreak : MonoBehaviour
{
    public TMP_Text streaktext;

    const string LastPlayedKey = "LastPlayedDate";
    const string StreakKey = "StreakCount";
    int currentStreak;

    //Læser currrent Saved values
    void Start()
    {
        bool hasPlayedBefore = PlayerPrefs.HasKey(LastPlayedKey);

        if (hasPlayedBefore)
        {
            string savedDataString = PlayerPrefs.GetString(LastPlayedKey);
            DateTime LastPlayedDate = DateTime.Parse(savedDataString);
            currentStreak = PlayerPrefs.GetInt(StreakKey);

            DateTime thisDay = DateTime.Today;
            int DaySince = (thisDay - LastPlayedDate).Days;


            if (DaySince == 0) {

            }

            else if(DaySince == 1) {

                currentStreak++;
            }

            else
            {
                currentStreak = 1;
            }

        }
        else 
        {
            currentStreak = 1;
        }

        PlayerPrefs.SetInt(StreakKey, currentStreak);
        PlayerPrefs.SetString(LastPlayedKey, DateTime.Today.ToString());
        // brug nederste kode til at vise counter funktionen til testen. giver en "fake" dag
        //PlayerPrefs.SetString(LastPlayedKey, DateTime.Today.AddDays(-1).ToString());
        PlayerPrefs.Save();

        streaktext.text = currentStreak.ToString();
    }
}