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
        statusText.text = " 회원 가입중 ...";
        yield return StartCoroutine(authManager.Register(usernameInput.text, passwordInput.text));
        statusText.text = "회원 가입 성공 , 로그인 해주세여";
    }

    private IEnumerator LoginCoroutine()
    {
        statusText.text = " 로그인중 ...";
        yield return StartCoroutine(authManager.Register(usernameInput.text, passwordInput.text));
        statusText.text = "로그인 성공";
    }
}
