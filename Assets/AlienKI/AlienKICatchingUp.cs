using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienKICatchingUp : AlienKIState {

	public AlienKICatchingUp (Alien stateManager) : base (stateManager)
	{

	}

	public override void checkTransitions ()
	{
		Vector2 toPlayer = stateManager.player.transform.position - stateManager.transform.position;

		if (toPlayer.magnitude < stateManager.maxDistance) {
			stateManager.transitionTo (this, stateManager.random);
		}
	}

	public override void exit ()
	{

	}

	public override void enter ()
	{
		Debug.Log ("the alien went too far away from the player and has to catch up again");
	}

	public override void tick ()
	{

	}
}
