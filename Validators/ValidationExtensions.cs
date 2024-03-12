using FluentValidation;
using System.Text.RegularExpressions;

namespace WTS.Validators
{
    /// <summary>
    /// Extends validation rules and creates standard rules for string, int and double
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Creates standard rule for input strings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> MustBeValidString<T>(this IRuleBuilder<T, string> ruleBuilder, Func<T, bool> isRequired)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("This field is required.").When(isRequired)
                .MaximumLength(100).WithMessage("Must be less than 100 characters.")
                .Must(BeFreeOfMaliciousContent).WithMessage("Invalid content detected.");
        }

        /// <summary>
        /// Creates standard rule for nullable input strings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string?> MustBeValidNullableString<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, bool> isRequired)
        {
            return ruleBuilder
                .MaximumLength(100).WithMessage("Must be less than 100 characters.")
                .Must(BeFreeOfMaliciousContent).WithMessage("Invalid content detected.");
        }

        /// <summary>
        /// Creates standard rule for input integers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, int> MustBeValidInteger<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, bool> isRequired)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("This field is required.").When(isRequired)
                .InclusiveBetween(0, int.MaxValue).WithMessage("Must be a positive number.");
        }

        /// <summary>
        /// Creates standard rule for nullable input integers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, int?> MustBeValidNullableInteger<T>(this IRuleBuilder<T, int?> ruleBuilder, Func<T, bool> isRequired)
        {
            return ruleBuilder
                .Must(value => !value.HasValue || value.Value >= 0).WithMessage("Must be a positive number.");
        }

        /// <summary>
        /// Creates standard rule for input doubles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, double> MustBeValidDouble<T>(this IRuleBuilder<T, double> ruleBuilder, Func<T, bool> isRequired)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("This field is required.").When(isRequired)
                .InclusiveBetween(0, double.MaxValue).WithMessage("Must be a positive number.");
        }

        /// <summary>
        /// Creates standard rule for nullable input doubles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, double?> MustBeValidNullableDouble<T>(this IRuleBuilder<T, double?> ruleBuilder, Func<T, bool> isRequired)
        {
            return ruleBuilder
                .Must(value => !value.HasValue || value.Value >= 0.0).WithMessage("Must be a positive number.");
        }

        /// <summary>
        /// Checks input string against regex pattern
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool BeFreeOfMaliciousContent(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            string regexPattern = GetMaliciousPatternsRegex();

            return !Regex.IsMatch(input, regexPattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Escape each pattern and join into a regex
        /// </summary>
        /// <returns></returns>
        private static string GetMaliciousPatternsRegex()
        {
            string[] maliciousPatterns = MaliciousArrayOFStrings();

            return string.Join("|", maliciousPatterns.Select(Regex.Escape));
        }

        /// <summary>
        /// Array of commonly used injections and inputs
        /// </summary>
        /// <returns></returns>
        public static string[] MaliciousArrayOFStrings()
        {
            return [
                // SQL Injection
                "select", "insert", "update", "delete", "drop", "union", "execute", "exec", "xp_", "sp_", "declare", "fetch", "--", "/*", "*/", ";--", ";",
    
                // Cross-Site Scripting (XSS) 
                "<script>", "</script>", "javascript:", "onerror", "onload", "alert(", "<iframe", "<frame", "srcdoc=", "onmouseover", "onmousemove", "onmousedown", "onmouseup", "ondblclick", "onclick", "onchange", "onsubmit", "onreset", "onselect", "onblur", "onfocus", "onkeydown", "onkeypress", "onkeyup", "onmouseenter", "onmouseleave", "onmouseover", "onmouseout",
    
                // General HTML for possible XSS
                "<img", "<div", "<span", "<body", "<head", "<html", "<style", "<link", "<meta", "<object", "<embed", "<applet", "<form", "<input", "<button",
    
                // Directory traversal
                "../", "..\\", "%2e%2e/", "%2e%2e\\",
    
                // Command injection
                "|", "&", "$", "`", "cmd", "passthru", "system", "shell_exec",

                // Other potentially dangerous inputs
                "<!--", "-->", "<?", "?>", "<%", "%>", "eval(", "expression(", "xmlns:"
            ];
        }
    }
}