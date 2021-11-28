using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

namespace LindenmayerSystem
{
    public class LSystemsDataManager : MonoBehaviour
    {
        public KochCurve kochCurve;

        public TreesTextData textData;

        private int gen;
        private float angle;
        private string axiom;
        private string ruleOne;
        private Dictionary<char, string> rules;

        private const string checker = "F[+F]F[-F][F]";


        public void GenerateData()
        {
            /*
            kochCurve.GenerateNodeRewriting(5, 20, axiom, rules = new Dictionary<char, string>
            {
                {'F',ruleOne}
            }, 4.8f);
            */
        }


        string DataText(string dataName,TextMeshProUGUI nameTemp)
        {
            string name;
            name = nameTemp.text.ToString();
            name = dataName + ": " + name;
            return name;

            /* Debug Data
            Debug.Log(DataText("n", generationsText));
            Debug.Log(DataText("angle", angleText));
            Debug.Log(DataText("axiom", axiomText));
            Debug.Log(DataText("rule1", ruleOneText));
            Debug.Log(DataText("rule2", ruleTwoText));
            */
        }


    }
}