using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LSystemsDataManager : MonoBehaviour
{
    [SerializeField] private float angle = 20;
    public TextMeshProUGUI angleData;

    private void Awake()
    {
        angleData.text = angle.ToString();
    }

    public void IncrementAngle()
    {
        angle++;
        angleData.text = angle.ToString();

    }

    public void DecrmentAngle()
    {
        if (angle <= 0)
            return;
        angle--;
        angleData.text = angle.ToString();
    }
}
