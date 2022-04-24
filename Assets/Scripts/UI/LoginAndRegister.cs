using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginAndRegister : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    [SerializeField]
    private Firebase _db;
    public void Register()
    {
        _db.SignUpUser(EmailInput.text, EmailInput.text, PasswordInput.text);
    }
    public void Login()
    {
        _db.SignInUser(EmailInput.text, PasswordInput.text);
    }
}
