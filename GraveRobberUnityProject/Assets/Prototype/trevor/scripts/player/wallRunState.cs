using UnityEngine;
using System.Collections;

public class WallRunState : IState {

    //private CharacterController _characterController;
    //private Animator _characterAnimator;
    //public float MovementSpeed = 1f;
    //private float x;
    //private float y;

	public WallRunState(CharacterController pController)
	{
        //x = pController.velocity[0];
        //y = pController.velocity[1];
	}

	public void BeginState(StateMachine stateMachine)
	{
        //_characterController.Move(new Vector3(x, 1, y) * Time.deltaTime * MovementSpeed);
	}

	// Update is called once per frame
	public void Update (StateMachine stateMachine) {

        //if(!_characterController.isGrounded)
            //_characterController.Move(new Vector3(x, 1, y) * Time.deltaTime * MovementSpeed);
	
	}

	public void EndState(StateMachine stateMachine)
	{
		//_characterAnimator.SetBool ("WRun",false);
	}
}
