using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RegularExpresionDemo_53;

class Program
{
    static void Main()
    {
        string folderPath = AppContext.BaseDirectory.Replace("\\bin\\Debug\\net10.0\\", "") + "\\Data";

        // Read all .log files from the folder
        string[] files = Directory.GetFiles(folderPath, "*.txt");

        // Output file
        string outputFile = Path.Combine(folderPath, "UnknownKeyword_Errors.txt");

        // Regex:
        // :\s+                  -> Colon followed by whitespace
        // CREATE_PROCEDURE_     -> Procedure name
        // [^\r\n]+              -> Rest of filename on same line
        // [\s\S]*?              -> Capture everything, non-greedy
        // Mlogica\.             -> Stop at "Mlogica."
        string pattern =@":\s+CREATE_PROCEDURE_[^\r\n]+[\s\S]*?Mlogica\.";

        Regex regex = new Regex(
            pattern,
            RegexOptions.Multiline | RegexOptions.IgnoreCase
        );

        foreach (string file in files)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("FILE: " + file);
            Console.WriteLine("========================================");

            string text = File.ReadAllText(file);

            MatchCollection matches = regex.Matches(text);

            Console.WriteLine("Matches Found: " + matches.Count);

            int count = 1;


            StringBuilder result = new StringBuilder();

            foreach (Match match in matches)
            {
                if (match.Value.Contains("Unknown keyword"))
                {
                    result.AppendLine($"----- Match {count} -----");
                    result.AppendLine(match.Value);
                    result.AppendLine();
                    result.AppendLine(new string('-', 50));
                    result.AppendLine();

                    count++;
                }
            }

            using (StreamWriter writer = new StreamWriter(outputFile, false))
            {
                writer.Write(result.ToString());
            }
        }
    }
}

