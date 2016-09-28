using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 1;

	public Sprite upSprite;
	public Sprite upRightSprite;

	public Transform rotation;
	public GameObject flashLight;

	private SpriteRenderer sprite;
	private Rigidbody2D rigid;

	private float hideouts = 0;

	// Use this for initialization
	void Awake () {
		sprite = GetComponentInChildren<SpriteRenderer> ();
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input.Normalize ();
		input *= speed;
		rigid.velocity = input;

		int direction = Mathf.RoundToInt (Input.GetAxis ("Horizontal")) + 1 + (Mathf.RoundToInt (Input.GetAxis ("Vertical")) + 1) * 3;
		/* maps 9 directions on a compass
		 * 6 7 8
		 * 3 4 5
		 * 0 1 2
		 * */

		switch (direction) {
		case 0:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 180);
			rotation.rotation = Quaternion.Euler (0, 0, 135);
			break;
		case 1:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler(0, 0, 180);
			rotation.rotation = Quaternion.Euler (0, 0, 180);
			break;
		case 2:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 270);
			rotation.rotation = Quaternion.Euler (0, 0, 225);
			break;
		case 3:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler(0, 0, 90);
			rotation.rotation = Quaternion.Euler (0, 0, 90);
			break;
		case 4:
			break;
		case 5:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler(0, 0, 270);
			rotation.rotation = Quaternion.Euler (0, 0, 270);
			break;
		case 6:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 90);
			rotation.rotation = Quaternion.Euler (0, 0, 45);
			break;
		case 7:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 0);
			rotation.rotation = Quaternion.Euler (0, 0, 0);
			break;
		case 8:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 0);
			rotation.rotation = Quaternion.Euler (0, 0, 315);
			break;
		}

		/*if (Input.GetButton ("Fire1")) {
			flashLight.SetActive (true);
		} else {
			flashLight.SetActive (false);
		}*/
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log (coll);
		if (coll.gameObject.layer == LayerMask.NameToLayer ("hideout")) {
			hideouts++;
			flashLight.SetActive (false);
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		//Debug.Log (coll);
		if (coll.gameObject.layer == LayerMask.NameToLayer ("hideout")) {
			hideouts--;
			if (hideouts == 0)
				flashLight.SetActive (true);
			else if (hideouts < 0)
				Debug.LogError ("left more hideouts than joined");
		}
	}
}
