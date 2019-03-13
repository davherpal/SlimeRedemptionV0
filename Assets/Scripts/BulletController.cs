using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float time2Destroy = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time2Destroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
