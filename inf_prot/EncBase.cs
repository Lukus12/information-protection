using System.Security.Cryptography;
using System.Text.Json;

namespace inf_prot
{
    /// <summary>
    /// Механизм шифрования сообщений
    /// </summary>
    internal class EncBase
    {
        // Алфавит шифруемых сообщений
        protected HashSet<char> alphabet { get; set; }

        // Длина блока открытого текста и блока шифрограммы
        protected int combinationLength { get; set; }

        // JSON ключ
        protected string key { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="alph">Алфавит шифруемых сообщений</param>
        /// <param name="alphLength">Длина блока открытого текста и блока шифрограммы</param>
        protected EncBase(string alph, int alphLength)
        {
            alphabet = [.. alph];
            combinationLength = alphLength;
        }

        /// <summary>
        /// Перемешивание комбинаций
        /// </summary>
        /// <param name="values">Лист комбинаций</param>
        protected void Shuffle<T>(List<T> values)
        {
            int n = values.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int j = RandomNumberGenerator.GetInt32(i, n);

                if (i != j)
                {
                    var temp = values[i];
                    values[i] = values[j];
                    values[j] = temp;
                }
            }
        }

        /// <summary>
        /// Получить ключ в формате JSON
        /// </summary>
        /// <param name="jsonDict"></param>
        /// <returns>JSON ключ</returns>
        protected string SerializeKey<K, V>(Dictionary<K, V> jsonDict)
        {
            return JsonSerializer.Serialize(jsonDict, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}
