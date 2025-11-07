using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveButtons : MonoBehaviour
{
    [SerializeField] public Button rightButton;
    [SerializeField] public Button leftButton;

    [SerializeField] public Block block;
    
    AnswerButtons answerButtons;


    private void Awake()
    {
        block = FindObjectOfType<Block>();
        answerButtons = FindObjectOfType<AnswerButtons>();
        

        if (block == null)
        {
            Debug.LogError("Block nesnesi sahnede bulunamadý!");
        }
    }

    public void RightButtonAction()
    {
        block = FindObjectOfType<Block>();
        rightButton.gameObject.SetActive(true);

        rightButton.onClick.RemoveAllListeners();


        if (block != null) 
        {
            Debug.Log("Block bulundu: " + block.gameObject.name);
            rightButton.onClick.AddListener(() => block.HandleMovement(Vector2Int.right));
        }
        else
        {
            Debug.LogError("Block referansý null! Hareket fonksiyonu çaðrýlamaz.");
        }

    }
    public void LeftButtonAction()
    {
        block = FindObjectOfType<Block>();
        leftButton.gameObject.SetActive(true);
        leftButton.onClick.RemoveAllListeners();

        if (block != null)
        {
            Debug.Log("Block bulundu: " + block.gameObject.name);
            leftButton.onClick.AddListener(() => block.HandleMovement(Vector2Int.left));
        }
        else
        {
            Debug.LogError("Block referansý null! Hareket fonksiyonu çaðrýlamaz.");
        }
    }
}
