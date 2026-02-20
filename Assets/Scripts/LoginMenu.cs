using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LoginMenu : MonoBehaviour
{
    public TMP_InputField loginInput;
    public TMP_InputField passwordInput;
    public TMP_Text showPasswordButtonText;
    public GameObject ErrorPanel;
    public TMP_Text ErrorText; 
    private bool isPasswordVisible = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        passwordInput.contentType = TMP_InputField.ContentType.Password;
        showPasswordButtonText.text = "SHOW";
        passwordInput.ForceLabelUpdate();
    }

    public void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            showPasswordButtonText.text = "HIDE";
        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            showPasswordButtonText.text = "SHOW";
        }

        passwordInput.ForceLabelUpdate();
    }

    public void OnLoginClick()
    {
        string login = loginInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            ShowError("ENTER CORRECT LOGIN AND PASSWORD");
            return;
        }

        if (AuthService.TryLogin(login, password, out UserData user))
        {
            SessionManager.Instance.SetUser(user);
            SceneManager.LoadScene("Bank");
        }
        else
        {
            ShowError("THE USERNAME OR PASSWORD IS INCORRECT");
        }
    }

    void ShowError(string message)
    {
        StopAllCoroutines();
        ErrorPanel.SetActive(true);
        ErrorText.gameObject.SetActive(true);
        ErrorText.text = message;
        StartCoroutine(HideError());
    }

    IEnumerator HideError()
    {
        yield return new WaitForSeconds(3f);
        ErrorText.gameObject.SetActive(false);
        ErrorPanel.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("You left the apllication");
        Application.Quit();
    }
}