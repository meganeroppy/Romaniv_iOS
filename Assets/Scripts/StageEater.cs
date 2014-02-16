using UnityEngine;
using System.Collections;

public class StageEater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "ground" || col.tag == "tools" || col.tag == "obstacle" || col.tag == "hair"){
			Destroy(col.gameObject);
		}
	}
}
