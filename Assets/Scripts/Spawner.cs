using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRate = 10f;
    public GameObject hexagonPrefab;
    public float nextTimeToSpawn = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTimeToSpawn)
        {
            Instantiate(hexagonPrefab, Vector3.zero, Quaternion.identity);
            nextTimeToSpawn = Time.time + .3f / spawnRate;
        }
    }
}
