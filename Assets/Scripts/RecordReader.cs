using UnityEngine;
using System.Collections;
using System.IO;

public class RecordReader : MonoBehaviour {
	private enum INDEX{ID, KE, CM};
	public TextAsset record_file;
	public string[] record_text;

	//dont wanna do like that..!
	public GameObject rankingDisplay;

	// Use this for initialization
	void Start () {
		record_text = record_file.text.Split('\n');
	//	for(uint v = 0; v < this.record_text.Length ; v++){
	//	Debug.Log("START-> " + this.record_text[v].ToString());
	//	}
		//GetBestRecord();
	}

	void Update(){
	}

	/*
	uint[][] GetAllRecord(){
		uint[][] records = new uint[3][];
		return records;
	}
	*/

	public uint[] GetBestRecord(){
	///////
		record_text = record_file.text.Split('\n');
	//////
		uint[] best_record = new uint[3];
		string[] tmp_record = new string[3];

		// initialize best_record as 0
		for(int i = 0 ; i < 3 ;i++){
			best_record[i] = 0;
		}

		for (uint cur_id = 0; cur_id < record_text.Length ; cur_id++){
			tmp_record = record_text[cur_id].Split(',');

			if(best_record[(int)INDEX.KE] +  best_record[(int)INDEX.CM]  < uint.Parse(tmp_record[(int)INDEX.KE]) + uint.Parse(tmp_record[(int)INDEX.CM])){
				best_record[(int)INDEX.ID] = uint.Parse(tmp_record[(int)INDEX.ID]);
				best_record[(int)INDEX.KE] = uint.Parse(tmp_record[(int)INDEX.KE]);
				best_record[(int)INDEX.CM] = uint.Parse(tmp_record[(int)INDEX.CM]);
			}
		}
		return best_record;
	}

	public void AppendToRecord(string newestRecord){
		FileInfo file = new FileInfo(Application.dataPath + "/Resource/Text/Record.txt");
		StreamWriter sw = file.AppendText();

		//create timestamp as the id of record
		string id = System.DateTime.Now.Month.ToString() +
					System.DateTime.Now.Day.ToString() +
					System.DateTime.Now.Hour.ToString() +
					System.DateTime.Now.Minute.ToString() +
					System.DateTime.Now.Second.ToString();

		string data = "\n" + id + "," + newestRecord;

		sw.Write(data);
		sw.Flush();
		sw.Close(); 
	}
}
