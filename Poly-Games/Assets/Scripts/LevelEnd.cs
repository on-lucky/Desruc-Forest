using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

    bool isPere, isFille, levelCompleted;
    int level;

    void Start () {
        level = GameObject.Find("GameController").GetComponent<GameController>().level;
    }

    void Update()
    {
        if (isPere && isFille)
        {
            Application.LoadLevel(level + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "filleBody")
        {
            isFille = true;
        }
        if (col.gameObject.tag == "pereBody")
        {
            isPere = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "filleBody")
        {
            isFille = false;
        }
        if (col.gameObject.tag == "pereBody")
        {
            isPere = false;
        }
    }
}


