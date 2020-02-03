using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "equation", menuName = "ScriptableObjects/IkedaEquation", order = 1)]
public class IkedaEquation : Equation
{
    public override string GetName()
    {
        return "Ikeda Map";
    }

    public override Vector3 Step(Vector3 currentState)
    {
        float tn = .4f - (6f / (1 + Mathf.Pow(currentState.x, 2) + Mathf.Pow(currentState.y, 2))) * Mathf.Deg2Rad;
        return new Vector3(
            1 + parameters.List[0].value * (currentState.x * Mathf.Cos(tn) - currentState.y * Mathf.Sin(tn)),
            parameters.List[0].value * (currentState.x * Mathf.Sin(tn) + currentState.y * Mathf.Cos(tn)),
            0);
    }
}
