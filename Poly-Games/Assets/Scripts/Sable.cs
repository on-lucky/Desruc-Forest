using UnityEngine;
using System.Collections;

public class Sable : MonoBehaviour {

    // Use this for initialization

    public GameObject pere, aura;
    bool corrupted, isPere;

    void Update()
    {
        if (isPere && !corrupted)
        {
            pere.GetComponent<PlayerPhysics>().isInSable = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "pereBody")
        {
            isPere = true;
        }
        if (col.gameObject.tag == "aura")
        {
            corrupted = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "pereBody")
        {
            isPere = false;
        }
        if (col.gameObject.tag == "aura")
        {
            corrupted = false;
        }
    }
}
