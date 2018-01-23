using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(DelegateConnector))]
public class DelegateConnectorEditor : Editor
{
	private void WriteBehaviorHeader(MonoBehaviour behavior)
	{
		EditorGUILayout.LabelField(new GUIContent(behavior.GetType().Name));
	}

	private void WriteDelegateField(DelegateConnector connector, MonoBehaviour behavior, FieldInfo delegateField)
	{
		Delegate delegateValue = delegateField.GetValue(behavior) as Delegate;

		MonoBehaviour lastTarget = null;
		string lastMethod = null;

		if (delegateValue != null)
		{
			lastTarget = delegateValue.Target as MonoBehaviour;
			lastMethod = delegateValue.Method.Name;
		}

		MonoBehaviour newTarget = EditorGUILayout.ObjectField(new GUIContent(delegateField.Name), lastTarget, typeof(MonoBehaviour), true) as MonoBehaviour;

		List<string> memberNames = new List<string>();

		int previousIndex = 0;
		int index = 0;

		if (newTarget != null)
		{
			foreach (MethodInfo method in newTarget.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance))
			{
				if (Delegate.CreateDelegate(delegateField.FieldType, newTarget, method.Name, false, false) != null)
				{
					memberNames.Add(method.Name);

					if (method.Name == lastMethod)
					{
						previousIndex = index;
					}

					++index;
				}
			}
		}
		else
		{
			memberNames.Add("No behavior selected");
		}

		GUIContent[] memberFunctionOptions = new GUIContent[memberNames.Count];

		index = 0;

		foreach (string methodName in memberNames)
		{
			memberFunctionOptions[index++] = new GUIContent(methodName);
		}

		int newIndex = EditorGUILayout.Popup(previousIndex, memberFunctionOptions);

		if (previousIndex != newIndex || lastTarget != newTarget)
		{
			string newMethodName = null;

			if (newTarget == null || memberNames.Count == 0)
			{
				delegateField.SetValue(behavior, null);
			}
			else
			{
				newMethodName = memberNames[newIndex];
				Delegate newDelegate = Delegate.CreateDelegate(delegateField.FieldType, newTarget, newMethodName);
				delegateField.SetValue(behavior, newDelegate);
			}

			connector.MapDelegate(behavior, delegateField.Name, newTarget, newMethodName);
		}
	}

	private void WriteBehavior(DelegateConnector connector, MonoBehaviour behavior)
	{
		bool hasWrittenHeader = false;

		foreach (FieldInfo fieldMember in behavior.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
		{
			if (typeof(Delegate).IsAssignableFrom(fieldMember.FieldType))
			{
				if (!hasWrittenHeader)
				{
					WriteBehaviorHeader(behavior);
					hasWrittenHeader = true;
				}

				WriteDelegateField(connector, behavior, fieldMember);
			}
		}

		if (hasWrittenHeader)
		{
			EditorGUILayout.Separator();
		}
	}

	public override void OnInspectorGUI()
	{
		DelegateConnector connector = (DelegateConnector)target;
		//connector.Clean();
		connector.VerifyDelegateBinding();

		foreach (MonoBehaviour behavior in connector.GetComponents<MonoBehaviour>())
		{
			WriteBehavior(connector, behavior);
		}
		
		UnityEngine.Object prefabInstance = PrefabUtility.GetPrefabObject(connector);
		
		if (prefabInstance != null)
		{
			PrefabUtility.RecordPrefabInstancePropertyModifications(connector);
		}
	}
}
