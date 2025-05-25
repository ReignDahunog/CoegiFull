using Logitech;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public static Steering Instance; // Singleton

    public float SteeringAxis { get; private set; }
    public float GasInput { get; private set; }
    public float BrakeInput { get; private set; }
    public float ClutchInput { get; private set; }
    public int CurrentGear { get; private set; }

    private bool[] lastButtonStates = new bool[128];

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LogitechGSDK.LogiSteeringInitialize(false);
    }

    void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            var rec = LogitechGSDK.LogiGetStateUnity(0);

            // Correct pedal mappings for G29
            SteeringAxis = rec.lX / 32768f;
            GasInput = rec.lRz < 0 ? -rec.lRz / 32768f : 0;       // Gas = RZ
            BrakeInput = rec.lY < 0 ? -rec.lY / 32768f : 0;       // Brake = Y
            ClutchInput = rec.rglSlider[0] < 0 ? -rec.rglSlider[0] / 32768f : 0; // Clutch = Slider[0]

            HandleHShifter(rec);
        }
    }

    void HandleHShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter)
    {
        bool gearSelected = false;

        for (int i = 0; i < 128; i++)
        {
            bool isPressed = shifter.rgbButtons[i] == 128;

            if (isPressed && !lastButtonStates[i])
            {
                gearSelected = true;

                if (ClutchInput > 0.5f) // Clutch must be pressed to shift
                {
                    switch (i)
                    {
                        case 12: CurrentGear = -1; break; // Reverse
                        case 18: CurrentGear = 1; break;  // 1st
                        case 13: CurrentGear = 2; break;
                        case 14: CurrentGear = 3; break;
                        case 15: CurrentGear = 4; break;
                        case 16: CurrentGear = 5; break;
                        case 17: CurrentGear = 6; break;
                    }
                }
            }

            lastButtonStates[i] = isPressed;
        }

        if (!gearSelected && ClutchInput > 0.5f)
        {
            CurrentGear = 0; // Neutral
        }
    }
}
