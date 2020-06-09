using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Parameter
{
    public string name;
    public float value;
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