namespace CoR_Middleware;

class PasswordChecker : BaseChecker
{
    public override bool Check(object request)
    {
        if (request is Human human)
        {
            if (string.IsNullOrWhiteSpace(human.Password) && human.Password?.Length > 8)
                Next.Check(request);
        }

        return false;
    }
}