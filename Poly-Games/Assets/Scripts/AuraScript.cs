using UnityEngine;
using System.Collections;

public class AuraScript : MonoBehaviour {

    public GameObject fille;
    Camera aura;
    public Camera main;
    private CircleCollider2D auraCollider;
    public float nbCasesAura;
    Vector3 screenPos;

    // Use this for initialization
    void Start () {
        aura = transform.parent.gameObject.GetComponent<Camera>();
        auraCollider = GetComponent<CircleCollider2D>();
        auraCollider.radius = nbCasesAura / 2;
    }
	
	// Update is called once per frame
	void Update () {
        float grandeurAura = nbCasesAura * Screen.height / (main.orthographicSize * 2f);
        screenPos = main.WorldToScreenPoint(fille.transform.position);
        aura.pixelRect = new Rect(screenPos.x - (grandeurAura) / 2, screenPos.y - (grandeurAura) / 2, grandeurAura, grandeurAura);
        aura.orthographicSize = nbCasesAura / 2;
    }
}
