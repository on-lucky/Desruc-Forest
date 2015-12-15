using UnityEngine;
using System.Collections;

public class LucioleScript : MonoBehaviour {

    // Use this for initialization
    Vector3 PositionInit;
    public float t, speed = 1;

	void Start () {
        PositionInit = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        transform.position = new Vector3(PositionInit.x + Mathf.Cos(t * speed), PositionInit.y + (Mathf.Cos(t * speed) * Mathf.Sin(t * speed)), PositionInit.z);
	}
}
