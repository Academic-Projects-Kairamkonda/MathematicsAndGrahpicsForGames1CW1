using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LindenmayerSystem
{
    public class KochCurve : MonoBehaviour
    {
        //Checking the count of the tree generation
        private int generationCount = 0;

        [SerializeField ]private int generations = 0;

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

        void Start()
        {
            axiom = "F";
            rules.Add('F',"FF+[+F-F-F]-[-F+F+F]");
            angle = 25f;
            length = 10f;

            //Adding the starting position to current string
            currentString = axiom;

            StartCoroutine("GenerateTree");
        }

        void Update()
        {
            if (generationCount>=generations)
            {
                StopAllCoroutines();
            }
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
        IEnumerator Generate()
        {
            length = length / 2f;

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
                    Debug.DrawLine(intialPosition, transform.position, Color.white, 100000f, false);
                    yield return null;
                }
                else if(currentCharacter=='+')
                {
                    transform.Rotate(Vector3.up * angle);
                }
                else if (currentCharacter == '-')
                {
                    transform.Rotate(Vector3.up * -angle);
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
            generationCount++;
        }
    }
}
