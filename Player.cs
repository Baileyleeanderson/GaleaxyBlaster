using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public int lives = 3;
	public bool canTripleShot = false;
	public bool canSpeedBoost = false;
	public bool shieldOn = false;  
	private float _fireRate = 0.25f;
	private float _canFire = 0.0f;
	private UIManager _uiManager;
	private GameManager _gameManager;
	private AudioSource _audioSource;


	[SerializeField]
	private GameObject _shieldPrefab;
	[SerializeField]
	private GameObject _explosionPrefab;
	[SerializeField] 
	private float _speed = 5.0f;
	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _tripleShotPrefab;
	[SerializeField]
	private GameObject _playerHurtPrefab;
	[SerializeField]
	private GameObject _thrusterPrefab;
	private SpawnManager _spawnManger;

	void Start () {
		transform.position = new Vector3(0,0,0);
		_playerHurtPrefab.SetActive(false);
		_thrusterPrefab.SetActive(true);
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

		if(_uiManager != null){
			_uiManager.UpdateLives(lives);
		}
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		_spawnManger = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

		if(_spawnManger != null){
			_spawnManger.StartSpawnRoutine();
		}

		_audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		Movement();

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)){
			Shoot();
		}	
	}

	private void Shoot(){
		if (Time.time > _canFire){
			_audioSource.Play();
			if (canTripleShot == true){
				Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
			}
			else{
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.78f, 0), Quaternion.identity);
			}
			_canFire = Time.time + _fireRate;
		}
	}

	private void Movement(){
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		if(canSpeedBoost == true){
			transform.Translate(Vector3.right * Time.deltaTime * _speed * 3.0f * horizontalInput);
			transform.Translate(Vector3.up * Time.deltaTime * _speed * 3.0f * verticalInput);
		}
		else{
			transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
			transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
		}
		//y boundary for player
		if (transform.position.y > 0){
			transform.position = new Vector3(transform.position.x,0,0);
		}
		else if (transform.position.y < -4){
			transform.position = new Vector3(transform.position.x, -4, 0);
		}

		//x boundary for player
		if (transform.position.x > 8.5){
			transform.position = new Vector3(-8.5f, transform.position.y,0);
		}
		else if (transform.position.x < -8.5){
			transform.position = new Vector3(8.5f, transform.position.y,0);
		}
	}

	public void Damage(){
		if(shieldOn == true){
			shieldOn = false;
			_shieldPrefab.SetActive(false);
			return;
		}
		lives -= 1;
		_uiManager.UpdateLives(lives);
		if(this.lives == 1){
			_thrusterPrefab.SetActive(false);
			_playerHurtPrefab.SetActive(true);
		}
		if(this.lives <= 0){
			_playerHurtPrefab.SetActive(false);
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen();
		}
	}

	public void ShieldActive(){
		shieldOn = true;
		_shieldPrefab.SetActive(true);
	}

	public void SpeedBoostOn(){
		canSpeedBoost = true;
		StartCoroutine(SpeedBoostOff());
	}
	public IEnumerator SpeedBoostOff(){
		yield return new WaitForSeconds(8.0f);
		canSpeedBoost = false;
	}
	public void TripleShotPowerUpOn(){
		canTripleShot = true;
		StartCoroutine(TripleShotPowerDown());
	}
	public IEnumerator TripleShotPowerDown(){
		yield return new WaitForSeconds(6.0f);
		canTripleShot = false;
	}
}

