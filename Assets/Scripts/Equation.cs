using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Equation : ScriptableObject
{
    [SerializeField] protected EquationParams parameters;

    public EquationParams Parameters => parameters;
    public abstract Vector3 Step(Vector3 currentState);
    public abstract string GetName();
}

[System.Serializable]
public class EquationParams : IEnumerable<Parameter>
{
    public List<Parameter> List;
    public IEnumerator<Parameter> GetEnumerator()
    {
        foreach (var parameter in List)
            yield return parameter;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[System.Serializable]
public class Parameter
{
    public string name;
    public float value;
}