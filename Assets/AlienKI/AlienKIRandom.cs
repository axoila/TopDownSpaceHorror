using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienKIRandom : AlienKIState
{
	public AlienKIRandom (Alien stateManager) : base (stateManager)
	{
		
	}

	public override void checkTransitions ()
	{
		Vector2 toPlayer = stateManager.player.transform.position - stateManager.transform.position;

		if (toPlayer.magnitude > stateManager.maxDistance) {
			stateManager.transitionTo (this, stateManager.catchUp);
		} else {
			RaycastHit2D lookToPlayerHit = 
				Physics2D.Raycast (stateManager.transform.position, toPlayer.normalized, stateManager.viewDistance, LayerMask.GetMask ("Default"));
			if (lookToPlayerHit.collider == stateManager.player.GetComponent<Collider2D> () &&
				(Vector2.Angle (-stateManager.rotation.up, toPlayer) < stateManager.viewangle || toPlayer.magnitude < stateManager.smellDistance)) {

				stateManager.transitionTo (this, stateManager.charge);
			}
		}
	}

	public override void exit ()
	{
		
	}

	public override void enter ()
	{
		Debug.Log ("the alien lost the player and will stroll around aimlessly");
	}

	public override void tick ()
	{

	}
}
