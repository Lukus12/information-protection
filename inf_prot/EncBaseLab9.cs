using System.Text;
using System.Text.RegularExpressions;

namespace inf_prot
{
    /// <summary>
    /// Метод изменения количества пробелов в конце текстовых строк
    /// </summary>
    internal class EncBaseLab9 : IEncBaseLab
    {
        // Максимальное возможное количество бит для кодировки
        private int maxBitsCount;

        // Контейнер разбитый на строки
        private List<string> containerStrings;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="containerText">Текстовый контейнер</param>
        public EncBaseLab9(string containerText)
        {
            // Делим контейнер на строки
            containerStrings = containerText.Split('\n').ToList();

            maxBitsCount = containerStrings.Count * 2;
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
                if (containerIndex * 2 + 8 > maxBitsCount)
                {
                    throw new Exception($"Невозможно уместить заданное сообщение в контейнере. Максимум бит - {maxBitsCount}");
                }

                var bitsCount = 1;
                foreach (var bit in bits)
                {
                    if (bitsCount % 2 != 0)
                    {
                        encryptedMsg.Append(containerStrings[containerIndex++].TrimEnd(' '));
                    }

                    switch (bit)
                    {
                        // Ставим «обычный» пробел
                        case '0':
                            encryptedMsg.Append((char)32);
                            break;


                        // Ставим «неразрывный» пробел
                        case '1':
                            encryptedMsg.Append((char)160);
                            break;
                    }

                    if (bitsCount % 2 == 0)
                    {
                        encryptedMsg.Append('\n');
                    }

                    bitsCount++;
                }
            }

            // Дозаполняем контейнер
            while (containerIndex < containerStrings.Count)
            {
                encryptedMsg.Append(containerStrings[containerIndex++].TrimEnd(' '));

                if (containerIndex < containerStrings.Count)
                {
                    encryptedMsg.Append('\n');
                }
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

            foreach (var str in enc.Split('\n'))
            {
                var noBits = false;
                var tempBit = ' ';

                if (str.Length == 0)
                {
                    continue;
                }

                switch (str[str.Length - 1])
                {
                    case (char)32:
                        tempBit = '0';
                        break;

                    case (char)160:
                        tempBit = '1';
                        break;

                    default:
                        noBits = true;
                        break;
                }

                if (noBits)
                {
                    break;
                }

                switch (str[str.Length - 2])
                {
                    case (char)32:
                        decryptedBits.Append('0');
                        break;

                    case (char)160:
                        decryptedBits.Append('1');
                        break;

                    default:
                        noBits = true;
                        break;
                }

                if (noBits)
                {
                    break;
                }

                decryptedBits.Append(tempBit);
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

        public void GenerateKey() { }

        public string GetKey()
        {
            return "";
        }

        public void SetKey(string jsonKey) { }

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
