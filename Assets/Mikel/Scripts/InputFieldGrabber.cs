using TMPro;
using UnityEngine;

public class InputFieldGrabber : MonoBehaviour
{
    [SerializeField] private string inputText;

    [SerializeField] private GameObject reactionGroup;
    [SerializeField] private TMP_Text reactionTextBox;


    public void GrabFromInputField(string input)
    {
        inputText = input;
        DisplayReactionToInput();
    }

    private void DisplayReactionToInput()
    {
        reactionTextBox.text = "blah blah blah, you typed: " + inputText + "!";
        reactionGroup.SetActive(true);
    }
}
