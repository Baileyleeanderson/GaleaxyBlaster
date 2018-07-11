using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField]
	private GameObject _enemyShipPrefab;
	[SerializeField]
	private GameObject _enemyPrefab;
	[SerializeField]
	private GameObject _enemyRedPrefab;
	[SerializeField]
	private GameObject _enemyBigDawg;
	[SerializeField]
	private GameObject[] powerups;
	private GameManager _gameManager;
	
	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	void Update(){
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	public void StartSpawnRoutine(){
		
		StartCoroutine(SpawnNewEnemy());
		StartCoroutine(SpawnNewEnemy1());
		StartCoroutine(SpawnNewEnemy2());
		StartCoroutine(SpawnNewEnemyBigDawg());
		StartCoroutine(SpawnNewPowerup());
	}
	
	public IEnumerator SpawnNewEnemy(){
		while(_gameManager.gameOver == false){
			yield return new WaitForSeconds(2f);
			Instantiate(_enemyShipPrefab);
		}
	}
	public IEnumerator SpawnNewEnemy1(){
		while(_gameManager.gameOver == false){
			yield return new WaitForSeconds(6f);
			Instantiate(_enemyPrefab);
		}
	}
	public IEnumerator SpawnNewEnemy2(){
		while(_gameManager.gameOver == false){
			yield return new WaitForSeconds(12f);
			Instantiate(_enemyRedPrefab);
		}
	}
	public IEnumerator SpawnNewEnemyBigDawg(){
		while(_gameManager.gameOver == false){
			yield return new WaitForSeconds(17f);
			Instantiate(_enemyBigDawg);
		}
	}

	public IEnumerator SpawnNewPowerup(){
		while(_gameManager.gameOver == false){
			int rnd = Random.Range(0,3);
			yield return new WaitForSeconds(9.0f);
			Instantiate(powerups[rnd], new Vector3(Random.Range(-5.0f, 5.0f),6.0f, 0),Quaternion.identity);
		}
	}
}
