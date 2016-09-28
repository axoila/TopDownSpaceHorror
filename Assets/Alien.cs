using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

	public float strollingSpeed;
	public float searchingSpeed;
	public float chargeSpeed;

	public float viewDistance = 3f;
	//the distance the alien can see
	public float viewangle = 30f;
	//the viewangle in degree
	public float smellDistance = 0.3f;
	//the distance the moster can see in all directions

	public float maxDistance = 10f;
	//the maximum distance the alien can get away before returning to the direction of the Player

	public GameObject player;
	public Rigidbody2D rigid { get; private set;}

	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] public Transform rotation;
	[SerializeField] private Sprite upSprite;
	[SerializeField] private Sprite upRightSprite;

	public AlienKIState random { get; private set;}
	public AlienKIState catchUp { get; private set;}
	public AlienKIState charge { get; private set;}
	public AlienKIState search { get; private set;}
	private AlienKIState KIstate;
	// 0 is random movement; 1 is getting in proximity of the player again; 2 is going to the presumed player position; 3 is seeing the player and running towards it

	// Use this for initialization
	void Awake ()
	{
		//playerMngr = player.GetComponent<Player> ();

		random= new AlienKIRandom (this);
	    catchUp = new AlienKICatchingUp (this);
	    search = new AlienKISearch (this);
	    charge = new AlienKICharge (this);

		KIstate = random;

		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		KIstate.checkTransitions ();

		KIstate.tick ();

		//set rotation of sprite and collider
		int direction = Mathf.RoundToInt((Mathf.Atan2(rigid.velocity.y , rigid.velocity.x) * Mathf.Rad2Deg + 157.5f) / 45);
		/* maps 9 directions on a compass
		 * 6 7 8
		 * 3 4 5
		 * 0 1 2
		 * */

		Debug.Log (direction);

		switch (direction) {
		case 0:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 270);
			rotation.rotation = Quaternion.Euler (0, 0, 270);
			break;
		case 1:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
			rotation.rotation = Quaternion.Euler (0, 0, 315);
			break;
		case 2:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 0);
			rotation.rotation = Quaternion.Euler (0, 0, 0);
			break;
		case 3:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 90);
			rotation.rotation = Quaternion.Euler (0, 0, 45);
			break;
		case 4:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 90);
			rotation.rotation = Quaternion.Euler (0, 0, 90);
			break;
		case 5:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler(0, 0, 180);
			rotation.rotation = Quaternion.Euler (0, 0, 135);
			break;
		case 6:
			sprite.sprite = upSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 180);
			rotation.rotation = Quaternion.Euler (0, 0, 180);
			break;
		case 7:
			sprite.sprite = upRightSprite;
			sprite.transform.rotation = Quaternion.Euler (0, 0, 270);
			rotation.rotation = Quaternion.Euler (0, 0, 225);
			break;
		}
	}

	public void transitionTo(AlienKIState from, AlienKIState to){
		from.exit ();
		to.enter ();
		KIstate = to;
	}
}