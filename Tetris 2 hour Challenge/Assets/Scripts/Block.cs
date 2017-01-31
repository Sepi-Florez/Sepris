using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public Animator anim;
	// Use this for initialization
	void Awake () {
        transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
