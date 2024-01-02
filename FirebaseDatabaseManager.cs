using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void WriteNewUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);

        databaseReference.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("User data written successfully.");
            }
            else
            {
                Debug.LogError("Failed to write user data: " + task.Exception);
            }
        });
    }

    public void ReadUser(string userId)
    {
        databaseReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, object> userData = snapshot.Value as Dictionary<string, object>;

                Debug.Log("User data read successfully: " + userData["name"] + ", " + userData["email"]);
            }
            else
            {
                Debug.LogError("Failed to read user data: " + task.Exception);
            }
        });
    }
}

[System.Serializable]
public class User
{
    public string username;
    public string email;

    public User() { }

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}
