using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public interface IMove
{
    public float Move { get; }
    public float Rotate { get; }
}

public interface IAction
{
    public bool Fire { get; }
    public bool Reload { get; }
    
}

public interface IInput : IMove, IAction
{
}

public interface IGameManager
{
    public bool IsGameover { get; }
}
