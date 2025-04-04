using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCheck : MonoBehaviour
{
    public static int score = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectible>() != null)
        {
            score++;
            if (score <= 3)
            {
                Collectible.spawning = true;
            } else {
                Debug.Log("Score MAX");
            }
            
            StartCoroutine(DestroyPrefab());
        }
    }

    IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(2);
        Destroy(Collectible.Instance);
    }
}
