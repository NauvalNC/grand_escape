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

public class Player : CustomComponent
{
    public float gravityScale = 2f;
    public float outFactor = 5f;
    float gravityMode = -1;

    Camera mainCam;
    Vector3 screenPos;
    Animator ac;

    Rigidbody2D rb;

    public AudioSource switchSound;

    public override void CallInAwake()
    {
        mainCam = FindObjectOfType<Camera>();
        ac = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = new Vector2(0, gravityMode * gravityScale);
    }

    public override void CallInUpdate()
    {
        if (IsEnabled == false) return;
        ControlGravity();
        CheckBorder();

        if (rb.velocity.magnitude > 0) ac.Play("idle");
        else ac.Play("run");
    }

    void ControlGravity()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switchSound.Play();
            gravityMode *= -1;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        }

        Physics2D.gravity = new Vector2(0, gravityMode * gravityScale);
    }

    void CheckBorder() 
    {
        screenPos = mainCam.WorldToScreenPoint(transform.position);

        if (screenPos.y < -outFactor || screenPos.y > Screen.height + outFactor) 
        {
            GameManager.Instance.InvokeGameOver();
        }
    }
}
