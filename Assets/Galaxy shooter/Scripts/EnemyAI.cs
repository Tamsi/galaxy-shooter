using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {
	private float _speed = 3.0f;
	[SerializeField]
	private AudioClip _clip;
	public GameObject explosionPrefab;
	public UIManager uIManager;

	void Start () {
		uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}

	public void ResetEnemy() {
		float randomX = Random.Range(-7.0f, 7.0f);
		transform.position = new Vector3(randomX, 6.5f, 0.0f);
	}

	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		if (transform.position.y <= -6.5f) {
			ResetEnemy();
		}
	}

	public void PlayExplosion() {
		AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1.0f);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		PlayerController player = other.GetComponent<PlayerController>();

		if (other.tag == "Player") {
			if (player.currentShield == null) {
				player.lives -= 1;
				player.uIManager.UpdateLives(player.lives);
			}
			GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			PlayExplosion();
			Destroy(explosion, 2.5f);
			Destroy(this.gameObject);
		}
	}
}
