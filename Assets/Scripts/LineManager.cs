using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public List<float> Lines;
    public int DefaultLineId;

    private void Start()
    {
        if (Lines.Count == 0) Debug.LogError("Ни одна линия не назначена");
    }
}