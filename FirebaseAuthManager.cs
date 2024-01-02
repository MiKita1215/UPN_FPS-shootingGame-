using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUp(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignUp was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignUp encountered an error: " + task.Exception);
                return;
            }

            user = task.Result;
            Debug.LogFormat("User signed up successfully: {0} ({1})", user.DisplayName, user.UserId);
        });
    }

    public void SignIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignIn was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignIn encountered an error: " + task.Exception);
                return;
            }

            user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
        });
    }

    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out.");
    }
}
