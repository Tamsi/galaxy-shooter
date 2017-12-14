using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {
	public UIManager uIManager;

	private void Start() {
		uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	private void Update() {
		transform.Translate(Vector3.up * 10.0f * Time.deltaTime);
		if (transform.position.y >= 6.0f) {
			if (transform.parent != null)
				Destroy(transform.parent.gameObject);
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		EnemyAI enemy = other.GetComponent<EnemyAI>();

		if (other.tag == "Enemy") {
			uIManager.UpdateScore();
			GameObject explosion = Instantiate(enemy.explosionPrefab, enemy.transform.position, Quaternion.identity);
			enemy.PlayExplosion();
			Destroy(enemy.gameObject);
			Destroy(this.gameObject);
			Destroy(explosion, 2.5f);
		}
	}
}
