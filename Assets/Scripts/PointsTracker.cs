using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PointsTracker : MonoBehaviour
{
    public static int scoreNum = 3;
    public Image[] scoreBar;

    public Sprite point;
    public Sprite emptyPoint;

    // Update is called once per frame
    void Update()
    {
        // GetComponent<TMP_Text>().text = CollectibleCheck.score.ToString();
        if (CollectibleCheck.score > scoreNum){
                CollectibleCheck.score = scoreNum;
            }

        for (int i = 0; i < scoreBar.Length; i++)
        {
            if (i > CollectibleCheck.score) {
                scoreBar[i].sprite = point;
            } else {
                scoreBar[i].sprite = emptyPoint;
            }

            if (i < scoreNum){
                scoreBar[i].enabled = true;
            } else {
                scoreBar[i].enabled = true;
            }
        }

        
    }
}
