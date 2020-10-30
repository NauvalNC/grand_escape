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

public class Gear : MonoBehaviour
{
    public float gearSpeed = 5f;

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, gearSpeed) * Time.deltaTime;
    }
}
