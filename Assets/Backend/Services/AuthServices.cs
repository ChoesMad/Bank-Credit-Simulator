using UnityEngine;
using System.Linq;

public static class AuthService
{
    public static bool TryLogin(string login, string password, out UserData user)
    {
        user = null;

        TextAsset jsonFile = Resources.Load<TextAsset>("user");

        if (jsonFile == null)
        {
            Debug.LogError("user.json not found");
            return false;
        }

        UserDatabase db = JsonUtility.FromJson<UserDatabase>(jsonFile.text);

        user = db.users.FirstOrDefault(u =>
            u.login == login && u.password == password
        );

        return user != null;
    }
}