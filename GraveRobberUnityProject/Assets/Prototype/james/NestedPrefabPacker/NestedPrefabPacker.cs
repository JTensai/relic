using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NestedPrefabPacker : MonoBehaviour {

#if UNITY_EDITOR
	public void Pack()
	{
		InteratePrefabs(transform);
	}

	public void Unpack()
	{

	}

	private void InteratePrefabs(Transform targetTransfrom)
	{
		if (PrefabUtility.GetPrefabType(targetTransfrom.gameObject) == PrefabType.PrefabInstance)
		{
			GameObject targetGameObject = targetTransfrom.gameObject;

			PropertyModification[] modifications = PrefabUtility.GetPropertyModifications(targetGameObject);

			foreach (PropertyModification property in modifications)
			{
				Debug.Log(property.propertyPath + " = " + property.value);
			}

			Component[] components = targetGameObject.GetComponents<Component>();

			foreach (Component component in components)
			{
				Debug.Log(component);
				Debug.Log(PrefabUtility.GetPrefabType(component).ToString());
				
				PropertyModification[] componentModifications = PrefabUtility.GetPropertyModifications(targetGameObject);
				
				foreach (PropertyModification property in componentModifications)
				{
					Debug.Log(property.propertyPath + " = " + property.value);
				}
			}
		}

		foreach (Transform child in targetTransfrom)
		{
			InteratePrefabs(child);
		}
	}
#endif

}
