using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = player.position;
        transform.position = targetPos;
    }
}

