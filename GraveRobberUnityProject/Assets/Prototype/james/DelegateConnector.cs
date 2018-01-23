using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class DelegateConnector : MonoBehaviour
{
	[System.Serializable]
	public class Mapping
	{
		public MonoBehaviour delegateSource;
		public string delegateName;

		public MonoBehaviour methodTarget;
		public string methodName;
	}
	
	public List<Mapping> delegateMappingList = new List<Mapping>();
	private bool bindingVerified = false;

	private void RebindDelegates()
	{
		foreach (Mapping mapping in delegateMappingList)
		{
			if (mapping.methodTarget != null && mapping.delegateSource != null)
			{
				FieldInfo delegateField = mapping.delegateSource.GetType().GetField(mapping.delegateName, BindingFlags.Public | BindingFlags.Instance);

				if (delegateField != null)
				{
					Delegate delegateValue = Delegate.CreateDelegate(delegateField.FieldType, mapping.methodTarget, mapping.methodName, false, false);

					if (delegateValue != null)
					{
						delegateField.SetValue(mapping.delegateSource, delegateValue);
					}
				}
			}
		}
	}

	public void CleanDeletedConnections()
	{
		for (int i = 0; i < delegateMappingList.Count;)
		{
			Mapping currentMapping = delegateMappingList[i];

			if (currentMapping.delegateSource == null || currentMapping.methodTarget == null)
			{
				delegateMappingList.RemoveAt(i);
			}
			else
			{
				++i;
			}
		}
	}

	public void VerifyDelegateBinding()
	{
		if (!bindingVerified)
		{
			RebindDelegates();

			bindingVerified = true;
		}
	}

	public void Start()
	{
		VerifyDelegateBinding();
	}

	public void MapDelegate(MonoBehaviour delegateSource, string delegateFieldName, MonoBehaviour methodTarget, string methodName)
	{
		bool mappingFound = false;

		for (int i = 0; i < delegateMappingList.Count; ++i)
		{
			Mapping currentMapping = delegateMappingList[i];

			if (currentMapping.delegateSource == delegateSource && currentMapping.delegateName == delegateFieldName)
			{
				if (methodTarget != null && methodName != null)
				{
					currentMapping.methodTarget = methodTarget;
					currentMapping.methodName = methodName;
				}
				else
				{
					delegateMappingList.RemoveAt(i);
				}

				mappingFound = true;
				break;
			}
		}

		if (!mappingFound && methodTarget != null && methodName != null)
		{
			Mapping mapping = new Mapping();
			mapping.delegateSource = delegateSource;
			mapping.delegateName = delegateFieldName;
			mapping.methodTarget = methodTarget;
			mapping.methodName = methodName;

			delegateMappingList.Add(mapping);
		}
	}
}
