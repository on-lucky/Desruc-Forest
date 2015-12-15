using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    public int startingLives = 5, currentLives = 4, level = 0;

    public Vector3 oldTotemPosition, initTotemPos, oldCameraPosition;

    public bool totemUpdated = true, isOriginal;

    void Awake()
    {
        GameController OG = GameObject.Find("GameController").GetComponent<GameController>();
        if (instance != null && instance != this || OG.isOriginal && OG!=this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        isOriginal = true;
    }
    void Start()
    {
        initTotemPos = GameObject.Find("totem").transform.position;
        Debug.Log("r pour reset");
    }
    void Update()
    {
        if(!totemUpdated)
            GameObject.Find("totem").GetComponent<TotemScript>().Descend();
    }



    

}

