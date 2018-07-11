using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	private float _speed = 4.0f;
	[SerializeField]
	private GameObject _enemyExplosionPrefab;
	private UIManager _uiManager;
	private GameManager _gameManager;
	[SerializeField]
	private AudioClip _clip;
	
	void Start () {
		transform.position = new Vector3(Random.Range(-7.0f, 6.0f), 6.2f, 0);
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	
	
	void Update () {
		
		if(_gameManager.gameOver == true){
			Destroy(this.gameObject);
		}
		
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y <= -5.0f){
			transform.position = new Vector3(Random.Range(-7.0f, 6.0f), 6.2f, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			Player player = other.GetComponent<Player>();	

			if(player != null){
				player.Damage();
				Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
				AudioSource.PlayClipAtPoint(_clip, transform.position);
				Destroy(this.gameObject);
				_uiManager.UpdateScore();
			}
		}
		if(other.tag == "Laser"){
			Laser laser = other.GetComponent<Laser>();

			if(laser != null){
				Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
				AudioSource.PlayClipAtPoint(_clip, transform.position);
				Destroy(this.gameObject);
				Destroy(laser.gameObject);
				_uiManager.UpdateScore();
			}
		}
	}
}
