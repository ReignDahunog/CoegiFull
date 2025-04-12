using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public virtual void changeText(float speed)
    {
        float s = speed * 2.23694f;
        text.text = Mathf.Clamp(Mathf.Round(s), 0f, 500f) + " mph";
    }
}
