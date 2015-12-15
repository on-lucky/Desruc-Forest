using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;    //array list of all the backgrounds to be parallaxed
    private float[] parallaxScales;    // proporion of camera movement
    public float smoothing = 1f;            //how smoth the parallax will be. have to be set above 0

    private Transform cam;           // reference to the main camera transform
    private Vector3 previousCamPos;   // position of the camera in the previous position

    // Is called before Start(). Great for references.
    void Awake()
    {
        // set up the camera reference
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        // The previous frame had the current frames position
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        // Assigning coresponding parallaxScales
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the oposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the surrent position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime); ;
        }

        //setthe previous cam pos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
