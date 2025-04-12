using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public List<Light> lights;
    public virtual void ControlHeadlights()
    {
        foreach (Light light in lights)
        {
            light.intensity = light.intensity == 0 ? 1.5f : 0;
        }
    }

    public virtual void ControlTailLights()
    {
        
    }
}
