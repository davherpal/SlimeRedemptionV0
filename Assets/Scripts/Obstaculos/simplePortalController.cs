using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplePortalController : MonoBehaviour
{
    public Transform otherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<audioController>().Play("portalSound");
            collision.gameObject.transform.position = otherPortal.position;
            collision.gameObject.GetComponent<SlimeController>().moreJumps = collision.gameObject.GetComponent<SlimeController>().moreJumpsValue;
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.transform.position = otherPortal.position;
        }
    }
}
