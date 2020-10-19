// Шифр Скитала – шифрование текста при помощи деревянного цилиндра и пергамента, также известен как шифр Древней Спарты. 
// Этот метод шифрования использовался античными спартанцами и греками, для обмена сообщениями во время войны.

// Описание алгоритма
// Для шифрования текста используется цилиндр фиксированного диаметра, на который наматывается узкая полоска пергамента. 
// Сообщение записывают вдоль цилиндра, а затем разматывают, в итоге получается зашифрованное сообщение, которое можно расшифровать применяя цилиндр того же диаметра. 
// При этом диаметр цилиндра выступает в роле ключа шифрования.
// Шифрование сообщения с ключем 4, можно представить в виде таблицы, где открытый текст записывается в строки, а разматывание полоски – это склейка всех столбцов в один.
using System;

public class ScytaleCipher
{
	public string Encrypt (string text, int d) 
	{
		var k = text.Length % d;
		if (k > 0)
		{
			text += new string(' ', d - k);
		}

		var column = text.Length / d;
		var result = "";

		for (int i = 0; i < column; i++)
		{
			for (int j = 0; j < d; j++)
			{
				result += text[i + column * j].ToString();
			}
		}
		return result;
	}

	public string Decrypt (string text, int d)
	{
		var column = text.Length / d;
		var symbols = new char[text.Length];
		int index = 0;

		for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < d; j++)
            {
                symbols[i + column * j] = text[index];
                index++;
            }
        }
        return string.Join("", symbols);
	}
}

class Program
{
    static void Main(string[] args)
    {
    	ScytaleCipher scytale = new ScytaleCipher();
    	string message = "шифрование текста при помощи деревянного цилиндра ";
    	int diameter = 7;
    	var encText = scytale.Encrypt(message, diameter);
    	Console.WriteLine("Зашифрованный текст: {0}", encText);
        Console.WriteLine("Расшифрованный текст: {0}", scytale.Decrypt(encText, diameter));
        Console.ReadLine();
    }
}
