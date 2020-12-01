using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    [TextArea(1, 20)]
    public string[] sentences;
}
