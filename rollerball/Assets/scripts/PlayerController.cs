using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private float moveHorizontal;
	private float moveVertical;

	public float speed; 

	public GUIText countText;
	public GUIText winText;
	private int count;

	public float jump_force = 0.7f;
	private bool jump_allowed = true;

	private bool wall_jump_allowed = false;


	void Start(){
		count = 0;
		setCountText();
		winText.text = "";
	}

	void FixedUpdate(){
		moveHorizontal = Input.GetAxis("Horizontal");
		moveVertical = Input.GetAxis("Vertical");

		Debug.Log (Input.GetAxis ("Horizontal"));

		if(jump_allowed == true) moveBall();

		if (Input.GetButton("Jump") && jump_allowed == true && wall_jump_allowed == false) {
			jump ();
		}
		if (Input.GetButton("Jump") && wall_jump_allowed == true && jump_allowed == false) {
			walljump ();
		}
	}

	//MOVE STUFF
	void moveBall(){
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		rigidbody.AddForce(movement * speed * Time.deltaTime);
	}
		
	//JUMP STUFF

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Floor") {
			jump_allowed = true;
			wall_jump_allowed = false;
		}
		if (collision.gameObject.tag == "Wall") {
			wall_jump_allowed = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Floor") {
			jump_allowed = false;
		}
		if (collision.gameObject.tag == "Wall") {
			wall_jump_allowed = false;
		}
	}

	void jump(){
		rigidbody.velocity = new Vector3(moveHorizontal/1.5f, jump_force, moveVertical/1.5f) * speed * Time.deltaTime;
		jump_allowed = false;
	}

	void walljump(){
		rigidbody.velocity = new Vector3(moveHorizontal*1.5f, jump_force*1.5f, moveVertical*1.5f) * speed * Time.deltaTime;
		wall_jump_allowed = false;
	}

	
	//POINTS STUFF

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive (false);
			count++;
			setCountText();
		}
	}
	
	void setCountText(){
		countText.text = "Count: " + count.ToString() + "/12";
		if(count >= 12){
			winText.text = "You Win!";
		}
	}
}