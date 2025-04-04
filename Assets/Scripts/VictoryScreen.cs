using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject VictoryScreenUI;
    // Start is called before the first frame update
    void Awake()
    {
        VictoryScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CollectibleCheck.score >= 3) {
            VictoryScreenUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
