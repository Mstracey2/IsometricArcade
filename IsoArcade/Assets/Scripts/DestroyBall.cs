using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoolBall"))                   //sets the state of the ball to false if trigger is activated
        {
            other.gameObject.SetActive(false);
        }
    }
}
