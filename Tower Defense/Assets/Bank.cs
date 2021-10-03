using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI label;
    public int CurrentBalance{get{ return currentBalance;}}

    void Awake() {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdrawal(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if(currentBalance < 0){
            ReloadScene();
            
        }
    }

    void UpdateDisplay(){
        label.text = "Gold:"+ currentBalance;
    }
    void ReloadScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

   
}
