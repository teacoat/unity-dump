using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public int speed = 1000;
	public float jump_force = 5000f;

	private float moveHorizontal;

	bool grounded = true;
	public Transform ground_check;
	float ground_radius = 0.2f;
	public LayerMask what_is_ground;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		if (grounded && Input.GetButtonDown("Jump")) {
			//rigidbody2D.velocity = new Vector2(0, jump_force);
			jump ();
		}
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (ground_check.position, ground_radius, what_is_ground);

		moveHorizontal = Input.GetAxis("Horizontal");

		movePlayer();

	}


	void movePlayer(){
		Vector2 movement = new Vector2(moveHorizontal, rigidbody2D.velocity.y);	
		rigidbody2D.velocity = movement * speed;
	}

	void jump(){
		rigidbody2D.AddForce(new Vector2(0, jump_force));
	}
}
