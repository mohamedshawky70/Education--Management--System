namespace School.Application.Const
{
	public class RegexPattern
	{
		public const string EnglishLetter = "^[A-Za-z]+$";
		public const string StrongPassword = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";

	}
}
