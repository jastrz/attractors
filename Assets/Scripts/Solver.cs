using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Solver : MonoBehaviour
{
    // currently processed equation
    [SerializeField] private Equation equation;
    [SerializeField] private List<Equation> availableEquations;
    private int currentEquationNum = 0;

    private Vector3 currentSolution;
    private Vector3[] solution;
    [SerializeField] private Vector3 initialState;
    [SerializeField] private Plotter plotter;

    // simulation stop treshold
    [SerializeField] private float epsilon = 0.0001f;
    [SerializeField] private float dT = 0.01f;
    [SerializeField] private float stepSolverDT = 0.0001f;

    private int maxNumSolutions = 10000;
    private int numSolutions = 10000;

    private bool isStepSolverRunning = false;
    private float solutionScaler = 1f;

    private void Start()
    {
        solution = new Vector3[maxNumSolutions];
        plotter.Initialize(GetComponent<MeshFilter>().mesh);
        AssignEquation(currentEquationNum);
    }

    private void Update()
    {
        Solve();
        if (!isStepSolverRunning)
            plotter.Show(solution);
    }

    public void Animate()
    {
        if (!isStepSolverRunning)
            StartCoroutine(RunStepSolver());
        else
            isStepSolverRunning = false;
    }

    private void Solve()
    {
        currentSolution = initialState;
        numSolutions = maxNumSolutions;

        ResetData(numSolutions, null);

        for (int i = 0; i < numSolutions; i++)
        {
            var nextState = equation.Step(currentSolution) * dT;
            var diff = (currentSolution - (currentSolution + nextState)).magnitude;

            if (diff < epsilon)
            {
                numSolutions = i;
                ResetData(i, solution);

                if (isStepSolverRunning)
                    isStepSolverRunning = false;

                break;
            }

            currentSolution += nextState;
            solution[i] = currentSolution * solutionScaler;
        }
    }

    private void ResetData(int num, Vector3[] startValues)
    {
        if (solution.Length != num)
        {
            solution = new Vector3[num];
            if (startValues != null)
                Array.Copy(startValues, 0, solution, 0, num);
        }
    }

    private IEnumerator RunStepSolver()
    {
        isStepSolverRunning = true;
        var tempNumSolutions = maxNumSolutions;
        maxNumSolutions = 0;
        float currentDelay = 0f;
        for(int i = 0; i < tempNumSolutions; i++)
        {
            maxNumSolutions++;

            if (!isStepSolverRunning)
                break;

            currentDelay += stepSolverDT;
            if(currentDelay >= Time.deltaTime)
            {
                currentDelay = 0f;
                plotter.Show(solution);
                yield return null;
            }
        }
        isStepSolverRunning = false;
        maxNumSolutions = tempNumSolutions;
    }

    private void AssignEquation(int num)
    {
        if (equation != null)
            Destroy(equation);

        equation = Instantiate(availableEquations[num]);
    }

    public void SetNextEquation()
    {
        currentEquationNum = currentEquationNum < availableEquations.Count-1 ? ++currentEquationNum : 0;
        AssignEquation(currentEquationNum);
    }

    float x = 1, y = 1, z = 1;
    private void OnGUI()
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
        GUI.Label(new Rect(25, Screen.height - 40, 50, 30), "x="+x.ToString("#.##"));

        y = GUI.HorizontalSlider(new Rect(80, Screen.height - 50, 50, 30), y, -10f, 10f);
        GUI.Label(new Rect(85, Screen.height - 40, 50, 30), "y="+y.ToString("#.##"));

        z = GUI.HorizontalSlider(new Rect(140, Screen.height - 50, 50, 30), z, -10f, 10f);
        GUI.Label(new Rect(145, Screen.height - 40, 50, 30), "z="+z.ToString("#.##"));


        initialState = new Vector3(x, y, z);
    }
}
