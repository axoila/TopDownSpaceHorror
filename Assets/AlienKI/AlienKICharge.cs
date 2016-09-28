using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienKICharge : AlienKIState {

	public AlienKICharge (Alien stateManager) : base (stateManager)
	{

	}

	public override void checkTransitions ()
	{
		Vector2 toPlayer = stateManager.player.transform.position - stateManager.transform.position;
		RaycastHit2D lookToPlayerHit = Physics2D.Raycast (stateManager.transform.position, 
			toPlayer.normalized, stateManager.viewDistance, LayerMask.GetMask ("Default"));
		if (!(lookToPlayerHit.collider == stateManager.player.GetComponent<Collider2D> () &&
			(Vector2.Angle (-stateManager.rotation.up, toPlayer) < stateManager.viewangle || 
				toPlayer.magnitude < stateManager.smellDistance))) {

			stateManager.transitionTo (this, stateManager.search);
		}
	}

	public override void exit ()
	{

	}

	public override void enter ()
	{
		Debug.Log ("the alien saw the player and charges it");
	}

	public override void tick ()
	{
		Vector2 toPlayer = stateManager.player.transform.position - stateManager.transform.position;
		stateManager.rigid.velocity = toPlayer.normalized * stateManager.chargeSpeed;
	}
}
