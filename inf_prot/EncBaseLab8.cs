using System.Text;
using System.Text.RegularExpressions;

namespace inf_prot
{
    /// <summary>
    /// Метод изменения интервала между предложениями
    /// </summary>
    internal class EncBaseLab8 : IEncBaseLab
    {
        // Максимальное возможное количество бит для кодировки
        private int maxBitsCount;

        // Контейнер разбитый на предложения
        private List<string> containerSentences;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="containerText">Текстовый контейнер</param>
        public EncBaseLab8(string containerText)
        {
            // Делим контейнер на предложения, сохраняя символы конца предложений
            containerSentences = Regex.Split(containerText, @"(?<=[.!?])").ToList();

            // Проверяем, чтобы между предложением был 1 пробел (в данном случае проверяем чтобы вначале строки был 1 пробел)
            for (var i = 1; i < containerSentences.Count; i++)
            {
                switch (containerSentences[i].Length)
                {
                    case 0:
                        containerSentences[i] = " ";
                        break;

                    case 1:
                        if (containerSentences[i] != " ")
                        {
                            containerSentences[i] = " " + containerSentences[i];
                        }
                        break;

                    default:
                        if (containerSentences[i][0] != ' ')
                        {
                            containerSentences[i] = " " + containerSentences[i];
                        }
                        else if (containerSentences[i][0] == ' ' && containerSentences[i][1] == ' ')
                        {
                            containerSentences[i] = containerSentences[i].Substring(1);
                        }
                        break;
                }
            }

            maxBitsCount = containerSentences.Count;
        }

        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        public string Encrypt(string msg)
        {
            var encryptedMsg = new StringBuilder();
            var containerIndex = 0;

            foreach (var bits in GetBits(msg))
            {
                // Проверка на ограничение контейнера
                if (containerIndex + 8 > maxBitsCount)
                {
                    throw new Exception($"Невозможно уместить заданное сообщение в контейнере. Максимум бит - {maxBitsCount}");
                }

                foreach (var bit in bits)
                {
                    encryptedMsg.Append(containerSentences[containerIndex++]);

                    if (bit == '0')
                    {
                        encryptedMsg.Append(" ");
                    }
                }
            }

            // Добавляем маркер завершения
            encryptedMsg.Append($" ...");

            // Дозаполняем контейнер
            while (containerIndex != maxBitsCount)
            {
                encryptedMsg.Append(containerSentences[containerIndex++]);
            }

            return encryptedMsg.ToString();
        }

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Зашифрованное сообщение</param>
        /// <returns></returns>
        public string Decrypt(string enc)
        {
            var decryptedBits = new StringBuilder(maxBitsCount);

            foreach (var sentence in Regex.Split(enc, @"(?<=[.!?])"))
            {
                // Regex.Split сохраняет сами символы конца предложения, в отличие от string.Split()
                // Следовательно при нескольких точек подряд будет возвращена не пустая строка, а сама точка.
                // Также не может быть предложений длинной 1
                if (sentence.Length == 1)
                {
                    break;
                }
                // Определяем бит в зависимости от количества пробелов вначале строки
                else if (sentence[0] == ' ')
                {
                    if (sentence[1] == ' ')
                    {
                        decryptedBits.Append('0');
                    }
                    else
                    {
                        decryptedBits.Append('1');
                    }
                }
                
            }

            return GetMsgFromBits(decryptedBits.ToString()).TrimStart(['\0']);
        }

        /// <summary>
        /// Получить биты
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private IEnumerable<string> GetBits(string msg)
        {
            // Получить массив байт из исходной строки
            byte[] bytes = Encoding.GetEncoding(1251).GetBytes(msg);

            foreach (var _byte in bytes)
            {
                var tempBits = new StringBuilder(Convert.ToString(_byte, 2), 8);
                while (tempBits.Length != 8)
                    tempBits.Insert(0, "0");

                yield return tempBits.ToString();
            }
        }

        /// <summary>
        /// Получить сообщение
        /// </summary>
        /// <param name="bits">Строка бит</param>
        /// <returns></returns>
        private string GetMsgFromBits(string bits)
        {
            // Определяем сколько байт нужно получить
            var bytesCount = bits.Length / 8;
            if (bits.Length % 8 > 0)
                bytesCount++;

            // Разделение на байты
            var startIndex = bits.Length - 8;
            byte[] bytes = new byte[bytesCount];
            for (var i = bytesCount - 1; i > 0; i--)
            {
                var symbol = Convert.ToInt32(bits.Substring(startIndex, 8), 2);
                bytes[i] = (byte)symbol;

                if (i != 1)
                {
                    startIndex -= 8;
                }
                else
                {
                    // Выделяем оставшуюся часть и дополняем нулями
                    var bitsStr = bits.Substring(0, startIndex);
                    while (bitsStr.Length != 8)
                    {
                        bitsStr = bitsStr.Insert(0, "0");
                    }

                    symbol = Convert.ToInt32(bitsStr, 2);
                    bytes[0] = (byte)symbol;
                }
            }

            // Получаем сообщение
            return Encoding.GetEncoding(1251).GetString(bytes);
        }

        public void GenerateKey() {}

        public string GetKey()
        {
            return "";
        }

        public void SetKey(string jsonKey) {}

        // Заглушка, не нужно в текущей лабе
        public bool IsCharInDict(char ch)
        {
            return true;
        }

        // Заглушка, не нужно в текущей лабе
        public bool JsonEditorValidater(char ch)
        {
            return true; ;
        }
    }
}
