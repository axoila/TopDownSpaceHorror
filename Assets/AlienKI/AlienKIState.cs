using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlienKIState {

	protected readonly Alien stateManager;

	public AlienKIState(Alien stateManager){
		this.stateManager = stateManager;
	}

	public abstract void enter ();

	public abstract void checkTransitions();

	public abstract void exit();
}
