using UnityEngine;
using System.Collections;

public interface IState {
	void BeginState(StateMachine stateMachine);
	void Update(StateMachine stateMachine);
	void EndState(StateMachine stateMachine);
}
