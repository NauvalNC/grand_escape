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

public class Intro : MonoBehaviour
{
    public TextTime[] text;
    public UnityEngine.UI.Text fadeText;
    public Animator fadingText;

    public float firstWait = 0.5f;

    private void Start()
    {
        StartCoroutine(IFadingText());
    }

    IEnumerator IFadingText() 
    {
        fadeText.gameObject.SetActive(false);
        yield return new WaitForSeconds(firstWait);
        fadeText.gameObject.SetActive(true);

        for (int i = 0; i < text.Length; i++) 
        {
            fadeText.text = text[i].text;

            fadingText.Play("in");
            
            yield return new WaitForSeconds(fadingText.GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(text[i].time);
            
            fadingText.Play("out");
            yield return new WaitForSeconds(fadingText.GetCurrentAnimatorStateInfo(0).length);
        }
        yield return new WaitForSeconds(firstWait);

        PlayerPrefs.SetInt("firstBoot", 1);

        SceneManager.LoadScene("Gameplay");
    }
}

[System.Serializable]
public class TextTime 
{
    public string text;
    public float time;
}
