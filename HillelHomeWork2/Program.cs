using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace HillelHomeWork2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1");
            int[] intarr = new int[] { 1, 2, 3, 4, 5, 9, 10 };
            Console.Write("Input arr 1: ");
            foreach (int i in intarr)
                Console.Write(i + " ");
            string str = "ithillel";
            Console.WriteLine($"\nInput arr 2: {str}");
            char[] chars = str.ToCharArray();
            Task1(intarr);
            Task1(chars);
            str = new string(chars);
            Console.Write("Output arr 1: ");
            foreach (int i in intarr)
                Console.Write(i + " ");
            Console.WriteLine($"\nOutput arr 2: {str}");

            Console.WriteLine("\nTask 2");
            List<string> badWords = new List<string> { "xd", "lol", "noob" };
            string input = "I want to say xd, not xdg and not xdlol, i want to say noob,xd/lol and its all for task 2 and changes for xd git";

            string filtered = Task2(input, badWords);
            Console.WriteLine(filtered);

            Console.WriteLine("\nTask 3");
            Console.WriteLine("Enter a count of chars");
            int count = int.Parse(Console.ReadLine());
            Console.WriteLine(Task3(count));

            Console.WriteLine("\nTask 4");
            int[] ints = { 0, 1, 2, 3, 4, 5, 6, 8, 9 };
            Console.Write("Output arr: ");
            foreach (int i in ints)
                Console.Write(i + " ");
            Console.WriteLine($"\nMissing int {Task4(ints)}");

            Console.WriteLine("\nTask 5");
            string str3 = "AAACGGTTTT";
            Console.WriteLine($"Input string: {str3}");
            string str4 = Task5Compr(str3);
            Console.WriteLine($"Compressed string: {str4}");
            string str5 = Task5DeCompr(str4);
            Console.WriteLine($"Decompressed string: {str5}");

            Console.WriteLine("\nTask 6");
            string strcode = "ItsTask6AndMyPassIs:123321";
            byte[] key = new byte[16];
            byte[] iv = new byte[16];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(key);
                random.GetBytes(iv);
            }
            byte[] encryptedPass = Task6Crypt(strcode, key, iv);
            string encryptedPassString = Convert.ToBase64String(encryptedPass);
            string decryptedPassString = Task6Decrypt(encryptedPass, key, iv);
            Console.WriteLine($"Original: {strcode}");
            Console.WriteLine($"Encrypted: {encryptedPassString}");
            Console.WriteLine($"Decrypted: {decryptedPassString}");
            Console.ReadLine();
        }
        public static void Task1<T>(T[] arr)
        {
            for (int i = 0, j = arr.Length; i < arr.Length / 2 && j > 0; i++)
            {
                T ch = arr[i];
                arr[i] = arr[--j];
                arr[j] = ch;
            }
        }
        public static string Task2(string input, List<string> badWords)
        {
            string pattern = @"\b(" + string.Join("|", badWords) + @")\b";
            return Regex.Replace(input, pattern, m => new string('*', m.Length), RegexOptions.IgnoreCase);
        }
        public static string Task3(int countchars)
        {
            char[] chars = new char[countchars];
            var rand = new Random();
            for (int i = 0; i < countchars; i++)
            {
                int randch = rand.Next(32, 126);
                chars[i] = (char)randch;
            }
            return new string(chars);
        }
        public static int Task4(int[] arr)
        {
            int expectedSum = arr.Length * (arr.Length + 1) / 2;
            int actualSum = 0;
            //actualSum = arr.Sum(x => x);
            foreach (int num in arr)
                actualSum += num;

            return expectedSum - actualSum;
        }
        public static string Task5Compr(string dnk)
        {
            char[] arr = dnk.ToCharArray();
            char current = arr[0];
            int count = 1;
            StringBuilder newstr = new StringBuilder();
            for (int i = 1; i < arr.Length; i++)
            {
                if (current == arr[i])
                    count++;
                else
                {
                    newstr.Append(current + count.ToString());
                    current = arr[i];
                    count = 1;
                }
            }
            newstr.Append(current + count.ToString());
            return newstr.ToString();
        }
        public static string Task5DeCompr(string dnk)
        {
            char[] arr = dnk.ToCharArray();
            StringBuilder newstr = new StringBuilder();
            for (int i = 0, j = 1; i < arr.Length; i++, i++, j++, j++)
            {
                for (int l = 0; l < int.Parse(arr[j].ToString()); l++)
                {
                    newstr.Append(arr[i]);
                }
            }
            return newstr.ToString();
        }
        static byte[] Task6Crypt(string plainText, byte[] key, byte[] iv)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        static string Task6Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            string plaintext;
            using (Aes aesAlg = Aes.Create())
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
