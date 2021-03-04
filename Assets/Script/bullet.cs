using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    public int Damage = 20;
    public float Speed = 10.0f;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
