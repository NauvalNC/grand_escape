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

public class ParallaxBG : CustomComponent
{
    public Transform[] background;
    public float speed = 2f;

    Transform lastBG;

    Vector3 screenPos;
    Vector3 tail;
    Camera mainCam;

    public override void CallInAwake()
    {
        mainCam = FindObjectOfType<Camera>();
    }

    public override void CallInUpdate()
    {
        if (IsEnabled == false) return;
        ScrollBG();
    }

    void ScrollBG() 
    {
        lastBG = background[1];

        for (int i = 0; i < background.Length; i++) 
        {
            background[i].position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            tail = background[i].GetChild(1).transform.position;

            screenPos = mainCam.WorldToScreenPoint(tail);

            if (screenPos.x <= 0) background[i].position = lastBG.GetChild(1).transform.position;

            lastBG = background[i];
        }
    }
}
