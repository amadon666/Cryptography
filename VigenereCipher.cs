// Шифр Виженера – алгоритм шифрования текстовых данных с помощью ключевого слова.
// Описание алгоритма:
// Шифрование Виженера можно представить как несколько шифров Цезаря с различными ключами. Проще всего шифры представить в виде таблицы, для английского алфавита мы получим 26 строк шифра Цезаря, в каждой строке сдвиг на единицу больше предыдущей.

// Математически шифр Виженера можно описать следующими формулами:
//  Encrypt(mn) = (Q + mn + kn) % Q;
//  Decrypt(cn) = (Q + cn - kn) % Q.
// где mn - позиция символа открытого текста, kn - позиция символа ключа шифрования, Q - количество символов в алфавите, cn - позиция символа зашифрованного текста.
using System;

public class VigenereCipher
{
	const string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	readonly string letters;

	public VigenereCipher (string alphabet = null) 
	{
		letters = String.IsNullOrEmpty(alphabet)? defaultAlphabet : alphabet;
	}

	private string GetRepeatKey (string s, int n) 
	{
		var p = s;
		while (p.Length < n)
		{	
			p += p;
		}
		return p.Substring(0, n);
	}

	private string Vigenere (string text, string password, bool encrypting = true)
	{
		var gamma = GetRepeatKey(password, text.Length);
		var retValue = "";
		var q = letters.Length;

		for (int i = 0; i < text.Length; i++)
		{
			var letterIndex = letters.IndexOf(text[i]);
			var codeIndex = letters.IndexOf(gamma[i]);
			if (letterIndex < 0)
			{
				retValue += text[i].ToString();
			} 
			else 
			{
				retValue += letters[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
			}
		}
		return retValue;
	}

	public string Encrypt (string plainMessage, string password) => Vigenere(plainMessage, password);
	public string Decrypt (string encryptedMessage, string password) => Vigenere(encryptedMessage, password, false);
}

class Program
{
    static void Main(string[] args)
    {
        //передаем в конструктор класса буквы русского алфавита
        var cipher = new VigenereCipher("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ");
        var inputText = "Шифрование".ToUpper();
        var password = "ключ".ToUpper();
        var encryptedText = cipher.Encrypt(inputText, password);
        Console.WriteLine("Зашифрованное сообщение: {0}", encryptedText);
        Console.WriteLine("Расшифрованное сообщение: {0}", cipher.Decrypt(encryptedText, password));
        Console.ReadLine();
    }
}
