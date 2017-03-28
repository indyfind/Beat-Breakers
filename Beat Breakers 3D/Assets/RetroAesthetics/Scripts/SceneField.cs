﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RetroAesthetics {
	[System.Serializable]
	public class SceneField
	{
		[SerializeField]
		private Object m_SceneAsset;
		[SerializeField]
		private string m_SceneName = "";
		
		public string SceneName
		{
			get { return m_SceneName; }
		}

		public static implicit operator string( SceneField sceneField )
		{
			return sceneField.SceneName;
		}
	}

	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(SceneField))]
	public class SceneFieldPropertyDrawer : PropertyDrawer 
	{
		public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
		{
			EditorGUI.BeginProperty(_position, GUIContent.none, _property);
			SerializedProperty sceneAsset = _property.FindPropertyRelative("m_SceneAsset");
			SerializedProperty sceneName = _property.FindPropertyRelative("m_SceneName");
			_position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
			if (sceneAsset != null)
			{
				sceneAsset.objectReferenceValue = EditorGUI.ObjectField(
					_position, sceneAsset.objectReferenceValue,	typeof(SceneAsset), false); 
				if (sceneAsset.objectReferenceValue != null )
				{
					sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
				}
			}
			if (sceneAsset != null)
			{
				EditorGUI.BeginChangeCheck();

				Object value = EditorGUI.ObjectField(
					_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
				if (EditorGUI.EndChangeCheck() )
				{
					sceneAsset.objectReferenceValue = value;
					if (sceneAsset.objectReferenceValue != null)
					{
						sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
					}
				}
			
			}
			EditorGUI.EndProperty();
		}
	}
	#endif
}