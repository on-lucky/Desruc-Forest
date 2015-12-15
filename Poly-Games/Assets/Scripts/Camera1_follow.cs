using UnityEngine;
using System.Collections;

public class Camera1_follow : MonoBehaviour {


    public GameObject fille, pere;
    public float distance = 10.0f, smoothFactor = 5f;
    Vector3 velocity = Vector3.zero, center;
    GameController gameController;


    void Start() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if (gameController.startingLives - gameController.currentLives != 0)
            transform.position = gameController.oldCameraPosition;
    }


    void Update()
    {
        center = (fille.transform.position + pere.transform.position) / 2;
            velocity = new Vector3((center.x - transform.position.x) / smoothFactor,
                               (center.y - transform.position.y) / smoothFactor,
                                0);
            transform.position += velocity;
            gameController.oldCameraPosition = transform.position;
    }
}
