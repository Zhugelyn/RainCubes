using System;

public interface IDeactivable<T> 
{
    public event Action<T> Deactivation;
}
