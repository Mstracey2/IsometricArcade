using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovementPoint : MonoBehaviour
{
    [SerializeField]
    private Transform Child;

    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = Child.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Child.position;

        transform.position += -transform.right * originalPos.x;
        transform.position += -transform.up * originalPos.y;
        transform.position += -transform.forward * originalPos.z;

        originalPos = Child.localPosition;
    }
}
