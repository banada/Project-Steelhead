using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]

public class ProtagonistController : MonoBehaviour {

	[SerializeField] float move_speed = 10f;
	[SerializeField] float jump_force = 1000f;
	[SerializeField] float slide_force = 50f;
	[SerializeField] float dive_force_x = 30f;
	[SerializeField] float dive_force_y = -40f;
	[SerializeField] float slide_limit = 1000f;
	[SerializeField] float slide_wait = 5f; 
	[SerializeField] float bullet_speed = 1f;
	float gnd_chk_radius = 0.1f;

	public Object Bullet;
	private Shooting shooting;
	private Movement movement;

	private bool jump;
	// Lock player movement when diving, etc.
	private bool is_locked = false;
	private bool is_diving = false;
	private bool slide_ready = true;
	private float slide_dist = 0f;

	private Vector3 aim_position;
	private float aim_angle;

	void Awake() {
		movement = GetComponent<Movement>();
		shooting = GetComponent<Shooting>();
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
				// Slide if we're on the ground
				if (movement.is_grounded(gnd_chk_radius)) {
					if ((slide_dist < slide_limit) && (slide_ready)) {
						slide_dist += movement.slide(slide_force);
					// if we just finished sliding, start cooldown
					} else if (slide_dist >= slide_limit) {
						StartCoroutine(slide_cooldown());
						slide_dist = 0f;
					}
				// Dive if we're in the air
				} else {
						movement.dive(dive_force_x, dive_force_y);
						is_locked = true;
						is_diving = true;
				}
			}
		}
		// If dive has started, continue regardless of user input
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
		// Shooting
		if (Input.GetMouseButtonDown(0)) {
			float aim_angle = shooting.get_mouse_angle();
			Vector3 aim_vector = new Vector3(0, 0, aim_angle);
			Quaternion rotation = Quaternion.Euler(aim_vector);
			GameObject bullet = (GameObject)Instantiate(Bullet, transform.position, rotation);
			bullet.SendMessage("set_speed", bullet_speed);
			bullet.SendMessage("set_vector", aim_position);
		}
	}

	IEnumerator slide_cooldown() {
		slide_ready = false;
		yield return new WaitForSeconds(slide_wait);
		slide_ready = true;
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
