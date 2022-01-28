using System;

namespace Dell.Lead.WeApi.Util
{
    public class ValidateCpf : IValidateCpf
    {
        public bool IsCpf(long cpf)
        {
            string isCpf = Convert.ToString(cpf);

            int[] multiplierFirstDigit = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplierSecondDigit = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;

            if (isCpf.Length != 11)
            {
                return false;
            }

            tempCpf = isCpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplierFirstDigit[i];
            }

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplierSecondDigit[i];
            }

            rest = sum % 11;

            if (rest < 2)
            {
                rest = 0;
            }
            else
            {
                rest = 11 - rest;
            }

            digit = digit + rest.ToString();

            return isCpf.EndsWith(digit);
        }
    }
}
