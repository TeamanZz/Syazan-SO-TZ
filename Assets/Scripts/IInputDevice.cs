using System;

public interface IInputDevice
{
    Action OnStartInput { get; set; }
    Action OnEndInput { get; set; }

    float GetHorizontalInput();
    float GetVerticalInput();
}