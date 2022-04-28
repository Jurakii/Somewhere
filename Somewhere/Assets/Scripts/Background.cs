using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{

	public Transform[] backgrounds;     //Arrary (list) of all the back and foregrounds to be parrallaxed
	private float[] parallaxScales;     // The proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1f;        // how smooth the parallax is going to be. Make sure to set this above 0

	public Transform cams;              //reference to the main cameras transform
	private Vector3 previousCamPos;     //the position of the camera in teh previous frame

	//Is called before start(). Great for references
	/*void Awake()
	{
		//set up camera reference
		cams = Camera.main.transform;
	}
	*/
	// Use this for initialization
	void Start()
	{
		//The previous fram the current fram's camera position
		previousCamPos = cams.position;

		//asigning coresponding parallaxScales
		parallaxScales = new float[backgrounds.Length];

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
			// the parallax is the opposite of the camera  movement becuase teh previous frame multiplied bye the scale
			float parallax = (previousCamPos.x - cams.position.x) * parallaxScales[i];
			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			// create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			// fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

		}

		// set the priviousCamPos to teh camera's position at the end of the frame
		previousCamPos = cams.position;


	}


}