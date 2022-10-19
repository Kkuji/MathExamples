using UnityEngine;
using UnityEngine.UI;


public class CheckExamplesNumber : MonoBehaviour
{
    private bool rightAnswer;
    private Text inButtons;
    public InputField examplesNumber;
    public GameObject examplesField;
    public GameObject panel;
    public GameObject text;
    public Text textExample;
    public Button[] buttons = new Button[4];
    private Animator[] buttonsAnim = new Animator[4];
    int j;

    private void Start()
    {

        for (int i = 0; i < buttons.Length; i++)
            buttonsAnim[i] = buttons[i].GetComponent<Animator>();
    }

    public int CheckNumberOfExamples()
    {
        int.TryParse(examplesNumber.text, out j);

        if (j > 0 && j < 21)
        {
            return j;
        }
        else
            return 0;
    }

    public void GetNumberOfExamples()
    {
        ExamplesController.examplesQuantity = CheckNumberOfExamples();
        if (ExamplesController.examplesQuantity > 0 && ExamplesController.examplesQuantity < 21)
        {
            examplesField.SetActive(false);
            text.SetActive(false);
            Animator exampleAnim = panel.GetComponent<Animator>();
            exampleAnim.SetTrigger("Smaller");
            Invoke(nameof(DelayButtons), 1f);
        }
    }

    public void DelayButtons()
    {
        Animator exampleAnim = panel.GetComponent<Animator>();
        for (int i = 0; i < buttons.Length; i++)
            buttonsAnim[i].SetTrigger("MoveFromCenter");

        exampleAnim.SetTrigger("Bigger");
        Invoke(nameof(ShowNumbers), 1f);
    }

    public void ShowNumbers()
    {
        rightAnswer = false;
        textExample.transform.SetSiblingIndex(9);
        for (int i = 0; i < ExamplesController.finalChar.Length; i++)
            textExample.text += ExamplesController.finalChar[i];
        int writeButton = Random.Range(0, 4);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == writeButton)
            {
                inButtons = buttons[i].GetComponentInChildren<Text>();
                inButtons.text = ExamplesController.rightAnswer;
                rightAnswer = true;
            }

            else
            {
                inButtons = buttons[i].GetComponentInChildren<Text>();
                if (rightAnswer)
                    inButtons.text = ExamplesController.wrongAnswers[i - 1].ToString();
                else
                    inButtons.text = ExamplesController.wrongAnswers[i].ToString();
            }
        }
    }
}
