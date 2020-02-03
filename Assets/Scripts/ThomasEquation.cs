using UnityEngine;
using System.Collections;

// https://en.wikipedia.org/wiki/Thomas%27_cyclically_symmetric_attractor

[CreateAssetMenu(fileName = "equation", menuName = "ScriptableObjects/ThomasEquation", order = 1)]
public class ThomasEquation : Equation
{
    public override string GetName()
    {
        return "Thomas' attractor";
    }

    public override Vector3 Step(Vector3 currentState)
    {
        //currentState *= Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * currentState.y) - parameters.List[0].value * currentState.x,
            Mathf.Sin(Mathf.Deg2Rad * currentState.z) - parameters.List[0].value * currentState.y,
            Mathf.Sin(Mathf.Deg2Rad * currentState.x) - parameters.List[0].value * currentState.z);
    }
}
