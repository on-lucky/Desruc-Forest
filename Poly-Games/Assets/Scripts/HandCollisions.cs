using UnityEngine;
using System.Collections;

public class HandCollisions : MonoBehaviour {

    PlayerPhysics playerPhysics;

    void Start()
    {
        playerPhysics = transform.parent.gameObject.GetComponent<PlayerPhysics>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "aura")
        {
            playerPhysics.isHandsInAura = true;
        }
        if (col.gameObject.tag == "vine")
        {
            playerPhysics.isHandsInVine = true;
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "aura")
        {
            playerPhysics.isHandsInAura = false;
        }
        if (col.gameObject.tag == "vine")
        {
            playerPhysics.isHandsInVine = false;
        }
    }
}
