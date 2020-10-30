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

public class LevelSpawner : CustomComponent
{
    public Transform[] levels;
    public float speed = 2f;

    Camera mainCamera;
    Vector3 screenPos;

    Transform onScreen, nextLevel;

    List<Transform> spawnedLevel = new List<Transform>();

    public override void CallInAwake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    public override void CallInStart()
    {
        InitLevel();
    }

    public override void CallInUpdate()
    {
        if (IsEnabled == false) return;
        ScrollLevel();
    }

    void InitLevel() 
    {
        onScreen = Instantiate(levels[0], transform.position, Quaternion.identity);
        onScreen.SetParent(transform);

        nextLevel = Instantiate(GetLevel(), onScreen.GetChild(0).position, Quaternion.identity);
        nextLevel.SetParent(transform);

        spawnedLevel.Add(onScreen);
        spawnedLevel.Add(nextLevel);
    }

    void ScrollLevel() 
    {
        try 
        {
            foreach (Transform tf in spawnedLevel)
            {
                tf.position -= new Vector3(speed, 0, 0) * Time.deltaTime;

                screenPos = mainCamera.WorldToScreenPoint(tf.GetChild(0).transform.position);

                if (screenPos.x <= 0) SpawnLevel();
            }
        } catch(System.Exception) { }
    }

    void SpawnLevel() 
    {
        spawnedLevel.Remove(onScreen);
        Destroy(onScreen.gameObject);

        onScreen = nextLevel;
        nextLevel = Instantiate(GetLevel(), onScreen.GetChild(0).position, Quaternion.identity);
        nextLevel.SetParent(transform);

        spawnedLevel.Add(nextLevel);
    }

    Transform GetLevel() 
    {
        return levels[Random.Range(0, levels.Length)];
    }
}
