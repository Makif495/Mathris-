using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MathQuestionGenerator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText; // Soru görüntülenecek alan
    
    private int correctAnswerIndex;
    public int correctAnswer;
    public Block block;
    public GameBoard board;
    AnswerButtons answerButtons;




    private void Awake()
    {
        board = FindObjectOfType<GameBoard>();
        block = FindObjectOfType<Block>();
        answerButtons = FindObjectOfType<AnswerButtons>();


    }

    private void Start()
    {
        GenerateQuestion();
      
    }




    public void GenerateQuestion()
    {
        int num1 = UnityEngine.Random.Range(1, 10);
        int num2 = UnityEngine.Random.Range(1, 10);
        correctAnswer = num1 + num2;
        questionText.text = $"{num1} + {num2} = ?";

        answerButtons.AssignAnswers();

        board.SpawnPiece();
        block = FindObjectOfType<Block>();

        if (block == null)
        {
            Debug.LogError("Block referansý null! Yeni parça spawn edildikten sonra bulunamadý.");
        }
    }




    private void CheckAnswer(int selectedAnswer)
    {
        Debug.Log($"CheckAnswer çaðrýldý! Seçilen: {selectedAnswer}, Doðru: {correctAnswer}");

        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Doðru cevap! Blok yok edilecek.");

            if (block == null)
            {
                Debug.LogError("Blok referansý null! Silme iþlemi gerçekleþtirilemiyor.");
                return;
            }

            FindObjectOfType<GameBoard>().Clear(block); // Blok silme fonksiyonu çaðrýlýyor
        }
        else
        {
            Debug.Log("Yanlýþ cevap! Blok düþmeye devam edecek.");
        }

        GenerateQuestion(); // Yeni soru oluþtur
    }


}
