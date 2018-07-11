﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	
	[SerializeField]
	private float _speed = 10.0f;
	void Start () {
		
	}
	void Update () {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);

		if (this.gameObject.transform.position.y >= 6){
			Destroy(this.gameObject);
		}
	}
}
