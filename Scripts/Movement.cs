using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


	//Check for objects in Ground layer
	[SerializeField] LayerMask marked_ground;
	Transform bottom;
	Transform top;
	Vector2 dive_vector;
	//float to_ground;

	void Awake() {
		bottom = transform.Find("Bottom");
		top = transform.Find ("Top");
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
	}

	// Lateral Movement
	public void move(float axis, float move_speed) {
		rigidbody2D.velocity = new Vector2(axis*move_speed, rigidbody2D.velocity.y);
	}

	// Jumping
	public void jump(float jump_force) {
		if (is_grounded()) {
			rigidbody2D.AddForce(new Vector2(0f, jump_force));
		}
	}

	// Lateral slide
	public void slide(float slide_force) {
		rigidbody2D.AddForce(new Vector2(slide_force, 0f));
	}

	// Downward dive
	public void dive(float dive_force_x, float dive_force_y) {
		dive_vector = new Vector2(dive_force_x, dive_force_y);
		rigidbody2D.MovePosition(rigidbody2D.position + dive_vector*Time.deltaTime);
	}

	// Checks if character is touching ground
	public bool is_grounded(float gnd_chk_radius=0.1f) {
		bool grounded = Physics2D.OverlapCircle(bottom.position, gnd_chk_radius, marked_ground);
		//bool grounded = Physics2D.Raycast(bottom.position, -Vector2.up, 1f);
		return grounded;
	}

	// Remove all velocity
	public void freeze() {
		rigidbody2D.velocity = new Vector2(0, 0);
	}

}
