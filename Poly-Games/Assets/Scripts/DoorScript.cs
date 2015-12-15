using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public int ID;
    bool triggered;
    public static GameObject[] buttons;


    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("bouton");
    }

	void Update () {

        triggered = false;
        foreach (GameObject go in buttons)
        {
            if (go.GetComponent<ButtonScript>().ID == ID && go.GetComponent<ButtonScript>().triggered > 0)
            {
                triggered = true;
            }
        }

        if (triggered)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
