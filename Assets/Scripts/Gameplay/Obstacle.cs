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

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.InvokeGameOver();
        }
    }
}
