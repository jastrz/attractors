using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "equation", menuName = "ScriptableObjects/BucklingEquation", order = 1)]
public class BucklingEquation : Equation
{
    public override string GetName()
    {
        return "Buckling Column Model";
    }

    public override Vector3 Step(Vector3 currentState)
    {
        return new Vector3(
            currentState.y,
            -1 / parameters.List[3].value * (parameters.List[0].value * Mathf.Pow(currentState.x, 3) +
            parameters.List[1].value * currentState.x + parameters.List[2].value * currentState.y),
            0);
    }
}