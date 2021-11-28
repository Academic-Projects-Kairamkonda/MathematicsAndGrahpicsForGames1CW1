using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LindenmayerSystem
{
    public class KochCurve : MonoBehaviour
    {
        public GameObject branch;
        public GameObject tree;
        private GameObject tempTree;
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
            TreeType();
        }

        private void TreeType()
        {
            length = 5f;

            if (tempTree!=null)
            {
                Destroy(tempTree);
            }

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

                    length = length / 5f;
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

                    length = length / 5f;
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

                    length = length / 1.5f;
                    break;
            }

            GenerateNodeRewriting();
        }


        public void Tree1()
        {
            trees = Trees.Tree1;
            TreeType();
        }

        public void Tree2()
        {
            trees = Trees.Tree2;
            TreeType();
        }

        public void Tree3()
        {
            trees = Trees.Tree3;
            TreeType();
        }

        public void Tree4()
        {
            trees = Trees.Tree4;
            TreeType();
        }

        public void Tree5()
        {
            trees = Trees.Tree5;
            TreeType();
        }

        public void Tree6()
        {
            trees = Trees.Tree6;
            TreeType();
        }

        public void GenerateTree()
        {
            trees = Trees.Tree3;
            TreeType();
        }

        void Start()
        {
            /*
            currentString = axiom;

            for (int i = 0; i < n; i++)
            {
                GenerateEdgeRewriting();
            }
            */

           // GenerateNodeRewriting();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextGame();
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                PreviousGame();
            }


        }

        public void NextGame()
        {
            trees = Trees.Tree4;
            TreeType();
        }

        public void PreviousGame()
        {
            trees = Trees.Tree2;
            TreeType();
        }

        //Node rewriting
        void GenerateNodeRewriting()
        {
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;

            currentString = axiom;

            tempTree = Instantiate(tree);
            tempTree.name = axiom;
         

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                foreach (char c in currentString)
                {
                    sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
                }

                currentString = sb.ToString();
                sb = new StringBuilder();
            }

            //Debug.Log(currentString);

            for (int i = 0; i < currentString.Length; i++)
            {
                char currentCharacter = currentString[i];

                if (currentCharacter == 'X')
                {

                }

                else if (currentCharacter == 'F')
                {
                    Vector3 intialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    /*
                    Debug.DrawLine(intialPosition, transform.position, Color.green, 100000f);
                    */

                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    treeSegment.gameObject.transform.SetParent(tempTree.transform);
                }

                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.forward * angle);
                }

                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.forward * -angle);
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

        //Edge rewriting
        void GenerateEdgeRewriting()
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
                    transform.Translate(Vector3.up * length);

                    /*
                    Debug.DrawLine(intialPosition, transform.position, Color.green, 100000f);
                    */

                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    treeSegment.gameObject.transform.SetParent(this.transform);
                }

                else if (currentCharacter == '+')
                {
                    transform.Rotate(Vector3.forward * angle);
                }

                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.forward * -angle);
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
