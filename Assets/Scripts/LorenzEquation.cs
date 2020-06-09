using UnityEngine;

[CreateAssetMenu(fileName = "equation", menuName = "ScriptableObjects/LorenzEquation", order = 1)]
public class LorenzEquation : Equation
{
    public override string GetName()
    {
        return "Lorenz Attractor";
    }

    public override Vector3 Step(Vector3 currentState)
    {
        return new Vector3(
            parameters.List[0].value * (currentState.y - currentState.x),
            currentState.x * (parameters.List[1].value - currentState.z) - currentState.y,
            currentState.x * currentState.y - parameters.List[2].value * currentState.z
        );
    }
}