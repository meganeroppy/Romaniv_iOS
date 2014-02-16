using UnityEngine;
using System.Collections;

public class Romaniv : MonoBehaviour {

	//system
	private float wait_to_dispResult;

	//private const float TIME_TO_REUSE_SLAP = 1.0f;
	//private float wait_to_slap = 0.0f;
	//private bool SlapIsReady = true;

	//private const float TIME_TO_REUSE_JUMP = 1.0f;
	//private float wait_to_jump = 0.0f;
	//private bool JumpIsReady = true;

	private const float TIME_TO_REUSE = 0.6f;
	private float wait_to_reuse = 0.0f;
	private bool nextActionIsReady = true;

	private const float START_POS_X = 0.0f;
	public bool MUTEKI = false;

	//status
	public float runSpeed = 7.0f;
	public float jumpForce = 650.0f;

	public enum STATUS{RUN, STOP, JUMP, SLAP, DEAD};
	public STATUS cur_status;
	public enum JUMP_STATUS{ACSEND, DESCEND};
	public JUMP_STATUS cur_j_status;

	private const float MAX_SPEED = 5.0f;
	private bool gameOver = false;

	
	//gameover
	public GameObject resultDispley;

	private Animator anim;

	//game objects
	public GameObject attack_zone;
	public GameObject explosion;
	private GameObject ground;
	private CircleCollider2D circleCollider;
	private GameObject mainCamera;
	public GameObject gameController;

	// Use this for initialization
	void Start () {
		mainCamera = transform.FindChild("Main Camera").gameObject;
		cur_j_status = JUMP_STATUS.ACSEND;

		ground = GameObject.FindWithTag("ground");
		circleCollider = this.GetComponent<CircleCollider2D>();

		cur_status = STATUS.JUMP;
		anim = GetComponent<Animator>();
	}

	void Update(){
		if(!nextActionIsReady){
			wait_to_reuse -= 1.0f * Time.deltaTime;
			if(wait_to_reuse <= 0.0f){
				nextActionIsReady = true;
			}
		}
	}
	
	void FixedUpdate () {

		GameController.advance = (this.transform.position.x - START_POS_X) / 10.0f ;

		switch(cur_status){
			case STATUS.RUN:
				transform.Translate(Vector2.right * runSpeed * Time.deltaTime);	//walking right direction
				break;
			case STATUS.STOP:
				break;
			case STATUS.JUMP:
				transform.Translate(Vector2.right * runSpeed * Time.deltaTime);	//walking right direction
				switch(cur_j_status){
						case JUMP_STATUS.ACSEND:
								cur_j_status = JUMP_STATUS.DESCEND;
							break;
						case JUMP_STATUS.DESCEND:
							break;
						default:
							break;
					}	//End of switch_jump
				break;
			case STATUS.SLAP:
				transform.Translate(Vector2.right * runSpeed * Time.deltaTime);	//walking right direction
				anim.SetTrigger("slap_t");
				GameObject atk = 
				 Instantiate(attack_zone, (new Vector3(this.transform.position.x + (float)1.5, this.transform.position.y - (float)0.5, 0)), Quaternion.identity) as GameObject;;
				atk.transform.parent = this.gameObject.transform;
				cur_status = STATUS.RUN;
				break;
			case STATUS.DEAD:
				if(Time.realtimeSinceStartup - wait_to_dispResult >= 3.0f && !gameOver){
					Instantiate(resultDispley);
				GameController.switchScene("result");
					gameOver = true;
				}
				break;
			default:
				break;
		}//end of switch_status

	}
	public void jump(){
		if(nextActionIsReady){
			if(cur_status == STATUS.RUN){
				rigidbody2D.AddForce( new Vector2(0.0f, jumpForce));
				anim.SetTrigger("jump_t");
				cur_status = STATUS.JUMP;
				cur_j_status = JUMP_STATUS.ACSEND;
				wait_to_reuse = TIME_TO_REUSE;
				nextActionIsReady = false;
			}
		}
	}
	
	public void slap(){
		if(nextActionIsReady){
			if(cur_status == STATUS.RUN){
				anim.SetTrigger("slap_t");
				cur_status = STATUS.SLAP;
				wait_to_reuse = TIME_TO_REUSE;
				nextActionIsReady = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(!MUTEKI){
			if (coll.gameObject.tag == "hair"){
				if(coll.gameObject.GetComponent<Hair>().CheckisAlive()){
					explode ();
				}
			}else if(coll.gameObject.tag == "obstacle"){
				explode();
			}else{
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "ground" && cur_status != STATUS.DEAD){
			anim.SetTrigger("run_t");
			cur_status = STATUS.RUN;
			cur_j_status = JUMP_STATUS.ACSEND;
		}else{
		}
	}

	void explode(){
		cur_status = STATUS.DEAD;
		this.renderer.enabled = false;
		this.collider2D.enabled = false;
		Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
		wait_to_dispResult = Time.realtimeSinceStartup;
		this.transform.DetachChildren();
		gameController.GetComponent<GameController>().SetTimeScaleAsDefault();
	}

	/*
	bool IsGrounded(){
		Debug.Log("tmp = " + tmp.ToString() );
		tmp =  Physics2D.Raycast(this.transform.position, new Vector2(0, -1), 0.0f).point;
		GameObject exp = Instantiate(explosion, tmp, Quaternion.identity) as GameObject;
		return true;
//		return Physics.Raycast(transform.position, new Vector3(0, -1, 0), this.circleCollider.radius);
	}
	*/
}
