
using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {

	//status
	public enum SCENE{START, RUN, RESULT, PAUSE};
	public static SCENE cur_scene;

	//system
	private const float DEFAULT_TIMESCALE = 1.0f;
	private float cur_timeScale = DEFAULT_TIMESCALE;

	//property
	public static int score;
	public static float advance;
	public static uint difficulty;

	public static float w;
	public static float h;
	public static float margin_side;
	public static float margin_updown;

	//GUI
	public GameObject GUIController;
	private GameObject GCPrefab;

	//BackGround
	public GameObject backGround_run;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = cur_timeScale;

		w = Screen.width;
		h = Screen.height;
		margin_side = w * 0.05f;
		margin_updown = h * 0.05f; 

		cur_scene = SCENE.START;
		score = 0;
		advance = 0.0f;
		difficulty = 1;
	}
	
	// Update is called once per frame
	void Update () {
		switch(cur_scene){
			case SCENE.START:
				GCPrefab = Instantiate( GUIController ) as GameObject;
				cur_scene = SCENE.RUN;
				break;
			case SCENE.RUN:
				break;
			case SCENE.RESULT:
					Destroy(GCPrefab);
				break;
			case SCENE.PAUSE:
				break;
			default:
				break;
		}
	}

	public static void switchScene(string nextScene){
		if(nextScene == "result"){
			cur_scene = SCENE.RESULT;
		}else if(nextScene == "run"){
			cur_scene = SCENE.RUN;
		}else{
			print ("exception occurred.");
		}
	}

	public static void pause(){
		Time.timeScale = 0;
		cur_scene = SCENE.PAUSE;
	}

	public void resume(){
		Time.timeScale = cur_timeScale;
		cur_scene = SCENE.RUN;
	}

	public void AddScore(int num){
		if(score % 11 == 0 && score != 0){
			//backGround_run.GetComponent<BackGround_Run>().switchPic();
			Time.timeScale += 0.05f;
			cur_timeScale = Time.timeScale;
			score += num; 
		}else{
			score += num;
		}
	}

	public void SetTimeScaleAsDefault(){
		Time.timeScale = DEFAULT_TIMESCALE;
	}

	//public uint GetScore(){
	//	return this.score;
	//}

	void reset(){
		score = 0;
		advance = 0;
	}
}
