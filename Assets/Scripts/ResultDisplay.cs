using UnityEngine;
using System.Collections;

public class ResultDisplay : MonoBehaviour {

	private float w;
	private float h;
	private float margin_side;
	private float margin_updown;

	// Use this for initialization
	public Texture2D btn_retry;
	public Texture2D btn_quit;

	public Texture2D tex_score;

	public Texture2D[] tex_num = new Texture2D[10];
	public Texture2D tex_ke;
	//private float size_tex;
	public Texture2D tex_hon;
	private const float TEX_SIZE_LATIO = 0.13f;
	private const float TEX_SIZE_LATIO_SCORE = 0.18f;

	private uint digit = 1;
	
	//advance
	public Texture2D tex_hitohada;
	public Texture2D tex_cm;

	private float slct_btn_width;// = 360.0f;
	private float slct_btn_height;// = 100.0f;

	public GameObject recordReader;
	void Start () {
		w = GameController.w;

		h = GameController.h;
		slct_btn_width = w * 0.32f;
		slct_btn_height = h * 0.22f;
		margin_side = GameController.margin_side;
		margin_updown = GameController.margin_updown;

		GameObject recordReaderPrefab = Instantiate(recordReader) as GameObject;
		recordReaderPrefab.GetComponent<RecordReader>().AppendToRecord(GameController.score.ToString() + ',' + Mathf.Floor(GameController.advance).ToString());
		Destroy(recordReaderPrefab.gameObject);
	}

	void Update () {

		if(Input.GetKey("r")){
			Application.LoadLevel("run");
		}
		if(Input.GetKey("q")){
			Application.LoadLevel("title");
		}
	}
	void OnGUI(){


		// Back Ground
		GUI.Box(new Rect (margin_side, margin_updown, w - (margin_side * 2), h - (margin_updown * 2)), " ");

		//tex_score
		GUI.Box(new Rect(w * 0.5f - (w * TEX_SIZE_LATIO_SCORE * 0.5f), h * 0.15f, w * TEX_SIZE_LATIO_SCORE, h * TEX_SIZE_LATIO_SCORE), tex_score, GUIStyle.none);

		// tex_ke
		GUI.Box(new Rect(w * 0.25f, h * 0.35f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_ke, GUIStyle.none);
		
		digit = getDigits(GameController.score);
		
		for(uint i = digit ; i != 0 ; i--){
			if(i > 1 ){	
				int tmp = (int)GameController.score;
				//Debug.Log(tmp);
				
				for(uint j = i ; j > 1 ; j--){
					tmp /= 10;
				}
				tmp %= 10;
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.35f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_num[tmp], GUIStyle.none);
				
			}else{
				int tmp = (int)GameController.advance;
				tmp %= 10;
				
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.35f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_num[(int)GameController.score % 10], GUIStyle.none);
			}
		}
		
		GUI.Box(new Rect(w * 0.65f, h * 0.35f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_hon, GUIStyle.none);
		
		
		GUI.Box(new Rect(w * 0.25f, h * 0.5f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_hitohada, GUIStyle.none);
		
		digit = getDigits((int)GameController.advance);
		
		for(uint i = digit ; i != 0 ; i--){
			if(i > 1 ){	
				int tmp = (int)GameController.advance;
				//Debug.Log(tmp);
				
				for(uint j = i ; j > 1 ; j--){
					tmp /= 10;
				}
				tmp %= 10;
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.5f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_num[tmp], GUIStyle.none);
				
			}else{
				int tmp = (int)GameController.advance;
				tmp %= 10;
				
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.5f, w * TEX_SIZE_LATIO, h * TEX_SIZE_LATIO), tex_num[(int)GameController.advance % 10], GUIStyle.none);
			}
		}
		
		GUI.Box(new Rect(w * 0.65f, h * 0.5f, w * TEX_SIZE_LATIO , h * TEX_SIZE_LATIO ), tex_cm, GUIStyle.none);
		///
		if(GUI.Button(new Rect(w * 0.3f - (slct_btn_width * 0.5f), h * 0.65f, slct_btn_width, slct_btn_height), btn_retry, GUIStyle.none)){
			Application.LoadLevel("run");
			//				reset ();
		}
		if(GUI.Button(new Rect(w * 0.7f - (slct_btn_width * 0.5f), h * 0.65f, slct_btn_width, slct_btn_height), btn_quit, GUIStyle.none)){
			Application.LoadLevel("title");
		}
	}

	uint getDigits(int num){
		if(num < 10){
			return 1;
		}
		uint numOfDigits = 1;
		while(num >= 10){
			num /= 10;
			numOfDigits++;
		}
		return numOfDigits;
		
	}
}
