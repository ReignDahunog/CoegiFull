using Logitech;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public static Steering Instance; // Singleton reference

    public float xAxes, GasInput, BrakeInput, ClutchInput;
    public int CurrentGear;

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

            xAxes = rec.lX / 32768f;
            GasInput = rec.lY < 0 ? rec.lY / -32768f : 0;
            BrakeInput = rec.lRz < 0 ? rec.lRz / -32768f : 0;
            ClutchInput = rec.rglSlider[0] < 0 ? rec.rglSlider[0] / -32768f : 0;

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
                if (ClutchInput > 0.5f)
                {
                    switch (i)
                    {
                        case 12: CurrentGear = -1; break;
                        case 18: CurrentGear = 1; break;
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
            CurrentGear = 0;
        }
    }
}
