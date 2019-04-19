using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalController : MonoBehaviour
{
    public Transform Player;
    public Rigidbody2D rb;
    public Transform otherPortalRight;
    public Transform otherPortalLeft;
    private Vector3 finalPosition;
    GameObject player;

    private void start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<audioController>().Play("portalSound");
            if (rb.velocity.x >= 0)
            {
                finalPosition = otherPortalRight.position;
            }
            else
            {
                finalPosition = otherPortalLeft.position;
            }
            Player.transform.position = finalPosition;
            
            //finalPosition = new Vector3(positionPortalX, transform.position.y, transform.position.z);
            
        }
    }
}
