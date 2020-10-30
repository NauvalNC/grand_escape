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
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator mainMenuAC, howToAC, creditAC, introAC;
    public AudioSource buttonClick;
    public bool reset = false;
    string firstBoot = "firstBoot";

    private void Start()
    {
        if (reset) PlayerPrefs.SetInt(firstBoot, 0);
    }

    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (Input.GetKeyDown(KeyCode.Space)) StartGame();
    }

    public void StartGame() 
    {
        buttonClick.Play();

        StartCoroutine(IStartGame());
    }

    bool toggleStart = true;
    IEnumerator IStartGame() 
    {
        toggleStart = !toggleStart;
        mainMenuAC.SetBool("toggle", toggleStart);
        float wait = mainMenuAC.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(wait);

        if (PlayerPrefs.GetInt("firstBoot") == 1) SceneManager.LoadScene("Gameplay");
        else Intro();
    }

    bool toggleIntro = false;
    public void Intro() 
    {
        if (introAC.gameObject.activeInHierarchy == false) introAC.gameObject.SetActive(true);
        toggleIntro = !toggleIntro;
        introAC.SetBool("toggle", toggleIntro);
    }

    public void PlayIntro() 
    {
        StartCoroutine(IntroOption("Intro"));
    }

    public void JumpToGameplay() 
    {
        StartCoroutine(IntroOption("Gameplay"));
    }

    IEnumerator IntroOption(string scene) 
    {
        Intro();
        yield return new WaitForSeconds(introAC.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(scene);
    }

    bool toggleHowToPlay = false;
    public void HowToPlay() 
    {
        if (howToAC.gameObject.activeInHierarchy == false) howToAC.gameObject.SetActive(true);
        toggleHowToPlay = !toggleHowToPlay;
        howToAC.SetBool("toggle", toggleHowToPlay);
    }

    bool toggleCredit = false;
    public void Credit() 
    {
        if (creditAC.gameObject.activeInHierarchy == false) creditAC.gameObject.SetActive(true);
        toggleCredit = !toggleCredit;
        creditAC.SetBool("toggle", toggleCredit);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
