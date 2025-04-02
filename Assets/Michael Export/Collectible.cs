using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static Collectible Instance;
    public GameObject prefab;
    [HideInInspector] public Rigidbody rb;

    public static bool spawning = false;
    private Vector3 spawnLoc;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        spawnAtRandom();
    }

    void spawnAtRandom()
    {
        spawnLoc = GameObject.Find("Spawn Zone (" + Random.Range(0, 30).ToString() + ")").transform.position;
        if (spawning == true)
        {
            Instantiate(prefab, spawnLoc, Quaternion.identity);
        }
        spawning = false;
    } 
}
