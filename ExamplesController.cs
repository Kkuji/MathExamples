using UnityEngine;
using UnityEngine.UI;

public class ExamplesController : MonoBehaviour
{
    private string[] examples = new string[10]; // всего примеров
    private string symbols = "(+-*:"; // всего примеров
    private int numbersInSummand;
    private int currentSummand;
    private int j;
    private int i;
    public static int examplesQuantity;
    private int currentIndex;
    private bool currentNumberX;
    private bool firstSymbolMinus = false;
    private bool specialSymbol = false;
    private int spaceNumber;
    public static string rightAnswer;
    private float rightAnswerFl;
    public static float[] wrongAnswers = new float[3];
    private char[] exampleChar;
    public static char[] finalChar;
    public Text myText;
    public InputField examplesNumber;

    void Start()
    {
        examples[0] = "2*6+5=17";
        examples[1] = "-2.5:0.5+2=7";
        examples[2] = "5-3--4=6";
        examples[3] = "2-(6+3)=-7";
        examples[4] = "5*3+2=17";
        examples[5] = "(4+3):7=1";
        examples[6] = "10+-5*2=0";
        examples[7] = "5*3+1=16";
        examples[8] = "3*2+5-2=9";
        examples[9] = "2*(13-6)=14";
        CreateX(examples[2], 2, 1);
    }

    public void CreateX(string example, int number, int way) //number - номер слагаемого заменяемого на Х; way - 1 или 2 способ определения
                                                             //неправильных ответов, согласно описанию тестового задания
    {
        spaceNumber = 0;
        currentSummand = 0;
        numbersInSummand = 0;
        firstSymbolMinus = false;
        exampleChar = example.ToCharArray();
        for (i = 0; i < exampleChar.Length; i++)
        {
            CheckSpecialSymbols(exampleChar[i]);

            if (char.IsDigit(exampleChar[i]) || exampleChar[i] == '.')
                numbersInSummand++;

            else if (!firstSymbolMinus && exampleChar[0] == '-' && exampleChar[1] != '(')
            {
                numbersInSummand++;
                firstSymbolMinus = true;
            }

            else if (i > 0 && specialSymbol && exampleChar[i] == '-' && !char.IsDigit(exampleChar[i - 1]))
                numbersInSummand++;

            else if (exampleChar[i] != '(' && i != currentIndex && (i > 0 && char.IsDigit(exampleChar[i - 1])))
            {
                currentIndex = i;
                currentSummand++;
                if (currentSummand == number)
                {
                    finalChar = new char[exampleChar.Length - numbersInSummand + 1];

                    for (int c = i - numbersInSummand; c != i; c++)
                        rightAnswer += exampleChar[c];

                    exampleChar[i - numbersInSummand] = 'X';
                    int l = 0;

                    for (j = 0; j < finalChar.Length + spaceNumber; j++)
                    {
                        if (exampleChar[j] == 'X')
                        {
                            currentNumberX = true;

                            if (spaceNumber > 0)
                                finalChar[j - spaceNumber] = 'X';

                            else
                                finalChar[j] = 'X';

                            l++;
                        }
                        else if (currentNumberX)
                        {
                            if (char.IsDigit(exampleChar[j]) || exampleChar[j] == '.')
                            {
                                spaceNumber++;
                                l++;
                            }
                            else
                            {
                                currentNumberX = false;

                                if (spaceNumber > 0)
                                    finalChar[j - spaceNumber] = exampleChar[l];

                                else
                                    finalChar[j] = exampleChar[l];

                                l++;
                            }
                        }
                        else
                        {
                            if (spaceNumber > 0)
                                finalChar[j - spaceNumber] = exampleChar[l];

                            else
                                finalChar[j] = exampleChar[l];

                            l++;
                        }
                    }
                    break;
                }
                numbersInSummand = 0;
            }

        }
        if (way == 1)//рандомно
        {

            float.TryParse(rightAnswer, out rightAnswerFl);
            for (int j = 0; j < wrongAnswers.Length; j++)
            {
                int randMultiply = Random.Range(1, 11);
                int plusOrminus = Random.Range(1, 3);
                if (plusOrminus == 1)
                    wrongAnswers[j] = rightAnswerFl + randMultiply;
                if (plusOrminus == 2)
                    wrongAnswers[j] = rightAnswerFl - randMultiply;
            }
        }
        /*if (way == 2)//вручную
        {
            wrongAnswers[0] =
            wrongAnswers[1] =
            wrongAnswers[2] =
        }*/
    }
    public bool CheckSpecialSymbols(char value)
    {
        specialSymbol = false;
        if (!char.IsDigit(value))
        {
            for (int q = 0; q < symbols.Length; q++)//для уменьшения проверок в строке 57
            {
                if (symbols[q] == value)
                    specialSymbol = true;
            }
        }
        return specialSymbol;
    }
}
