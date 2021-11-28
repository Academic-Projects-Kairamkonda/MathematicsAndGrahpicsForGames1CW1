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

        private const string checker1 = "F[+F]F[-F][F]";
        private const string checker2 = "FF-[-F+F+F]+[+F-F-F]";


        public void GenerateData()
        {
            axiom = textData.axiomText.text.ToString();
            ruleOne = textData.ruleOneText.text.ToString();

            if(string.IsNullOrEmpty(textData.ruleTwoText.text))
            {
                Debug.Log("it is mty");
            }

            kochCurve.GenerateNodeRewriting(3, 20, axiom, rules = new Dictionary<char, string>
            {
                {'F',ruleOne}
            }, 3f);
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