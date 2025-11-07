using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButtons : MonoBehaviour
{
    [SerializeField] public Button[] answerButtons; // 4 buton

    MathQuestionGenerator questionGenerator;
    public GameBoard board;
    public Block block;
    public MoveButtons moveButtons;


    private int correctAnswerIndex;
    private int correctAnswer;
    private void Awake()
    {
        moveButtons = FindObjectOfType<MoveButtons>();
        board = FindObjectOfType<GameBoard>();
        block = FindObjectOfType<Block>();
        questionGenerator = FindObjectOfType<MathQuestionGenerator>();

    }

    public void AssignAnswers()
    {
        correctAnswerIndex = UnityEngine.Random.Range(0, answerButtons.Length);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answer;
            if (i == correctAnswerIndex)
            {
                answer = questionGenerator.correctAnswer;

            }
            else
            {
                do
                {
                    answer = UnityEngine.Random.Range(questionGenerator.correctAnswer - 5, questionGenerator.correctAnswer + 5);
                }
                while (answer == questionGenerator.correctAnswer);
            }

            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = answer.ToString();

            int capturedAnswer = answer; // Lambda fonksiyonunda kullanmak için
            answerButtons[i].onClick.RemoveAllListeners();

            if (i == correctAnswerIndex)
            {
                answerButtons[i].onClick.AddListener(() => CorrectAnswerSelected());
            }
            else
            {
                answerButtons[i].onClick.AddListener(() => WrongAnswerSelected());
            }
        }
    }


    public void CorrectAnswerSelected()
    {
        Debug.Log("Doðru cevap! Blok yok edilecek.");

        if (block == null)
        {
            Debug.LogError("Blok referansý null! Silme iþlemi gerçekleþtirilemiyor.");
            return;
        }

        board.Clear(block); // Blok yok et
        questionGenerator.GenerateQuestion();
    }

    public void WrongAnswerSelected()
    {
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        moveButtons.RightButtonAction();
        moveButtons.LeftButtonAction();
    }
}

