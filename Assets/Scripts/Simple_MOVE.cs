using UnityEngine;
using System.Collections;

public class Simple_MOVE : MonoBehaviour {
	bool grounded = true;
	public Camera[] cams;
//	public static Rigidbody darigidbody;
	bool moved = false;
	float waitingFor = 0; float curTime = 0;
	int curCam = 0;
	// Use this for initialization
	void Start () {
//		rigidbody = GetComponent<Rigidbody>();
	}

	void waitFor(float ms){
		curTime = Time.time;
		waitingFor = ms;
	}

	bool waitFor(){
		if (Time.time - curTime >= waitingFor/1000f) {
			waitingFor = 0;
			curTime = 0;
			return true;
		} else
			return false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A))
			transform.Rotate (0, -0.8f, 0);
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (0, 0.8f, 0);
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (0, 0, -0.05f);
			moved = true;
		} else if (Input.GetKey (KeyCode.W)) {
			transform.Translate (0, 0, 0.05f);
			moved = true;
		} else
			moved = false;
		if (moved)
			GetComponent<Animator> ().SetFloat ("speed", 1);
		else 			GetComponent<Animator> ().SetFloat ("speed", 0);
		Debug.DrawRay (transform.position, Vector3.down);
		if (Physics.Raycast (transform.position, Vector3.down, 0.35f) && Input.GetKeyDown (KeyCode.Space)) {
				GetComponent<Rigidbody> ().AddForce (Vector3.up * 100f);
			Debug.Log ("JUMPED");
			GetComponent<Animator> ().SetBool ("jump", true); waitFor (470);
		} else if(waitFor ())
			GetComponent<Animator> ().SetBool ("jump", false);
//		GetComponent<Rigidbody> ().AddForce (Vector3.down * 9.8f*1000f);
		if (Input.GetKey (KeyCode.C) && waitFor ()) {
			cams[curCam].enabled=false;
			curCam++; if(curCam == cams.Length) curCam=0;
			cams[curCam].enabled=true;
			waitFor (500);
		}
	}
}
