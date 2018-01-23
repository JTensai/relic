using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Switch.Trigger propogateSignalTo;
	public bool invertInput;
	public bool isOpen;

	private void UpdateOpenState()
	{
		bool openValue = isOpen != invertInput;

		renderer.enabled = !openValue;
		collider.enabled = !openValue;
	}

	public void Start()
	{
		UpdateOpenState();
	}

	public void SetIsOpen(bool isOpen, bool animated)
	{
		this.isOpen = isOpen;

		UpdateOpenState();

		if (propogateSignalTo != null)
		{
			propogateSignalTo(isOpen, animated);
		}
	}
}
