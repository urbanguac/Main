using UnityEngine;
using System.Collections;

public class respawner : MonoBehaviour {
	Quaternion spawn;
	WWW www;
	string url;
	string returned;
	int x, y, z=0;
	int height = 0;
	int prevZ = 0;
	// Use this for initialization
	void Start () {
		spawn = transform.rotation;
		url= "http://localhost:3000/";
		StartCoroutine (getGyro());
	}

	void Update() {
		if (Input.GetKey (KeyCode.R))
			transform.rotation = spawn;
		transform.Translate (x*0.002f, z*0.001f, y*-0.002f,Space.World);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Collided");
		if (other.gameObject.tag == "Respawn");
//			transform.position = spawn;
	}

	int getFirstNumber(string x){
		return int.Parse (x.Substring (0,x.IndexOf(' ')));
	}

	void deadZone(){
		if (Mathf.Abs (x) < 500)
			x = 0;
		else
			x = (int)Mathf.Sign (x)*750;
		if (Mathf.Abs (y) < 500)
			y = 0;
		else
			y = (int)Mathf.Sign (y)*750;
		if (Mathf.Abs (z) < 20)
			z = 0;
		else if(Mathf.Abs(y) < 500 && Mathf.Abs(x) < 500)
			z *= 2;
	}

	IEnumerator getGyro() {
		while (true) {
			prevZ = z;
			www = new WWW (url);
			yield return www;
			Debug.Log (www.text);
			returned = www.text;
			x = getFirstNumber(returned);
			returned = returned.Substring (returned.IndexOf(' ')+1);
			y = getFirstNumber(returned);
			returned = returned.Substring (returned.IndexOf(' ')+1);
			z = int.Parse (returned);
			Debug.Log(x+" "+y+" "+z);
			if(Mathf.Abs (z-prevZ) < 20) z = 0;
			deadZone ();
		}
	}
}
