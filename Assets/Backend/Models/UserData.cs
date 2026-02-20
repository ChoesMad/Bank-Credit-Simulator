[System.Serializable]
public class UserData
{
    public string login;
    public string password;
    public int income;
}

[System.Serializable]
public class UserDatabase
{
    public UserData[] users;
}