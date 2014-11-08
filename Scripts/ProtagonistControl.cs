using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]

public class ProtagonistControl : MonoBehaviour {

	[SerializeField] float move_speed = 10f;
	[SerializeField] float jump_force = 1000f;
	[SerializeField] float slide_force = 30f;
	[SerializeField] float dive_force_x = 30f;
	[SerializeField] float dive_force_y = -40f;
	float gnd_chk_radius = 0.1f;
	
	private Movement movement;
	private bool jump;
	// Lock player movement when diving, etc.
	private bool is_locked = false;
	private bool is_diving = false;

	void Awake() {
		movement = GetComponent<Movement>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButtonDown("Jump")) && (!is_locked)) {
			movement.jump(jump_force);
		}
		if (Input.GetButton("Ctrl")) {
			if (!is_locked) {
				if (movement.is_grounded(gnd_chk_radius)) {
					movement.slide(slide_force);
				} else {
						movement.dive(dive_force_x, dive_force_y);
						is_locked = true;
						is_diving = true;
				}
			}
		}
		if (is_diving) {
			if (movement.is_grounded(gnd_chk_radius)) {
				// Rigidbody2D can bounce weirdly on grounding if character moving
				movement.freeze();
				is_locked = false;
				is_diving = false;
			} else {
				movement.dive(dive_force_x, dive_force_y);
			}
		}
	}
	
	//use FixedUpdate instead of Update on Rigidbody
	void FixedUpdate() {
		//gets change in position
		float axis = Input.GetAxis("Horizontal");
		//move player
		if (!is_locked) {
			movement.move(axis, move_speed);
		}
	}
}
