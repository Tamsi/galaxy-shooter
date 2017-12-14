using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	private float _speed = 3.0f;
	[SerializeField]
	private AudioClip _clip;
	public int powerUpID;
	private void Start () {
	}

	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		if (transform.position.y <= -6.0f)
			Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		PlayerController player = other.GetComponent<PlayerController>();

		if (other.tag == "Player") {
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1.0f);
			if (this.powerUpID == 0)
				player.TripleShotPowerUpOn();
			else if (this.powerUpID == 1)
				player.SpeedBoostPowerUpOn();
			else if (this.powerUpID == 2)
				player.ShieldBoostPowerUpOn();
		}
		Destroy(this.gameObject);
	}
}
