using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LindenmayerSystem
{
    public class KochCurve : MonoBehaviour
    {
        public GameObject branch;
        private float length;

        private int n;
        private float angle;

        private string axiom;
        private string currentString;
        private Dictionary<char, string> rules;
        private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();

        TreesData treesData = new TreesData();

        public Trees trees=Trees.Tree1;

        private void Awake()
        {
            length = treesData.length;

            switch (trees)
            {
                default:
                    break;
                case Trees.Tree1:
                    TreeSettings temp1 = treesData.tree1;
                    n = temp1.n;
                    angle = temp1.angle;
                    axiom = temp1.axiom;
                    rules= new Dictionary<char, string>
                    { 
                        { temp1.F, temp1.rule}
                    };
                    length = length / 5.1f;
                    break;

                case Trees.Tree2:
                    TreeSettings temp2 = treesData.tree2;
                    n = temp2.n;
                    angle = temp2.angle;
                    axiom = temp2.axiom;
                    rules.Add(temp2.F, temp2.rule);
                    length = length / 1.35f;
                    break;

                case Trees.Tree3:
                    TreeSettings temp3 = treesData.tree3;
                    n = temp3.n;
                    angle = temp3.angle;
                    axiom = temp3.axiom;
                    rules.Add(temp3.F, temp3.rule);
                    length = length / 1f;
                    break;


                case Trees.Tree4:
                    TreeSettings temp4 = treesData.tree3;
                    n = temp4.n;
                    angle = temp4.angle;
                    axiom = "X";
                    
                    rules = new Dictionary<char, string>
                    {
                        { 'X',"F[+X]F[-X]+X" },
                        {'F',"FF" }
                    };
                    
                    length = length / 1f;
                    break;

                case Trees.Tree5:
                    TreeSettings temp5 = treesData.tree3;
                    n = temp5.n;
                    angle = temp5.angle;
                    axiom = temp5.axiom;
                    rules = new Dictionary<char, string>
                    {
                        {'X',"F[+X][-X]FX"},
                        {'F',"FF" }
                    };
                    length = length / 1f;
                    break;

                case Trees.Tree6:
                    TreeSettings temp6 = treesData.tree3;
                    n = temp6.n;
                    angle = temp6.angle;
                    axiom = temp6.axiom;
                    rules = new Dictionary<char, string>
                    {
                        {temp6.F,temp6.rule },
                        {'F',"FF" }
                    };
                    rules.Add('X', "F-[[X]+X]+F[+FX]-X");
                    rules.Add('F', "FF");
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
                    Debug.Log(currentCharacter);
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

                switch (currentCharacter)
                {
                    case 'X':
                        break;
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
                }
            }
        }
    }
}
