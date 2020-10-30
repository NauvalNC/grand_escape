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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Animator gameOverAC;
    public Text chipTxt;
    public Text distTxt;
    public GameObject newHScorePanel;

    private void Start()
    {
        chipTxt.text = ScoreManager.GetChips().ToString() + " chips";
        distTxt.text = ScoreManager.GetDistance().ToString() + "m traveled";

        newHScorePanel.SetActive(ScoreManager.IsNewHighScore);
        ScoreManager.IsNewHighScore = false;
    }

    public void MainMenu() 
    {
        StartCoroutine(LoadScene("MainMenu"));
    }

    public void PlayAgain() 
    {
        StartCoroutine(LoadScene("Gameplay"));
    }

    IEnumerator LoadScene(string scene) 
    {
        gameOverAC.SetBool("toggle", false);
        float wait = gameOverAC.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(scene);
    }
}
