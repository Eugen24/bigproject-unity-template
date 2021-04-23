using System;
using Template.Plugins.MUnityUtils.StateMachine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Template.Plugins.MUnityUtils.StateMachine.Editor
{
    [CustomEditor(typeof(MUnityUtils.StateMachine.StateMachine))]
    public class Editor : UnityEditor.Editor
    {
        private MUnityUtils.StateMachine.StateMachine stateMachine;

        private void OnEnable()
        {
            stateMachine = (MUnityUtils.StateMachine.StateMachine) target;
        }
        public override void OnInspectorGUI()
        {
            if (stateMachine.states.Count > 0)
            {
                foreach (var state in stateMachine.states)
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("X", GUILayout.Width(15), GUILayout.Height(15)))
                    {
                        stateMachine.states.Remove(state);
                        return;
                    }
                    EditorGUILayout.EndHorizontal();
                    state.Name = EditorGUILayout.TextField("Name", state.Name);
                    state.Component = EditorGUILayout.ObjectField("Component", state.Component, 
                        typeof(MonoBehaviour)) as State;
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                UnityEditor.EditorGUILayout.LabelField("No states in state machine");
            }
            if(GUILayout.Button("Add state", GUILayout.Height(30))) 
                stateMachine.states.Add(new StateData());
            if(GUI.changed) {
                EditorUtility.SetDirty(stateMachine.gameObject);
                EditorSceneManager.MarkSceneDirty(stateMachine.gameObject.scene);
            }
        }
    }
}
