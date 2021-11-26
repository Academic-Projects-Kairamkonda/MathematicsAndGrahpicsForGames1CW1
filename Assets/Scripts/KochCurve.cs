using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LindenmayerSystem
{
    [System.Serializable]
    public class TreeSettings
    {
        public string axiom;
        public float angle;
        public int generations;
        public char F;
        public string rule;

        public TreeSettings(string axiom, float angle, int gen, char F,string rule)
        {
            this.axiom = axiom;
            this.angle = angle;
            this.generations = gen;
            this.F = F;
            this.rule = rule;
        }
    }

    public class KochCurve : MonoBehaviour
    {
        //Four references in the code to make changes for another tree
        private TreeSettings tree1 = new TreeSettings("F", 25.7f, 5, 'F',"F[+F]F[-F]F");
        private TreeSettings tree2 = new TreeSettings("F", 20f, 5, 'F', "F[+F]F[-F][F]");
        private TreeSettings tree3 = new TreeSettings("F", 22.5f, 4, 'F', "FF-[-F+F+F]+[+F-F-F]");

        //Checking the count of the tree generation
        private int generationCount = 1;

        //Starting state of the tree
        private string axiom;

        //String rules
        private string currentString;

        //Dictionary to store the tree structure for recursive
        private Dictionary<char, string> rules = new Dictionary<char, string>();

        //stacking of a string
        private Stack<TransformInfo> transformStack= new Stack<TransformInfo>();

        // length of line between two points
        private float length;

        //Angle to rotate from a point or position
        private float angle;

        //boolean for generating the tree
        private bool isGenerating;

        //Tree used to destroy the whole tree if there is any change and generate new one
        [SerializeField] private GameObject tree;

        //This branch is used to draw line on Game Scene 
        [SerializeField]private GameObject branch;

        void Start()
        {
            TreeSettings temp = tree1;

            //Adding rules according to the tree
            axiom = temp.axiom;
            rules.Add(temp.F, temp.rule);
            angle = temp.angle;


            //Adding the starting position to current string
            currentString = axiom;

            //Length of the each branch
            length = 5f;

            //StartCoroutine("GenerateTree");
            for (int i = 0; i <temp.generations+1; i++)
            {
                Generate();
            }
        }

        void Update()
        {
            /*
            TreeSettings temp = tree3;

            if (generationCount > temp.generations)
            {
                StopCoroutine("GenerateTree");
            }
            */
        }


        /// <summary>
        /// Populate the tree accorindg the time 
        /// </summary>
        /// <returns> a generation of a tree</returns>
        IEnumerator GenerateTree()
        {
            int count = 0;

            while(count<5)
            {
                if (!isGenerating)
                {
                    isGenerating = true;
                    StartCoroutine("Generate");
                    generationCount++;
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        /// <summary>
        /// Generating the whole tree
        /// </summary>
        void Generate()
        {

            length = length / 1.5f;
            //Creating a new string, used to add the newstring to current string
            string newString = "";

            //adding the charavcter to the list
            char[] stringCharacters = currentString.ToCharArray();


            //Adding the character to string
            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if (rules.ContainsKey(currentCharacter))
                {
                    newString += rules[currentCharacter];
                }

                else
                {
                    newString += currentCharacter.ToString();
                }
            }

            //the total data is used to save in to stringCharacters in char array
            currentString = newString;

            Debug.Log(currentString);

            for (int i = 0; i < stringCharacters.Length; i++)
            {
                char currentCharacter = stringCharacters[i];

                if (currentCharacter=='F')
                {
                    //Move forward
                    Vector3 intialPosition = transform.position;
                    transform.Translate(Vector3.forward * length);

                    GameObject treeSegment = Instantiate(branch);
                    treeSegment.gameObject.transform.SetParent(this.transform);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, intialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    //Debug.DrawLine(intialPosition, transform.position, Color.green, 100000f, false);
                    //yield return null;
                }
                else if(currentCharacter=='+')
                {
                    transform.Rotate(Vector3.up * -angle);
                }
                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.up * angle);
                }
                else if(currentCharacter == '[')
                {
                    TransformInfo ti = new TransformInfo();
                    ti.position = transform.position;
                    ti.rotation = transform.rotation;
                    transformStack.Push(ti);
                }
                else if(currentCharacter==']')
                {
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                }
            }

            isGenerating = false;
        }
    }
}
