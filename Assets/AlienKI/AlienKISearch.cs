using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienKISearch : AlienKIState {

	private Vector3 lastPlayerPosition;

	public AlienKISearch (Alien stateManager) : base (stateManager)
	{

	}

	public override void checkTransitions ()
	{
		Vector2 toPlayer = stateManager.player.transform.position - stateManager.transform.position;
		RaycastHit2D lookToPlayerHit = Physics2D.Raycast (stateManager.transform.position, 
			toPlayer.normalized, stateManager.viewDistance, LayerMask.GetMask ("Default"));
		if (lookToPlayerHit.collider == stateManager.player.GetComponent<Collider2D> () &&
		    (Vector2.Angle (-stateManager.transform.up, toPlayer) < stateManager.viewangle || 
				toPlayer.magnitude < stateManager.smellDistance)) {

			stateManager.transitionTo (this, stateManager.charge);
		} else {
			if ((lastPlayerPosition - stateManager.transform.position).magnitude < 0.1f) {
				stateManager.transitionTo (this, stateManager.random);
			}
		}
	}

	public override void exit ()
	{

	}

	public override void enter ()
	{
		lastPlayerPosition = stateManager.player.transform.position;
		Debug.Log ("the alien can't see the player anymore and goes to the last seen location");
	}
}
