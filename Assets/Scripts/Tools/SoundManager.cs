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

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = FindObjectOfType<SoundManager>();
        }

        DontDestroyOnLoad(gameObject);
    }
}
