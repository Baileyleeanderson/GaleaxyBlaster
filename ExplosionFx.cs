﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFx : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 4f);
	}
	
}
