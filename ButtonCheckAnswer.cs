using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCheckAnswer : MonoBehaviour
{
    Text buttonText;
    Animator buttonAnim;
    public void CheckRight()
    {
        buttonText = this.GetComponentInChildren<Text>();
        buttonAnim = this.GetComponent<Animator>();
        if (buttonText.text == ExamplesController.rightAnswer)
            buttonAnim.SetTrigger("RightAnswer");
        else
            buttonAnim.SetTrigger("WrongAnswer");
    }
}
