using UnityEngine;
using System.Collections;

public class DeathState : IState {

	private Transform _characterTransform;
	private Animator _characterAnimator;
	private float currDodgeDist;
	private Vector3 dodgeDirection;
	private PlayerController _pController;
	
	private InteractableComponent targetIC = null;

	private float _deathTime = 2f;
	private float _elapsedTime;

	public DeathState(PlayerController pController)
	{
		_pController = pController;
		_characterTransform = pController.transform;
		_characterAnimator = pController.GetCharAnimator ();
	}
	
	public void BeginState(StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Death", true);
		
		//_pController.playDodgeSFX();
	}
	
	public void Update(StateMachine stateMachine)
	{
		_elapsedTime += Time.deltaTime;
		if (_elapsedTime >= _deathTime) {

			_characterAnimator.SetBool ("Death", false);
			stateMachine.ResetForce();
			stateMachine.SetNextState(new IdleState(_pController));
		}
	}
	
	
	public void EndState(StateMachine stateMachine)
	{
		_characterAnimator.SetBool ("Death", false);

		_pController.ResetOnDeath ();
	}

}
