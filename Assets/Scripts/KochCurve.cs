using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LindenmayerSystem
{
    public class KochCurve : MonoBehaviour
    {
        public GameObject branch;
        public GameObject tree;
        private float length=5f;

        private int n;
        private float angle;

        private string axiom;
        private string currentString;
        private Dictionary<char, string> rules;
        private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();

        public Trees trees;

        private void Awake()
        {
            TreeSwitch();
        }

        private void TreeSwitch()
        {
            switch (trees)
            {
                case Trees.Tree1:
                    n = 5;
                    angle = 25.7f;
                    axiom = "F";

                    rules = new Dictionary<char, string>
                    {
                        {'F', "F[+F]F[-F]F"}
                    };

                    length = length / 5.1f;
                    break;

                case Trees.Tree2:
                    n = 5;
                    angle = 20f;
                    axiom = "F";

                    rules = new Dictionary<char, string>
                    {
                        { 'F', "F[+F]F[-F][F]"}
                    };

                    length = length / 1.35f;
                    break;

                case Trees.Tree3:
                    n = 4;
                    angle = 22.5f;
                    axiom = "F";

                    rules = new Dictionary<char, string>
                    {
                        { 'F', "FF-[-F+F+F]+[+F-F-F]"}
                    };

                    length = length / 1f;
                    break;


                case Trees.Tree4:
                    n = 7;
                    angle = 20f;
                    axiom = "X";
                    
                    rules = new Dictionary<char, string>
                    {
                        { 'X',"F[+X]F[-X]+X" },
                        {'F',"FF" }
                    };
                    
                    length = length / 5f;
                    break;

                case Trees.Tree5:
                    n = 7;
                    angle = 25.7f;
                    axiom = "X";

                    rules = new Dictionary<char, string>
                    {
                        {'X',"F[+X][-X]FX"},
                        {'F',"FF" }
                    };

                    length = length / 6f;
                    break;

                case Trees.Tree6:
                    n = 5;
                    angle = 22.5f;
                    axiom = "X";

                    rules = new Dictionary<char, string>
                    {
                        {'X', "F-[[X]+X]+F[+FX]-X"},
                        {'F',"FF" }
                    };

                    length = length / 1f;
                    break;
            }
        }


        void Start()
        {
            currentString = axiom;

            for (int i = 0; i < n; i++)
            {
                Generate();
            }
        }

        void Generate()
        {

            string newString = "";

            char[] stringCharacters = currentString.ToCharArray();

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if (rules.ContainsKey(currentCharacter))
                {
                    //Debug.Log(currentCharacter);
                    newString += rules[currentCharacter];
                }
                else
                {
                    //Debug.Log(currentCharacter);
                    newString += currentCharacter.ToString();
                }
            }

            currentString = newString;
            //Debug.Log(currentString);

            stringCharacters = currentString.ToCharArray();

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if(currentCharacter=='X')
                {

                }

                else if (currentCharacter=='F')
                {

                    Vector3 intialPosition = transform.position;
                    transform.Translate(Vector3.forward * length);

                    GameObject treeSegment = Instantiate(branch);

                    treeSegment.gameObject.transform.SetParent(tree.transform);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                }

                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.up * -angle);
                }

                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.up * angle);
                }

                else if (currentCharacter == '[')
                {
                    TransformInfo ti = new TransformInfo();
                    ti.position = transform.position;
                    ti.rotation = transform.rotation;
                    transformStack.Push(ti);
                }

                else if (currentCharacter == ']')
                { 
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                }
            }
        }
    }
}
