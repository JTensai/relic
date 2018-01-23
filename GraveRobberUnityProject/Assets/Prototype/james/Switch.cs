using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public delegate void Trigger(bool value, bool animate);

	public Trigger onChange;

	public bool isPressed = false;
	public bool isSticky = false;
	private bool containsSomething = false;

	// Use this for initialization
	void Start () {
		GetComponent<DelegateConnector>().VerifyDelegateBinding();

		if (onChange != null)
		{
			onChange(isPressed, false);
		}
	}

	public void Update()
	{
		if (containsSomething != isPressed && (!isSticky || !isPressed))
		{
			isPressed = containsSomething;

			if (onChange != null)
			{
				onChange(isPressed, true);
			}
		}
	}

	public void FixedUpdate()
	{
		containsSomething = false;
	}

	public void OnTriggerStay(Collider collider)
	{
		containsSomething = true;
	}
}
