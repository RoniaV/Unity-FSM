using System.Collections.Generic;
using UnityEngine;
using UnityFSM;

/// <summary>Base class for FSM states; enter and exit map to the component being enabled and disabled.</summary>
[RequireComponent(typeof(UFSM))]
public class State : MonoBehaviour
{
    public delegate void StateDelegate();

    public StateDelegate OnEnterDelegate { get; set; }
    public StateDelegate OnFixedUpdateDelegate { get; set; }
    public StateDelegate OnUpdateDelegate { get; set; }
    public StateDelegate OnExitDelegate { get; set; }

    [HideInInspector] public UFSM fSM;
    [HideInInspector] public List<Transition> transitions = new List<Transition>();


    protected virtual void Awake()
    {
        if (fSM == null)
            fSM = GetComponent<UFSM>();
    }

    protected virtual void OnEnable()
    {
        OnEnterDelegate?.Invoke();
    }

    protected virtual void FixedUpdate()
    {
        OnFixedUpdateDelegate?.Invoke();
    }

    protected virtual void Update()
    {
        OnUpdateDelegate?.Invoke();
    }

    protected virtual void OnDisable()
    {
        OnExitDelegate?.Invoke();
    }

    public void AddTransition(string name)
    {
        transitions.Add(new Transition(name));
    }

    public void DeleteTransition(Transition transition)
    {
        transitions.Remove(transition);
    }

    public void DeleteAllTransitions()
    {
        transitions.Clear();
    }
}
