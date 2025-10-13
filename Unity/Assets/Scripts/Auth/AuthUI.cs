using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthUI : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public Button registerButton;
    public Button loginButton;

    public Text statusText;

    private AuthManager authManager;

     void Start()
    {
        authManager = GetComponent<AuthManager>();
        registerButton.onClick.AddListener(OnRegisterClick);
        loginButton.onClick.AddListener(OnlLoginClick);
    }

    private void OnlLoginClick()
    {
        StartCoroutine(RegisterCoroutine());
    }
    private void OnRegisterClick()
    {
        StartCoroutine(RegisterCoroutine());
    }

    private IEnumerator RegisterCoroutine()
    {
        statusText.text = " ȸ�� ������ ...";
        yield return StartCoroutine(authManager.Register(usernameInput.text, passwordInput.text));
        statusText.text = "ȸ�� ���� ���� , �α��� ���ּ���";
    }

    private IEnumerator LoginCoroutine()
    {
        statusText.text = " �α����� ...";
        yield return StartCoroutine(authManager.Register(usernameInput.text, passwordInput.text));
        statusText.text = "�α��� ����";
    }
}
