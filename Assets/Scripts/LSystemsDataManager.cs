using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LindenmayerSystem
{

    public class LSystemsDataManager : KochCurve
    {
        private float angle = 20;

        //public KochCurve kochCurve;

        //public TextMeshProUGUI generations;
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


        private void Update()
        {
            //generations.text = kochCurve.name;
        }
    }
}