using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public Coroutine currentShield = null;
	public Coroutine currentSpeed = null;
	public Coroutine currentShot = null;
	public GameObject laserPrefab;
	public GameObject tripleShotPrefab;
	public GameObject shieldGameObject;
	public UIManager uIManager;
	public int lives = 3;
	public GameObject explosionPrefab;
	[SerializeField]
	private AudioClip _shot;
	[SerializeField]
	private GameObject[] _engines;
	private GameManager _gameManager;
	private SpawnManager _spawnManager;
	private float _speed;
	private float _fireRate = 0.25f;
	private float _nextFire = 0.0f;

	private void Start() {
		uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

		if (uIManager)
			uIManager.UpdateLives(lives);
		if (_spawnManager)
			_spawnManager.StartSpawnRoutines();
	}

	void Update () {
		Move();
		Shooting();
		PlayerDamages();
	}

	void Shooting() {
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.88f, 0.0f);

		if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) {
			_nextFire = Time.time + _fireRate;
			AudioSource.PlayClipAtPoint(_shot, Camera.main.transform.position, 0.5f);
			if (currentShot != null) {
				Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
			} else {
				Instantiate(laserPrefab, pos, Quaternion.identity);
			}
		}
	}

	private void PlayerDamages() {
		if (lives == 2)
			_engines[0].SetActive(true);
		else if (lives == 1)
			_engines[1].SetActive(true);
		else if (lives <= 0) {
			GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			Destroy(explosion, 2.5f);
			Destroy(this.gameObject);
			_gameManager.gameOver = true;
			uIManager.ShowTitleScreen();
		}
	}

	void Boundaries() {
		if (transform.position.y > 4.2f)
			transform.position = new Vector3(transform.position.x, 4.2f, 0.0f);
		else if (transform.position.y < -4.2f)
			transform.position = new Vector3(transform.position.x, -4.2f, 0.0f);
		if (transform.position.x > 9.0f)
			transform.position = new Vector3(-9.0f, transform.position.y, 0.0f);
		else if (transform.position.x < -9.0f)
			transform.position = new Vector3(9.0f, transform.position.y, 0.0f);
	}

	void Move() {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (currentSpeed != null)
			_speed = 10.0f;
		else
			_speed = 6.0f;
		transform.Translate(Vector3.right * _speed * h * Time.deltaTime);
		transform.Translate(Vector3.up * _speed * v * Time.deltaTime);
		Boundaries();
	}

	public void ShieldBoostPowerUpOn() {
		if (this.currentShield != null)
			StopCoroutine(this.currentShield);
		shieldGameObject.SetActive(true);
		this.currentShield = StartCoroutine(ShieldDown());
	}

	public void SpeedBoostPowerUpOn() {
		if (this.currentSpeed != null)
			StopCoroutine(this.currentSpeed);
		this.currentSpeed = StartCoroutine(SpeedDown());
	}

	public void TripleShotPowerUpOn() {
		if (this.currentShot != null)
			StopCoroutine(this.currentShot);
		this.currentShot = StartCoroutine(TripleShotDown());
	}

	public IEnumerator ShieldDown() {
		yield return new WaitForSeconds(5.0f);
		this.currentShield = null;
		shieldGameObject.SetActive(false);
	}

	public IEnumerator SpeedDown() {
		yield return new WaitForSeconds(5.0f);
		this.currentSpeed = null;
	}

	public IEnumerator TripleShotDown() {
		yield return new WaitForSeconds(5.0f);
		this.currentShot = null;
	}
}
