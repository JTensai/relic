using UnityEngine;
using System.Collections;

public class FallState : IState {

	private float rotationSpeed;
	private float fallTimer;
	
	public FallState(PlayerController pController)
	{

	}
	
	public void BeginState(StateMachine stateMachine)
	{
		fallTimer = 0.0f;
	}
	
	public void Update(StateMachine stateMachine)
	{
		fallTimer += Time.deltaTime;
	}
	
	public void EndState(StateMachine stateMachine)
	{
		
	}
}
