using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveController : MonoBehaviour
{
    public float speed;
    private float time;

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y + time, transform.position.z);
    }
}