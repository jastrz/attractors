using UnityEngine;

public abstract class Equation : ScriptableObject
{
    [SerializeField] protected EquationParams parameters;

    public EquationParams Parameters => parameters;
    public abstract Vector3 Step(Vector3 currentState);
    public abstract string GetName();
}