// ============================================================
// REGULAR EXPRESSION (REGEX) QUICK REFERENCE
// ============================================================
//
// BASIC CHARACTER MATCHING
// ------------------------------------------------------------
//
// .       -> Any character except newline
// \.      -> Literal dot "."
// \       -> Escape character / special character prefix
//
// \d      -> Digit: 0-9
// \D      -> Non-digit
//
// \w      -> Word character: A-Z, a-z, 0-9, underscore (_)
// \W      -> Non-word character
//
// \s      -> Whitespace: space, tab, newline, etc.
// \S      -> Non-whitespace
//
//
// WHITESPACE
// ------------------------------------------------------------
//
// \s      -> Any whitespace character
// \s+     -> One or more whitespace characters
// \s*     -> Zero or more whitespace characters
// \s?     -> Zero or one whitespace character
//
// \t      -> Tab
// \r      -> Carriage return
// \n      -> Newline
//
//
// CHARACTER CLASSES
// ------------------------------------------------------------
//
// [abc]       -> a OR b OR c
// [a-z]       -> Lowercase letter
// [A-Z]       -> Uppercase letter
// [0-9]       -> Digit
// [a-zA-Z]    -> Uppercase OR lowercase letter
// [a-zA-Z0-9] -> Letter OR digit
//
// [^abc]      -> NOT a, b, or c
// [^0-9]      -> Anything except a digit
// [^\r\n]     -> Anything except CR or LF
//
//
// QUANTIFIERS
// ------------------------------------------------------------
//
// *       -> Zero or more
// +       -> One or more
// ?       -> Zero or one
// {n}     -> Exactly n times
// {n,}    -> At least n times
// {n,m}   -> Between n and m times
//
// Examples:
//
// \d+         -> One or more digits
// \d{4}       -> Exactly 4 digits
// \d{2,4}     -> 2 to 4 digits
// \s+         -> One or more spaces/whitespace
// [A-Z]+      -> One or more uppercase letters
//
//
// ANCHORS
// ------------------------------------------------------------
//
// ^       -> Start of string/line
// $       -> End of string/line
// \A      -> Start of entire string
// \z      -> End of entire string
// \Z      -> End of string before final \n
// \b      -> Word boundary
// \B      -> NOT a word boundary
//
// Examples:
//
// ^Hello       -> Starts with "Hello"
// World$       -> Ends with "World"
// \bcat\b      -> Matches whole word "cat"
// \d+$         -> Digits at the end
//
//
// GROUPS
// ------------------------------------------------------------
//
// (...)       -> Capturing group
// (?<name>...) -> Named capturing group
//
// Example:
//
// (\d{4})-(\d{2})-(\d{2})
//
// Captures:
//
// Group 1 -> Year
// Group 2 -> Month
// Group 3 -> Day
//
//
// NON-CAPTURING GROUP
// ------------------------------------------------------------
//
// (?:...)     -> Group without capturing
//
// Example:
//
// (?:Mr|Mrs|Ms)\s+\w+
//
// Matches:
//
// Mr Ali
// Mrs Sara
// Ms Ayesha
//
//
// ALTERNATION
// ------------------------------------------------------------
//
// |       -> OR
//
// Example:
//
// cat|dog
//
// Matches:
//
// cat
// dog
//
//
// LOOKAROUND
// ------------------------------------------------------------
//
// (?=...)     -> Positive lookahead
// (?!...)     -> Negative lookahead
// (?<=...)    -> Positive lookbehind
// (?<!...)    -> Negative lookbehind
//
// NOTE:
// Lookaround checks text without consuming it.
//
// ============================================================
// ESCAPING SPECIAL CHARACTERS
// ============================================================
//
// The following characters have special meanings in Regex:
//
// .  ^  $  *  +  ?  {  }  [  ]  \  |  (  )
//
// To match them literally, escape them with:
//
// \.   -> dot
// \+   -> plus
// \*   -> asterisk
// \?   -> question mark
// \(   -> opening parenthesis
// \)   -> closing parenthesis
// \[   -> opening bracket
// \]   -> closing bracket
// \\   -> backslash
//
//
// ============================================================
// GREEDY vs NON-GREEDY
// ============================================================
//
// *       -> Greedy: as much as possible
// *?      -> Non-greedy: as little as possible
//
// +       -> Greedy
// +?      -> Non-greedy
//
// {n,m}   -> Greedy
// {n,m}?  -> Non-greedy
//
//
// Example:
//
// [\s\S]*
//
// Means:
//
//     Match everything including newlines.
//
// [\s\S]*?
//
// Means:
//
//     Match everything including newlines,
//     but stop at the earliest possible match.
//
//
// ============================================================
// YOUR REGEX
// ============================================================
//
// string pattern =
//     @":\s+CREATE_PROCEDURE_[^\r\n]+[\s\S]*?Mlogica\.";
//
// Breakdown:
//
// :
//     -> Literal colon
//
// \s+
//     -> One or more whitespace characters
//
// CREATE_PROCEDURE_
//     -> Exact text
//
// [^\r\n]+
//     -> One or more characters that are NOT
//        carriage return (\r) or newline (\n)
//
// [\s\S]
//     -> Either whitespace OR non-whitespace
//     -> Effectively ANY character, including newline
//
// *?
//     -> Zero or more characters, non-greedy
//     -> Stops at the first possible match
//
// Mlogica
//     -> Exact text "Mlogica"
//
// \.
//     -> Literal dot
//
//
// ============================================================
// COMMON REGEX EXAMPLES
// ============================================================
//
// Email-like pattern:
//
// [\w.+-]+@[\w.-]+\.[A-Za-z]{2,}
//
// Phone number:
//
// \d{3}-\d{3}-\d{4}
//
// Number:
//
// \d+
//
// Decimal number:
//
// \d+\.\d+
//
// Date:
//
// \d{4}-\d{2}-\d{2}
//
// SQL identifier:
//
// [A-Za-z_][A-Za-z0-9_]*
//
// Multiple spaces:
//
// \s+
//
// Text until newline:
//
// [^\r\n]+
//
// Anything including newline:
//
// [\s\S]+
//
// Anything until a specific word:
//
// [\s\S]*?Mlogica