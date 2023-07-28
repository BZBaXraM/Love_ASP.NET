namespace CoR_Middleware;

class CheckerDirector
{
    public ICheckBuilder? Builder { get; set; }

    public bool MakeHumanChecker(Human human)
    {
        UserNameChecker userNameChecker = new();
        PasswordChecker passwordChecker = new();
        EmailChecker emailChecker = new();
        userNameChecker.Next = passwordChecker;
        passwordChecker.Next = emailChecker;

        return userNameChecker.Check(human);
    }
}