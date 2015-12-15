using UnityEngine;
using System.Collections;

public class Buisson : MonoBehaviour {

    // Use this for initialization

    public GameObject fille, pere;
    bool corrupted, isPere, isFille;


    void Update()
    {
        if (isFille)
        {
            fille.GetComponent<PlayerPhysics>().isDead = true;
        }
        if (isPere && corrupted)
        {
            pere.GetComponent<PlayerPhysics>().isInBuisson = true;

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "filleBody")
        {
            isFille = true;
        }
        if (col.gameObject.tag == "aura")
        {
            corrupted = true;
        }
        if (col.gameObject.tag == "pereBody")
        {
            isPere = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "aura")
        {
            corrupted = false;
        }
        if (col.gameObject.tag == "pereBody")
        {
            isPere = false;
        }
    }
}
