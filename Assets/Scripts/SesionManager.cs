using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;

    public UserData LoggedUser { get; private set; }
    public string CurrentLogin { get; private set; }
    public float CurrentIncome { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUser(UserData user)
    {
        LoggedUser = user;
        CurrentLogin = user.login;
        CurrentIncome = user.income;
    }

    public void EndSession()
    {
        LoggedUser = null;
        CurrentLogin = null;
        CurrentIncome = 0f;

        Instance = null;
        Destroy(gameObject);
    }
}