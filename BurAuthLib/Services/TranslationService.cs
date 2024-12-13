namespace BurAuthLib.Services;
public class TranslationService
{
    public string GetTranslation(string key)
    {
        return key switch
        {
            "LOGGING_IN_TEXT" => "Logging in...",
            "LOG_IN_FAILED_TEXT" => "Log in failed. Please try again.",
            "COMPLETING_LOGGING_IN_TEXT" => "Completing log in...",
            "LOG_OUT_TEXT" => "Logging out...",
            "LOG_OUT_FAILED_TEXT" => "Log out failed. Please try again.",
            "LOG_OUT_SUCCEEDED_TEXT" => "Log out succeeded.",
            "COMPLETING_LOG_OUT_TEXT" => "Completing log out...",
            _ => key,
        };
    }
}
