using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Get rotation angle from mouse vector
	public float get_mouse_angle() {
		Vector3 aim_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float aim_angle = Mathf.Atan2((transform.position.x - aim_position.x), (aim_position.y - transform.position.y)) * Mathf.Rad2Deg;
		return aim_angle;
	}

	// Get rotation angle from arbitrary vector
	public float get_target_angle(Vector3 input_vector3) {
		float aim_angle = Mathf.Atan2((transform.position.x - input_vector3.x), (input_vector3.y - transform.position.y)) * Mathf.Rad2Deg;
		return aim_angle;
	}

}
