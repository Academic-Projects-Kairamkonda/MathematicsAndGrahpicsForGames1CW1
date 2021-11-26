using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LindenmayerSystem
{
    public class KochCurve : MonoBehaviour
    {
        public GameObject branch;
        private float length=5f;

        private int n=4;
        private float angle=22.5f;

        private string axiom="F";
        private string currentString;
        private Dictionary<char, string> rules= new Dictionary<char, string>();
        private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();

        void Start()
        {
            rules.Add('F', "FF-[-F+F+F]+[+F-F-F]");
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
                    newString += rules[currentCharacter];
                }
                else
                {
                    //Debug.Log(currentCharacter);
                    newString += currentCharacter.ToString();
                }
            }

            currentString = newString;
            Debug.Log(currentString);

            stringCharacters = currentString.ToCharArray();

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                switch (currentCharacter)
                {
                    case 'F':
                    Vector3 intialPosition = transform.position;
                    transform.Translate(Vector3.forward * length);

                    GameObject treeSegment = Instantiate(branch);

                    treeSegment.gameObject.transform.SetParent(this.transform);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                        break;

                    case '+':
                    transform.Rotate(Vector3.up * -angle);
                        break;
    
                    case '-':
                    transform.Rotate(Vector3.up * angle);
                    break;

                    case '[':
                        TransformInfo ti = new TransformInfo();
                        ti.position = transform.position;
                        ti.rotation = transform.rotation;
                        transformStack.Push(ti);
                    break;

                    case ']':
                        TransformInfo t = transformStack.Pop();
                        transform.position = t.position;
                        transform.rotation = t.rotation;
                    break;

                    case 'X':
                    break;
                }
            }
        }
    }
}
