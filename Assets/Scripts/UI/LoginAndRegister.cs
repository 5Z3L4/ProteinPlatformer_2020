using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginAndRegister : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    public InputField PasswordCheckInput;
    public TMP_Text errorText;
    public Animator loadingAnim;
    [SerializeField]
    private Firebase _db;
    public void Register()
    {
        if (PasswordInput.textComponent.text.Length < 6)
        {
            Debug.Log("Password should contain atleast 6 characters");
            errorText.SetText("Password should contain atleast 6 characters");
            return;
        }
        if (!(PasswordInput.text == PasswordCheckInput.text))
        {
            Debug.Log("Passwords doesn't match Mokebe :(");
            errorText.SetText("Passwords don't match");
            return;
        }
        StartCoroutine(StartLoading());
        _db.SignUpUser(EmailInput.text, EmailInput.text, PasswordInput.text);
    }
    public void Login()
    {
        _db.SignInUser(EmailInput.text, PasswordInput.text);
    }
    private IEnumerator StartLoading()
    {
        loadingAnim.SetBool("IsLoading", true);
        yield return new WaitForSeconds(3f);
        loadingAnim.SetBool("IsLoading", false);
        errorText.SetText("<color=#00ff44>Account has been created successfully</color>");
    }
}
