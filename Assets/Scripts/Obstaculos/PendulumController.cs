using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
 
    public Rigidbody2D body2d;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityTreshold;
    // Start is called before the first frame update
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = velocityTreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    public void Push()
    {
        if(transform.rotation.z > 0
            && transform.rotation.z < rightPushRange
            && (body2d.angularVelocity > 0)
            && body2d.angularVelocity< velocityTreshold)
        {
            body2d.angularVelocity = velocityTreshold;
        }
        else if (transform.rotation.z < 0
            && transform.rotation.z > leftPushRange
            && (body2d.angularVelocity <0)
            && body2d.angularVelocity > velocityTreshold * -1)
        {
            body2d.angularVelocity = velocityTreshold * -1;
        }
            
         
    }
}
