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

	//public float hearDistance = 1f;
	//useless??
	public float smellDistance = 0.3f;
	//the distance the moster can see in all directions

	public float maxDistance = 10f;

	public GameObject player;

	private Player playerMngr;

	private Vector2 playerPos;

	public AlienKIState random { get; set;}
	public AlienKIState catchUp { get; set;}
	public AlienKIState charge { get; set;}
	public AlienKIState search { get; set;}
	private AlienKIState KIstate;
	// 0 is random movement; 1 is getting in proximity of the player again; 2 is going to the presumed player position; 3 is seeing the player and running towards it

	// Use this for initialization
	void Awake ()
	{
		playerMngr = player.GetComponent<Player> ();

		random= new AlienKIRandom (this);
	    catchUp = new AlienKICatchingUp (this);
	    search = new AlienKISearch (this);
	    charge = new AlienKICharge (this);

		KIstate = random;
	}
	
	// Update is called once per frame
	void Update ()
	{
		KIstate.checkTransitions ();
	}

	public void transitionTo(AlienKIState from, AlienKIState to){
		from.exit ();
		to.enter ();
		KIstate = to;
	}
}