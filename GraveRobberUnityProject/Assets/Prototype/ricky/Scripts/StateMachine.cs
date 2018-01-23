using UnityEngine;
using System.Collections;

public class StateMachine {

	public delegate IState StateSource(string stateName);

	private IState currentState;
	private StateSource stateSource;

	private string nextStateName;
	private IState nextState;
	private bool force;

	public StateMachine(StateSource stateSource)
	{
		this.stateSource = stateSource;
		currentState = stateSource("start");
		currentState.BeginState(this);
	}

	public void SetState(StateSource stateSource){
		this.stateSource = stateSource;
		currentState = stateSource("start");
		currentState.BeginState(this);
	}

	private IState GetNextState()
	{
		IState result = null;

		if (nextState != null) {
				result = nextState;
		} else if (nextStateName != null) {
				result = stateSource (nextStateName);
		}

		nextState = null;
		nextStateName = null;
		return result;
	}

	private void SetCurrentState(IState newState)
	{
		currentState.EndState(this);
		currentState = newState;
		currentState.BeginState(this);
	}

	public void Update()
	{
		currentState.Update(this);
		IState nextState = GetNextState();

		if (nextState != null)
		{
			SetCurrentState(nextState);
		}
	}

	public void SetNextState(IState value)
	{

		if (force == false) {
			nextState = value;
			nextStateName = null;
		}
	}

	public void SetNextState(string name)
	{
		if (force == false) {
			nextState = null;
			nextStateName = name;
		}
	}

	public void SetNextState(IState value, bool force){

		SetNextState (value);
		this.force = force;
	}

	public void ResetForce(){
		this.force = false;
	}
}
