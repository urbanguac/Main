using UnityEngine;
using System.Collections;

public class HoleKeeper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.gameObject.tag == "Tile")
			Destroy (other.collider.gameObject);
	}
}
