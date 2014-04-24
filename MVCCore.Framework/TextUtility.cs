using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace MVCCore.Framework
{
    public class TextUtility
    {
        private static readonly string[] vietnameseSigns =
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };


        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                    str = str.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);

            }
            return str;
        }

        public static string MakeAlias(string str)
        {
            string removeSignString = RemoveSign4VietnameseString(str).Trim().ToLower();
            return Regex.Replace(removeSignString, "\\s+", "-").Replace(",", string.Empty).Replace("---", "-").Replace(".", string.Empty).Replace("\"", string.Empty).Replace("'", string.Empty).Replace("&", string.Empty);
        }


        public static string HtmlRemove(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        public static string FormHash(string original)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(original, "MD5");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textToHash"></param>
        /// <param name="hashName">MD5, SHA, SHA1, SHA256, SHA384, SHA512, ...
        /// for complete list, see http://msdn.microsoft.com/en-us/library/wet69s13.aspx
        /// </param>
        /// <returns></returns>
        public static string Hash(string textToHash, string hashName = "SHA256")
        {

            HashAlgorithm algorithm = HashAlgorithm.Create(hashName);

            if (algorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name", "hashName");
            }

            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(textToHash));
            return Convert.ToBase64String(hash);

        }

        public static string GetSequenceFromCode(string code)
        {
            return code.Split(new[] { '-' })[1];
        }

        public static string TrimWord(string text, int numberOfWords)
        {
            string[] words = text.Split(' ');
            string @return = string.Empty;
            if (words.Length <= numberOfWords)
            {
                @return = text;
            }
            else
            {
                for (int i = 0; i < numberOfWords; i++)
                {
                    @return += words.GetValue(i) + " ";
                }
            }
            return @return;
        }

        public static string TrimWord(string text, int numberOfWords, string rep)
        {
            string[] words = text.Split(' ');
            string @return = string.Empty;
            if (words.Length <= numberOfWords)
            {
                @return = text;
            }
            else
            {
                for (int i = 0; i < numberOfWords; i++)
                {
                    @return += words.GetValue(i) + " ";
                }
                @return += rep;
            }
            return @return;
        }

        public static string TrimString(string text, int numberOfLeters, string rep)
        {
            string @return;
            if (text.Length <= numberOfLeters)
            {
                @return = text;
            }
            else
            {
                @return = text.Substring(0, numberOfLeters);
                @return += rep;
            }
            return @return;
        }

        public static string TrimSpace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return Regex.Replace(text, "\\s", string.Empty);
        }


        public static string BreakString(string original, int position)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;

            if (position >= original.Length)
                return original;


            return original.Insert(position, " ");
        }

        public static int GetIdFromAlias(string alias)
        {
            return Int32.Parse(alias.Split(new[] { '-' }).Last());
        }


        public static string GetPointSign(int type)
        {
            switch (type)
            {
                case 2:
                    return "-";
                case 5:
                    return "-";
                case 9:
                    return "-";
                default:
                    return string.Empty;
            }
        }




        public static string RemakeTextFromTextArea(string text)
        {
            return Regex.Replace(text, "\r?\n", "<br/>");
        }

        public static string ClearUnicode(string input)
        {
            Regex aReg;
            // UPPER CASE            
            aReg = new Regex("(À|Á|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ầ|Ấ|Ẩ|Ẫ|Ậ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "A");

            aReg = new Regex("(Đ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "D");

            aReg = new Regex("(È|É|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "E");

            aReg = new Regex("(Ì|Í|Ỉ|Ĩ|Ị)", RegexOptions.Singleline);
            input = aReg.Replace(input, "I");

            aReg = new Regex("(Ò|Ó|Ỏ|Õ|Ọ|Ô|Ồ|Ố|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "O");

            aReg = new Regex("(Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự)", RegexOptions.Singleline);
            input = aReg.Replace(input, "U");

            aReg = new Regex("(Ý|Ỳ|Ỷ|Ỹ|Ỵ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "Y");

            // LOWER CASE
            aReg = new Regex("(á|à|ả|ã|ạ|â|ấ|ầ|ẩ|ẫ|ậ|ă|ắ|ằ|ẳ|ẵ|ặ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "a");

            aReg = new Regex("(đ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "d");

            aReg = new Regex("(é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "e");

            aReg = new Regex("(í|ì|ỉ|ĩ|ị)", RegexOptions.Singleline);
            input = aReg.Replace(input, "i");

            aReg = new Regex("(ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "o");

            aReg = new Regex("(ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự)", RegexOptions.Singleline);
            input = aReg.Replace(input, "u");

            aReg = new Regex("(ý|ỳ|ỷ|ỹ|ỵ)", RegexOptions.Singleline);
            input = aReg.Replace(input, "y");

            return input;
        }

        public static string HMACSHA1Encode(string input)
        {
            SHA1 hash = SHA1.Create();
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);
            string localChecksum = BitConverter.ToString(hashBytes)
            .Replace("-", "").ToLowerInvariant();

            return localChecksum;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}