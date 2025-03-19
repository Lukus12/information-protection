using System.Text;
using System.Text.Json;

namespace inf_prot
{
    /// <summary>
    /// Механизм шифрования сообщений перестановками
    /// </summary>
    internal class EncBaseLab2 : EncBase, IEncBaseLab
    {
        // Шифрование перестановками
        private List<int> permutationEncryption;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="alph">Алфавит шифруемых сообщений</param>
        /// <param name="alphLength">Длина блока открытого текста и блока шифрограммы</param>
        public EncBaseLab2(string alph, int alphLength) : base(alph, alphLength)
        {
            permutationEncryption = new List<int>();

            GenerateKey();
        }

        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        public string Encrypt(string msg)
        {
            return EncDecProcess(msg, true);
        }

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Рашифрованное сообщение</param>
        /// <returns></returns>
        public string Decrypt(string enc)
        {
            return EncDecProcess(enc, false);
        }

        /// <summary>
        /// Проверяет присутсвие символа в алфавите шифруемых сообщений
        /// </summary>
        /// <param name="ch">Символ для проверки</param>
        /// <returns>true - есть в алфавите, false - нет</returns>
        public bool IsCharInDict(char ch)
        {
            if (ch == ' ' || alphabet.Contains(ch))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Валидатор для JSON редактора
        /// </summary>
        /// <param name="ch">Введенный символ</param>
        /// <returns></returns>
        public bool JsonEditorValidater(char ch)
        {
            if (int.TryParse(ch.ToString(), out int number))
            {
                if (number <= combinationLength || number >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Генерация алфавита
        /// </summary>
        /// <returns>Словарь перестановок</returns>
        private List<int> GenerateAlphabet()
        {
            var permEnc = new List<int>();
            for (var i = 0; i < combinationLength; i++)
            {
                permEnc.Add(i);
            }

            Shuffle(permEnc);

            return permEnc;
        }

        /// <summary>
        /// Алгоритм шифрования и расшифрования сообщений
        /// </summary>
        /// <param name="msg">Введенное сообщение</param>
        /// <param name="isEncrypt">true - шифрование, false - расшифрование</param>
        /// <returns></returns>
        private string EncDecProcess(string msg, bool isEncrypt)
        {
            var resultStr = new StringBuilder(msg.Length);
            var firstSymb = 0;
            var isProcessEnd = false;

            while (!isProcessEnd)
            {
                // Формирование новой строки
                var newSubStr = new StringBuilder(combinationLength);

                for (int i = 0; i < combinationLength; i++)
                {
                    // Шифрование / дешифрование
                    var symbPos = isEncrypt ?
                        permutationEncryption[i] :
                        permutationEncryption.FindIndex(value => value == firstSymb + i);

                    if (firstSymb + symbPos >= msg.Length)
                    {
                        newSubStr.Append(' ');
                        isProcessEnd = true;
                    }
                    else
                        newSubStr.Append(msg[firstSymb + symbPos]);
                }

                resultStr.Append(newSubStr);

                firstSymb = firstSymb + combinationLength;
                if (firstSymb == msg.Length)
                    isProcessEnd = true;
            }

            return resultStr.ToString();
        }

        /// <summary>
        /// Создание ключа
        /// </summary>
        /// <returns>JSON ключ</returns>
        private string CreateKey()
        {
            // Создание словаря: текущая позиция - позиция символа, который перейдет в текущую позицию
            var permutation = new Dictionary<int, int>();
            for (var i = 0; i < permutationEncryption.Count; i++)
            {
                permutation.Add(i + 1, permutationEncryption[i]);
            }

            return SerializeKey(permutation);
        }

        /// <summary>
        /// Генерация алфавита и ключа
        /// </summary>
        public void GenerateKey()
        {
            permutationEncryption = GenerateAlphabet();

            key = CreateKey();
        }

        /// <summary>
        /// Вернуть представление ключа в виде строки
        /// </summary>
        /// <returns>JSON ключ в виде строки</returns>
        public string GetKey()
        {
            return key;
        }

        /// <summary>
        /// Задать ключ из json
        /// </summary>
        /// <param name="jsonKey">Ключ в формате JSON</param>
        public void SetKey(string jsonKey)
        {
            var deserializedKey = JsonSerializer.Deserialize<Dictionary<int, int>>(jsonKey);

            if (deserializedKey != null)
            {
                combinationLength = deserializedKey.Count;
                for (var i = 0; i < combinationLength; i++)
                {
                    permutationEncryption[i] = deserializedKey[i];
                }

                key = jsonKey;
            }
            else
                throw new Exception();
        }
    }
}
