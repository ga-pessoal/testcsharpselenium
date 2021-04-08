using System;
using System.Linq;

namespace opentech.automacao.core
{
    public class Geradores
    {
        private static Random random = new Random();

        public string NumeroRandomico(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
        public string LetraRandomica(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        public string PlacaRandomica()
        {
            string retorno = LetraRandomica(3) + NumeroRandomico(4);
            return retorno;
        }

        public string GeradorCPF()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }
    }
}
