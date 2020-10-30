/*
 * Author Information
 * Nama	: Nauval Muhammad Firdaus
 * NIM	: 2301906331
 * Kelas	: LB04 (Kelas Kecil) / MA04 (Kelas Besar)
 * Matkul	: Game Programming (GAME6069)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static string chipAds = "chips", distAds = "dist";
    static string h_chips = "h_chips", h_dist = "h_dist";
    static bool isNewHScore = false;
    
    public bool reset = false;
    public Text chipsTxt, distanceTxt;

    void Setup() 
    {
        if (reset) PlayerPrefs.DeleteAll();

        IsNewHighScore = false;
        chipsTxt.text = GetHChips() + " chips";
        distanceTxt.text = GetHDistance() + " m\nTraveled";
    }

    private void Awake()
    {
        Setup();
    }

    public static bool IsNewHighScore 
    {
        get { return isNewHScore; }
        set { isNewHScore = value; }
    }

    public static void SaveScore(int chips, int dist) 
    {
        PlayerPrefs.SetInt(chipAds, chips);
        PlayerPrefs.SetInt(distAds, dist);

        if (GetHChips() < chips)
        {
            PlayerPrefs.SetInt(h_chips, chips);
            IsNewHighScore = true;
        }

        if (GetHDistance() < dist)
        {
            PlayerPrefs.SetInt(h_dist, dist);
            IsNewHighScore = true;
        }
    }

    public static int GetHChips() { return PlayerPrefs.GetInt(h_chips); }
    public static int GetHDistance() { return PlayerPrefs.GetInt(h_dist); }
    public static int GetChips() { return PlayerPrefs.GetInt(chipAds);  }
    public static int GetDistance() { return PlayerPrefs.GetInt(distAds); }
}
