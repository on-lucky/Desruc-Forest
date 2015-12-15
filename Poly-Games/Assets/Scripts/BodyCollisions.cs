using UnityEngine;
using System.Collections;

public class BodyCollisions : MonoBehaviour {

    PlayerPhysics playerPhysics;

    void Start()
    {
        playerPhysics = transform.parent.gameObject.GetComponent<PlayerPhysics>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "aura")
        {
            playerPhysics.isInAura = true;
        }
        if (col.gameObject.tag == "vine")
        {
            playerPhysics.isBodyInVine = true;
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "aura")
        {
            playerPhysics.isInAura = false;
        }
        if (col.gameObject.tag == "vine")
        {
            playerPhysics.isBodyInVine = false;
        }
    }

}
