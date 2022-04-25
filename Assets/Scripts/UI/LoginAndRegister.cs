using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginAndRegister : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    public InputField PasswordCheckInput;
    [SerializeField]
    private Firebase _db;
    public void Register()
    {
        if (PasswordInput.text.Length < 6)
        {
            Debug.Log("Password should contain atleast 6 characters");
            return;
        }
        if (!(PasswordInput.text == PasswordCheckInput.text))
        {
            Debug.Log("Passwords doesn't match Mokebe :(");
            return;
        }

        _db.SignUpUser(EmailInput.text, EmailInput.text, PasswordInput.text);
    }
    public void Login()
    {
        _db.SignInUser(EmailInput.text, PasswordInput.text);
    }
}
