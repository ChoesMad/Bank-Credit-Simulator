using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class LoanSystem : MonoBehaviour
{
    public GameObject loanPanel;
    public GameObject questionDialog;
    public GameObject errorDialog;
    public GameObject inputPanel;
    public GameObject enterBackPanel;
    public GameObject spaceLoginPanel;
    public GameObject confirmExitPanel;
    public TMP_InputField amountInput;
    public TMP_Text questionText;
    public TMP_Text errorText;
    private PlayerMovement currentPlayer;
    private Transform bankerTransform;
    private Animator playerAnimator;
    private bool waitingForReturn = false;
    private bool loanApprovedState = false;
    private bool confirmExitState = false;

    public void StartLoanConversation(PlayerMovement player)
    {
        currentPlayer = player;

        if (currentPlayer != null)
        {
            currentPlayer.enabled = false;

            playerAnimator = currentPlayer.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Walk", false);
            }
        }

        GameObject banker = GameObject.FindGameObjectWithTag("Banker");
        if (banker != null)
        {
            bankerTransform = banker.transform;
            RotatePlayerToBanker();
        }

        loanPanel.SetActive(true);
        ShowQuestion();
    }

    void RotatePlayerToBanker()
    {
        if (bankerTransform == null || currentPlayer == null)
            return;

        Vector3 direction = bankerTransform.position - currentPlayer.transform.position;
        direction.y = 0;

        currentPlayer.transform.rotation = Quaternion.LookRotation(direction);
    }

    void ShowQuestion()
    {
        waitingForReturn = false;
        loanApprovedState = false;
        confirmExitState = false;

        confirmExitPanel.SetActive(false);
        enterBackPanel.SetActive(false);
        spaceLoginPanel.SetActive(false);
        errorDialog.SetActive(false);
        questionDialog.SetActive(true);
        inputPanel.SetActive(true);

        questionText.text = "Good morning, please specify the loan amount you are interested in.";
        amountInput.text = "";

        EventSystem.current.SetSelectedGameObject(amountInput.gameObject);
        amountInput.ActivateInputField();
    }

    float CalculateMaxLoan(float income)
    {
        float maxMonthlyInstallment = income * 0.4f;
        int months = 60; 
        return maxMonthlyInstallment * months;
    }

    void ShowError(string msg)
    {
        waitingForReturn = true;

        questionDialog.SetActive(false);
        inputPanel.SetActive(false);
        errorDialog.SetActive(true);

        errorText.text = msg;
    }

    void ShowLoanApproved(float amount)
    {
        waitingForReturn = false;
        loanApprovedState = true;

        questionDialog.SetActive(false);
        inputPanel.SetActive(false);
        errorDialog.SetActive(true);
        enterBackPanel.SetActive(true);
        spaceLoginPanel.SetActive(true);

        errorText.text = "Loan for amount " + Math.Round(amount, 2) + " approved.\nPress ENTER to apply for another loan\nPress SPACE to return to Login.";
    }

    void ShowConfirmExit()
    {
        confirmExitState = true;
        confirmExitPanel.SetActive(true);
    }

    void Update()
    {
        if (!loanPanel.activeSelf)
            return;

        if (confirmExitState)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                confirmExitPanel.SetActive(false);
                confirmExitState = false;
                loanApprovedState = true;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (SessionManager.Instance != null)
                {
                    SessionManager.Instance.EndSession();
                }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                SceneManager.LoadScene("Login");
            }
            return;
        }

        if (loanApprovedState)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ShowQuestion();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowConfirmExit();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (waitingForReturn)
                ShowQuestion();
            else
                ValidateAmount();
        }
    }

    void ValidateAmount()
    {
        if (!float.TryParse(amountInput.text, out float amount))
        {
            ShowError("That is not a valid number. (Press Enter to continue.)");
            return;
        }

        if (amount <= 0)
        {
            ShowError("The amount must be greater than zero. (Press Enter to continue.)");
            return;
        }

        if (SessionManager.Instance == null)
        {
            ShowError("Session error. Please log in again.");
            return;
        }

        float income = SessionManager.Instance.CurrentIncome;

        if (income <= 0)
        {
            ShowError("We're sorry, but you don't have creditworthiness. Come back when your earnings are higher");
            ShowConfirmExit();
            return;
        }

        float maxLoan = CalculateMaxLoan(income);

        if (amount > maxLoan)
        {
            ShowError("You do not have sufficient credit capacity.\n" + "Maximum available loan: " + Math.Round(maxLoan, 2) + "\n(Press Enter to continue.)");
            return;
        }

        ShowLoanApproved(amount);
    }
}