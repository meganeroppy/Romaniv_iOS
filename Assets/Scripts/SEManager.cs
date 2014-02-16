using UnityEngine;
using System.Collections;

public class SEManager : MonoBehaviour {
	
	private AudioSource BGM;
	public AudioClip slap_se;

	// Use this for initialization
	void Start () {
		BGM = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySE(string clip){
		switch(clip){
		case "slap":
			BGM.PlayOneShot(slap_se);

			break;
		default:
			break;
		}
	}
}
