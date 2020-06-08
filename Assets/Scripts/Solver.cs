using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Solver : MonoBehaviour
{
    public bool IsStepSolverRunning = false;

    [SerializeField] private List<Equation> availableEquations;
    [SerializeField] private Vector3 initialState;
    [SerializeField] private Plotter plotter;
    [SerializeField] private float epsilon = 0.0001f;
    [SerializeField] private float dT = 0.01f;
    [SerializeField] private float stepSolverDT = 0.0001f;
    private Equation equation;
    private Vector3 currentSolution;
    private Vector3[] solution;
    private int currentEquationNum = 0;
    private int maxNumSolutions = 10000;
    private int numSolutions = 10000;
    private float solutionScaler = 1f;
    private SimpleGui simpleGui;

    private void Start()
    {
        simpleGui = new SimpleGui();
        solution = new Vector3[maxNumSolutions];
        //plotter.Initialize(GetComponent<MeshFilter>().mesh);
        AssignEquation(currentEquationNum);
    }

    private void Update()
    {
        Solve();
        if (!IsStepSolverRunning)
            plotter.Show(solution);
    }

    public void Animate()
    {
        if (!IsStepSolverRunning)
            StartCoroutine(RunStepSolver());
        else
            IsStepSolverRunning = false;
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

                if (IsStepSolverRunning)
                    IsStepSolverRunning = false;

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
        IsStepSolverRunning = true;
        var tempNumSolutions = maxNumSolutions;
        maxNumSolutions = 0;
        float currentDelay = 0f;
        for(int i = 0; i < tempNumSolutions; i++)
        {
            maxNumSolutions++;

            if (!IsStepSolverRunning)
                break;

            currentDelay += stepSolverDT;
            if(currentDelay >= Time.deltaTime)
            {
                currentDelay = 0f;
                plotter.Show(solution);
                yield return null;
            }
        }
        IsStepSolverRunning = false;
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

    public void SetPreviousEquation()
    {
        currentEquationNum = currentEquationNum == 0 ? availableEquations.Count-1 : --currentEquationNum;
        AssignEquation(currentEquationNum);
    }

    private void OnGUI()
    {
        simpleGui.Render(ref equation, ref dT, ref maxNumSolutions, ref epsilon, ref stepSolverDT, ref solutionScaler, ref initialState);
    }
}

