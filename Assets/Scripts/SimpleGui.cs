using UnityEngine;

public class SimpleGui
{
    float x = 1, y = 1, z = 1;

    public void Render(ref Equation equation, ref float dT, ref int maxNumSolutions, ref float epsilon, ref float stepSolverDT, ref float solutionScaler, ref Vector3 initialState)
    {
        if (equation != null)
        {
            GUI.Label(new Rect(Screen.width - 200, 10, 150, 30), equation.GetName());
            for (int i = 0; i < equation.Parameters.List.Count; i++)
            {
                equation.Parameters.List[i].value =
                    GUI.HorizontalSlider(new Rect(Screen.width - 190, 45 + i * 40, 150, 30), equation.Parameters.List[i].value, -50f, 50f);
                GUI.Label(new Rect(Screen.width - 245, 45 + i * 40, 100, 30), equation.Parameters.List[i].name);
                GUI.Label(new Rect(Screen.width - 100, 55 + i * 40, 100, 30), equation.Parameters.List[i].value.ToString());
            }
        }

        dT = GUI.HorizontalSlider(new Rect(100, 45, 100, 30), dT, 0.0001F, 0.01F);
        GUI.Label(new Rect(5, 40, 100, 30), "dt");

        maxNumSolutions = (int)GUI.HorizontalSlider(new Rect(100, 85, 100, 30), maxNumSolutions, 1000, 100000);
        GUI.Label(new Rect(5, 80, 100, 30), "length");

        epsilon = GUI.HorizontalSlider(new Rect(100, 125, 100, 30), epsilon, 0.01F, 0.0000001F);
        GUI.Label(new Rect(5, 120, 100, 30), "accuracy");

        stepSolverDT = GUI.HorizontalSlider(new Rect(100, 165, 100, 30), stepSolverDT, 0.001f, 0.00001f);
        GUI.Label(new Rect(5, 160, 100, 30), "anim speed");

        solutionScaler = GUI.HorizontalSlider(new Rect(100, 200, 100, 30), solutionScaler, .1f, 10f);
        GUI.Label(new Rect(5, 200, 100, 30), "scaler");

        x = GUI.HorizontalSlider(new Rect(20, Screen.height - 50, 50, 30), x, -10f, 10f);
        GUI.Label(new Rect(25, Screen.height - 40, 50, 30), "x=" + x.ToString("#.##"));

        y = GUI.HorizontalSlider(new Rect(80, Screen.height - 50, 50, 30), y, -10f, 10f);
        GUI.Label(new Rect(85, Screen.height - 40, 50, 30), "y=" + y.ToString("#.##"));

        z = GUI.HorizontalSlider(new Rect(140, Screen.height - 50, 50, 30), z, -10f, 10f);
        GUI.Label(new Rect(145, Screen.height - 40, 50, 30), "z=" + z.ToString("#.##"));

        initialState = new Vector3(x, y, z);
    }
}
