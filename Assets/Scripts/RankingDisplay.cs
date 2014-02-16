using UnityEngine;
using System.Collections;

public class RankingDisplay : MonoBehaviour {

	private enum INDEX{ID, KE, CM};
	private float w;
	private float h;
	private float margin_side;
	private float margin_updown;
	private uint[] bestRecord;

	// Use this for initialization
	public Texture2D btn_top;
	
	private float slct_btn_width;// = 360.0f;
	private float slct_btn_height;// = 100.0f;

	public Texture2D tex_saikou;
	public Texture2D tex_score;
	
	public Texture2D[] tex_num = new Texture2D[10];
	public Texture2D tex_ke;

	public Texture2D tex_hon;
	private float size_font_score;
	private uint digit = 1;
	
	private const float TEX_LATIO = 0.2f;
	private const float TEX_LATIO_SMALL = 0.12f;
	
	//advance
	public Texture2D tex_hitohada;
	public Texture2D tex_cm;

	//game object
	public GameObject recordReader;
	private GameObject recordReaderPrefab;

	/////////////////////////////////////////////
	private string tmpString;
	private string[] tmpStrings; 
	////////////////////////////////////////////////


	void Start () {
		w = TitleScreen.w;
		h = TitleScreen.h;
		slct_btn_width = w * 0.30f;
		slct_btn_height = h * 0.20f;
		margin_side = GameController.margin_side;
		margin_updown = GameController.margin_updown;
		recordReaderPrefab = Instantiate(recordReader) as GameObject;
		this.bestRecord = recordReaderPrefab.GetComponent<RecordReader>().GetBestRecord(); 
	//	Debug.Log(this.bestRecord[1].ToString() + " : " + this.bestRecord[2].ToString() );
	//	Debug.Log(this.bestRecord[0]);
	//	Debug.Log(this.bestRecord[1]);
	//	Debug.Log(this.bestRecord[2]);
	}
	
	void OnGUI(){
		//UI BACK ROUND
		GUI.Box(new Rect (margin_side, margin_updown, w - (margin_side * 2), h - (margin_updown * 2)), " ");

		GUI.Box(new Rect (w * 0.4f - (w * TEX_LATIO * 0.5f), h * 0.12f, w * TEX_LATIO, h * TEX_LATIO), tex_saikou, GUIStyle.none);
		GUI.Box(new Rect (w * 0.6f - (w * TEX_LATIO * 0.5f), h * 0.12f, w * TEX_LATIO, h * TEX_LATIO), tex_score, GUIStyle.none);
		
		// score : ke
		GUI.Box(new Rect(w * 0.25f, h * 0.35f, w * TEX_LATIO_SMALL, h * TEX_LATIO_SMALL), tex_ke, GUIStyle.none);

		uint best_ke = this.bestRecord[(int)INDEX.KE];

		digit = getDigits(best_ke);
		
		for(uint i = digit ; i != 0 ; i--){
			if(i > 1 ){	
				uint tmp = best_ke;
				//Debug.Log(tmp);
				
				for(uint j = i ; j > 1 ; j--){
					tmp /= 10;
				}
				tmp %= 10;
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.35f, w * TEX_LATIO_SMALL, h * TEX_LATIO_SMALL), tex_num[tmp], GUIStyle.none);
				
			}else{
				uint tmp = best_ke;
				tmp %= 10;
				
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.35f, w * TEX_LATIO_SMALL, h * TEX_LATIO_SMALL), tex_num[best_ke % 10], GUIStyle.none);
			}
		}
		// tex_hon
		GUI.Box(new Rect(w * 0.65f, h * 0.35f, w * TEX_LATIO_SMALL, h * TEX_LATIO_SMALL), tex_hon, GUIStyle.none);
		
		// score hitohada
		GUI.Box(new Rect(w * 0.25f, h * 0.5f, w * TEX_LATIO_SMALL * 1.0f, h * TEX_LATIO_SMALL * 1.0f), tex_hitohada, GUIStyle.none);
		uint best_cm = this.bestRecord[(int)INDEX.CM];

		digit = getDigits(best_cm);
		
		for(uint i = digit ; i != 0 ; i--){
			if(i > 1 ){	
				uint tmp = best_cm;

				for(uint j = i ; j > 1 ; j--){
					tmp /= 10;
				}
				tmp %= 10;
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.5f, w * TEX_LATIO_SMALL, h * TEX_LATIO_SMALL), tex_num[tmp], GUIStyle.none);
				
			}else{
				uint tmp = best_cm;
				tmp %= 10;
				
				GUI.Box(new Rect(w * 0.6f - ( w * 0.044f * i), h * 0.5f, w * TEX_LATIO_SMALL, h * TEX_LATIO_SMALL), tex_num[best_cm % 10], GUIStyle.none);
			}
		}

		// tex_cm
		GUI.Box(new Rect(w * 0.65f, h *  0.5f, w * TEX_LATIO_SMALL * 1.2f, h * TEX_LATIO_SMALL * 1.2f), tex_cm, GUIStyle.none);

		// quit button
		if(GUI.Button(new Rect(w * 0.5f - (slct_btn_width * 0.5f), h * 0.65f, slct_btn_width, slct_btn_height), btn_top, GUIStyle.none)){
			Application.LoadLevel("title");
		}
	}
	
	uint getDigits(uint num){
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
