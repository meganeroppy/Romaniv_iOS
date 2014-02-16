using UnityEngine;
using System.Collections;

public class BackGround_Run: MonoBehaviour {

	public enum METHOD{BY_SCORE, BY_DATE, BY_MODE};
	public METHOD changeMethod = METHOD.BY_DATE;
	private enum HOUR{DAYTIME, SUNSET, NIGHT};
	private HOUR cur_hour;
	public GameObject daytime;
	public GameObject sunset;
	public GameObject night;
	private GameObject cur_BG;

	
	// Use this for initialization
	void Start () {

		if(changeMethod == METHOD.BY_DATE){
			float cur_hour = System.DateTime.Now.Hour;
			if(cur_hour >= 6 && cur_hour < 15){
				cur_BG = Instantiate(daytime, this.transform.position, this.transform.rotation) as GameObject;
				cur_BG.transform.parent = this.transform;
			}else if(cur_hour >= 15 && cur_hour < 20){
				cur_BG = Instantiate(sunset, this.transform.position, this.transform.rotation) as GameObject;
				cur_BG.transform.parent = this.transform;
			}else{
				cur_BG = Instantiate(night, this.transform.position, this.transform.rotation) as GameObject;
				cur_BG.transform.parent = this.transform;
			}
		}else if(changeMethod == METHOD.BY_SCORE){
			cur_hour = HOUR.DAYTIME;
		}
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("z")){
			Destroy(cur_BG.gameObject);
			cur_BG = Instantiate(daytime, this.transform.position, this.transform.rotation) as GameObject;
			cur_BG.transform.parent = this.transform;
		}
		if(Input.GetKeyDown("x")){
			Destroy(cur_BG.gameObject);
			cur_BG = Instantiate(sunset, this.transform.position, this.transform.rotation) as GameObject;
			cur_BG.transform.parent = this.transform;
		}
		if(Input.GetKeyDown("c")){
			Destroy(cur_BG.gameObject);
			cur_BG = Instantiate(night, this.transform.position, this.transform.rotation) as GameObject;
			cur_BG.transform.parent = this.transform;
		}
	}

	public void switchPic(){
		if(changeMethod == METHOD.BY_SCORE){
			switch(cur_hour){
				case HOUR.DAYTIME:
					cur_hour = HOUR.SUNSET;
					Destroy(cur_BG.gameObject);
					cur_BG = Instantiate( sunset, this.transform.position, this.transform.rotation) as GameObject;
					cur_BG.transform.parent = this.transform;
					break;
				case  HOUR.SUNSET:
					cur_hour = HOUR.NIGHT;
					Destroy(cur_BG.gameObject);
					cur_BG = Instantiate( night, this.transform.position, this.transform.rotation) as GameObject;
					cur_BG.transform.parent = this.transform;
					break;
				case HOUR.NIGHT:
					Destroy(cur_BG.gameObject);
					cur_hour = HOUR.DAYTIME;
					cur_BG = Instantiate( daytime, this.transform.position, this.transform.rotation) as GameObject;
					cur_BG.transform.parent = this.transform;
					break;
				default:
					break;
			}
		}
	}
}
