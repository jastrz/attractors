using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "equation", menuName = "ScriptableObjects/RosslerEquation", order = 1)]
public class RosslerEquation : Equation
{
    public override string GetName()
    {
        return "Rossler Attractor";
    }

    public override Vector3 Step(Vector3 currentState)
    {
        return new Vector3(
            -currentState.y - currentState.z,
            currentState.x + parameters.List[0].value * currentState.y,
            parameters.List[1].value + currentState.z * (currentState.x - parameters.List[2].value)
        );
    }
}
