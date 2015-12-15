using UnityEngine;
using System.Collections;

public class TotemScript : MonoBehaviour {


    public float descentSpeed, sideSpeed;
    public GameObject pere, fille;
    public GUIText gameOverText;
    GameController gameController;
    int level, compteurDirection, dir = 1;
    public int nbFramesDir = 10;
    Vector3 targetPos, velocity;


    void Start () {
        compteurDirection = nbFramesDir / 2;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        level = gameController.level;

        if (gameController.startingLives - gameController.currentLives != 0)
            transform.position = gameController.oldTotemPosition;

        targetPos = new Vector3(gameController.initTotemPos.x, gameController.initTotemPos.y - (float)(gameController.startingLives - gameController.currentLives), gameController.initTotemPos.z);
	}
	

	void Update () {

        checkResetLevel();
        gameController.oldTotemPosition = transform.position;
    }

    public void Descend()
    {
        if (transform.position.y > targetPos.y && transform.position.y - descentSpeed * Time.deltaTime > targetPos.y)
        {
            if(compteurDirection < nbFramesDir)
            {
                compteurDirection++;
            }
            else
            {
                dir = -dir;
                compteurDirection = 0;
            }

            velocity = new Vector3(dir * sideSpeed, -descentSpeed, 0f) * Time.deltaTime;
            transform.position += velocity;
        }
        else
        {
            transform.position = targetPos;
            gameController.totemUpdated = true;
        }
    }

    bool isPlayerDead()
    {
        if (pere.GetComponent<PlayerPhysics>().isDead || fille.GetComponent<PlayerPhysics>().isDead)
            return true;
        return false;
    }
    void checkResetLevel()
    {
        if (Input.GetKeyDown("r"))
        {
            if (isPlayerDead())
            {
                gameController.currentLives -= 1;
                if (gameController.currentLives == 0)
                {
                    //gameOver
                }
                gameController.totemUpdated = false;
            }
            Application.LoadLevel(level);

        }
    }

}
