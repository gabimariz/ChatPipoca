using System.Text.RegularExpressions;

namespace Domain.Utils;

public static class Pass
{
	// Account activation key setup
	public static string Generate(string pass)
	{
		var regex = new Regex(@"[//\~^^%.;_-]");

		pass = pass.Replace("$2a$11$", "");
		pass = regex.Replace(pass, "");

		return pass;
	}
}
