using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    [SerializeField] TableMinigameManager manager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoolBall"))
        {
            manager.RemoveFromList(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
