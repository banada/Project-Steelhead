using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	Vector3 bullet_vector;
	float speed = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up*speed*Time.deltaTime);
	}

	// Speed is set by controller after instantiating
	void set_speed(float speed) {
		this.speed = speed;
	}

	void set_vector(Vector3 vector) {
		this.bullet_vector = vector;
	}
}
