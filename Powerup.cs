using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	private float speed = 3.0f;
	[SerializeField]
	private int powerUpId;
	[SerializeField]
	private AudioClip _clip;
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if(transform.position.y < -7){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.tag == "Player"){
			AudioSource.PlayClipAtPoint(_clip, transform.position);
			Player player = other.GetComponent<Player>();
			if (player != null){
				if(powerUpId == 0){
					speed = 2.0f;
					player.TripleShotPowerUpOn();
				}
				else if(powerUpId == 1){
					player.SpeedBoostOn();
				}
				else if(powerUpId == 2){
					speed = 1.0f;
					player.ShieldActive();
				}
			}
			
			Destroy(this.gameObject);
		}
	}
}
