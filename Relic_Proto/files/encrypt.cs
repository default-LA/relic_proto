using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class locker
{
    private String username;
    private String password;
    private const int salt = 1337;

    public locker(String username, String password)
    {
        this.password = password;
        this.username = username;
    }

    //Turn these into one function!
    public string encrypt(String text, bool mode)
    {
        string cyherText = "";
        int lenght;
        string fibStr;
        int i = 0;
        lenght = text.Length;
        fibStr = fibonacci(lenght);
        while (i < lenght)
        {
            if (mode)
            {
                cyherText = cyherText + Convert.ToChar(value(text[i]) + value(fibStr[i]) + salt);
            }
            else
            {
                cyherText = cyherText + Convert.ToChar(value(text[i]) - value(fibStr[i]) - salt);
            }
            i++;
        }
        return cyherText;
        
    }

    private long sum(String str)
    {
        long total = 0;
        foreach (char chr in str)
        {
            total = total + value(chr);
        }
        return total;
    }

    private long value(Char chr)
    {
        return Convert.ToInt32(chr);
    }

    private string fibonacci(int lenght)
    {
        String fibStr = "";
        long temp;
        long current;
        long previous;
        current = sum(password);
        previous = sum(username);
        while (fibStr.Length < lenght)
        {
            temp = current;
            current = current + previous;
            fibStr = current.ToString() + fibStr;
            previous = temp;
        }
        return fibStr;
    }

}
