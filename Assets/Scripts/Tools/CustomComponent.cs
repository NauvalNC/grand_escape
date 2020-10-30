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

public class CustomComponent : MonoBehaviour
{
    bool isEnabled = true;

    public bool IsEnabled 
    {
        get { return isEnabled; }
        set { isEnabled = value; }
    }

    private void Awake()
    {
        CallInAwake();
    }

    private void Start()
    {
        CallInStart();
    }

    private void Update()
    {
        CallInUpdate();
    }

    public virtual void CallInStart() 
    {
    
    }

    public virtual void CallInAwake() 
    {
    
    }

    public virtual void CallInUpdate() 
    {
    
    }
}
