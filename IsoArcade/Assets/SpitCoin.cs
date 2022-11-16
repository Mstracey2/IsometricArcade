using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitCoin : Clickable
{
    public GameObject coin;
    public GameObject location;

    private void Start()
    {
        inheritedFunction = SpawnCoin;
    }

    public void SpawnCoin()
    {
        GameObject clone = Instantiate(coin, location.transform.position, location.transform.rotation);

        clone.GetComponent<Rigidbody>().AddForce(transform.up * 500);
        clone.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) * 50);
    }
}
