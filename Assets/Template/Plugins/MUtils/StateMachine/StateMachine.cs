using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MUnityUtils.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public List<StateData> states = new List<StateData>();
        private Dictionary<string, StateData> stateDict = new Dictionary<string, StateData>();
        private State activeState;
        private string activeStateName;
        private Coroutine coroutine;
        public string ActiveStateName => activeStateName;
        private void Awake()
        {
            foreach (var state in states)
            {
                stateDict[state.Name] = state;
                state.Component.StateMachine = this;
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void SetState(string state, float waitTime = 0f)
        {
            if (stateDict.Keys.Contains(state))
            {   if(stateDict[state].Component == activeState) return;
                activeStateName = state;
                activeState?.StateExit();
                if(coroutine!=null) StopCoroutine(coroutine);
                coroutine = StartCoroutine(WaitTime(waitTime, () =>
                {
                    activeState = stateDict[state].Component;
                    activeState.StateEnter();
                }));
            }
            else
            {
                Debug.LogError("State does not exist");
            }
        }

        private void Update() { activeState?.StateUpdate(); }
        private void FixedUpdate() { activeState?.StateFixedUpdate(); }

        private IEnumerator WaitTime(float time, Action callBack)
        {
            yield return new WaitForSeconds(time);
            callBack?.Invoke();
        }
    }
    [Serializable] public class StateData
    {
        public string Name;
        public State Component;
    }
}
