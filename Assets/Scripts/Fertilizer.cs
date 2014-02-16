using UnityEngine;
using System.Collections;

public class Fertilizer : MonoBehaviour {

	public GameObject hairPoint;
	private GameObject hairPointPrefab;
	public GameObject obstacles;
	private GameObject obstaclePrefab;
	private RaycastHit2D rootPos;
	private RaycastHit2D subRootPos_left;
	private RaycastHit2D subRootPos_right;
	private Vector2 rayOrigin;
	private Vector2 subRayOrigin_left;
	private Vector2 subRayOrigin_right;
	private const float OFFSET_SUBRAY = 2.5f; 
	private Vector2 rayDirection = new Vector2(0.0f, -10.0f);
	
	public float ObstaclePercent = 25.0f;

	//public GameObject PosChecker_L;
	//public GameObject PosChecker_C;
	//public GameObject PosChecker_R;

	// Use this for initialization
	void Start () {

		int seed = (int)Mathf.Floor(Random.value * 1000.0f % 101.0f); //0 ~ 100

		rayOrigin = this.transform.position;
		subRayOrigin_left = new Vector2(this.transform.position.x - OFFSET_SUBRAY, this.transform.position.y);
		subRayOrigin_right = new Vector2(this.transform.position.x + OFFSET_SUBRAY, this.transform.position.y);

		//GameObject tmp = Instantiate(PosChecker_C, new Vector3(rayOrigin.x, rayOrigin.y, 0.0f), this.transform.rotation) as GameObject;
		//GameObject tmp2 = Instantiate(PosChecker_L, new Vector3(subRayOrigin_left.x, rayOrigin.y, 0.0f), this.transform.rotation) as GameObject;
		//GameObject tmp3 = Instantiate(PosChecker_R, new Vector3(subRayOrigin_right.x, rayOrigin.y, 0.0f), this.transform.rotation) as GameObject;
				 
		rootPos =  Physics2D.Raycast( rayOrigin, new Vector2(0.0f, -10.0f), 100.0f);
		subRootPos_left = Physics2D.Raycast( subRayOrigin_left, new Vector2(0.0f, -10.0f), 100.0f);
		subRootPos_right = Physics2D.Raycast( subRayOrigin_right, new Vector2(0.0f, -10.0f), 100.0f);

		if(seed <= ObstaclePercent){

			float centerPosY;

			// Position
			if(subRootPos_left.point.y >= rootPos.point.y && subRootPos_right.point.y >= rootPos.point.y){	//Valley

				if(subRootPos_left.point.y >= subRootPos_right.point.y){
					centerPosY = subRootPos_right.point.y;
				}else{
					centerPosY = subRootPos_right.point.y;
				}
			}else if(subRootPos_left.point.y <= rootPos.point.y && subRootPos_right.point.y >= rootPos.point.y){	//Acsend

				centerPosY = (subRootPos_left.point.y + rootPos.point.y ) /2.0f;

			}else if(subRootPos_left.point.y >= rootPos.point.y && subRootPos_right.point.y <= rootPos.point.y){	//Decsend

				centerPosY = (subRootPos_left.point.y + rootPos.point.y ) /2.0f;

			}else{														//Mountain

				if(subRootPos_left.point.y >= subRootPos_right.point.y){
					centerPosY = subRootPos_right.point.y;
				}else{
					centerPosY = subRootPos_right.point.y;
				}
			}
			//Rotation
			float carve =  Mathf.Atan2((float)subRootPos_left.point.y - (float)subRootPos_right.point.y, (float)subRootPos_left.point.x - (float)subRootPos_right.point.x);
			carve = (carve * 180.0f) / Mathf.PI - 180.0f;

			obstaclePrefab = Instantiate(obstacles, new Vector2(rootPos.point.x, centerPosY), Quaternion.Euler(0.0f, 0.0f, carve)) as GameObject;

		}else{
			hairPointPrefab = Instantiate(hairPoint, rootPos.point, this.transform.rotation) as GameObject;
		}
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
