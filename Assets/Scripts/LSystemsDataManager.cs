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

        public TextMeshProUGUI generationsText;
        public TextMeshProUGUI axiomText;
        public TextMeshProUGUI angleText;
        public TextMeshProUGUI ruleOneText;
        public TextMeshProUGUI ruleTwoText;

        private int gen;
        private float angle;
        private string axiom;
        private Dictionary<char, string> rules;


        public void GenerateData()
        {
            string temp = generationsText.GetComponent<TextMeshProUGUI>().text.ToString();

            int.TryParse(generationsText.GetParsedText(),out int value);
            Debug.Log("value: "+value);

            /* Debug Data
            Debug.Log(DataText("n", generationsText));
            Debug.Log(DataText("angle", angleText));
            Debug.Log(DataText("axiom", axiomText));
            Debug.Log(DataText("rule1", ruleOneText));
            Debug.Log(DataText("rule2", ruleTwoText));
            */

            kochCurve.GenerateNodeRewriting(2, 20, "F", rules = new Dictionary<char, string>
            {
                {'F',"FFFF[+F]" }
            }, 1.5f);
        }

        string DataText(string dataName,TextMeshProUGUI nameTemp)
        {
            string name;
            name = nameTemp.text.ToString();
            name = dataName + ": " + name;
            return name;
        }


    }
}