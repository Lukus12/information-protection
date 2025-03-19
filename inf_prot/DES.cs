using inf_prot;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace inf_prot
{
    /// <summary>
    /// DES шифрование
    /// </summary>
    internal class DES : IEncBaseLab
    {
        // Ключ K
        protected string key;
        // Преобразования ключа
        private List<string> transformedKeys;
        // Матрица начальной перестановки IP
        protected List<int> initialPermutationIP;
        // Матрица конечной перестановки IP^-1
        protected List<int> inversePermutationIP;
        // Функция расширения E
        private List<int> extensionFuncE;
        // Матрица G первоначальной подготовки ключа
        private List<int> keyStartPreparationG;
        // Таблица сдвигов для вычисления ключа
        private List<int> keyShift = new List<int>() { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        // Матрица H завершающей обработки ключа
        private List<int> keyEndPreparationH = new List<int>()
        {
            13, 16, 10, 23, 0, 4,
            2, 27, 14, 5, 20, 9,
            22, 18, 11, 3, 25, 7,
            15, 6, 26, 19, 12, 1,
            40, 51, 30, 36, 46, 54,
            29, 39, 50, 44, 32, 47,
            43, 48, 38, 55, 33, 52,
            45, 41, 49, 35, 28, 31
        };
        // Функции преобразования S1, S2, ..., S8
        private List<List<int>> transformFuncS = new List<List<int>>()
        {
            {
                [ 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
                  0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
                  4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
                  15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 ]
            },
            {
                [ 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
                  3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
                  0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
                  13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 ]
            },
            {
                [ 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
                  13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
                  13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
                  1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 ]
            },
            {
                [ 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
                  13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
                  10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
                  3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 ]
            },
            {
                [ 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
                  14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
                  4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
                  11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 ]
            },
            {
                [ 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
                  10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
                  9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
                  4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 ]
            },
            {
                [ 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
                  13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
                  1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
                  6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 ]
            },
            {
                [ 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
                  1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
                  7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
                  2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 ]
            }
        };
        // Функция перестановки P
        private List<int> permutationFuncP = new List<int>() {
            15, 6, 19, 20,
            28, 11, 27, 16,
            0, 14, 22, 25,
            4, 17, 30, 9,
            1, 7, 23, 13,
            31, 26, 2, 8,
            18, 12, 29, 5,
            21, 10, 3, 24
        };
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="key">Ключ</param>
        public DES(string key)
        {
            // Инициализация таблицы начальных перестановок
            initialPermutationIP = new List<int>();
            var initializeHelper = new List<int>() { 57, 59, 61, 63, 56, 58, 60, 62 };
            foreach (var num in initializeHelper)
            {
                initialPermutationIP.Add(num);
                var temp = initialPermutationIP[^1] - 8;
                while (temp >= 0)
                {
                    initialPermutationIP.Add(temp);
                    temp -= 8;
                }
            }
            // Инициализация таблицы начальных перестановок
            inversePermutationIP = new List<int>();
            initializeHelper = new List<int>() { 39, 7, 47, 15, 55, 23, 63, 31 };
            for (var i = 0; i < 64; i++)
                inversePermutationIP.Add(0);
            var index = 0;
            foreach (var num in initializeHelper)
            {
                inversePermutationIP[index] = initializeHelper[index];
                var prevVal = inversePermutationIP[index];
                for (var i = index + 8; i < index + 8 * 8; i += 8)
                {
                    inversePermutationIP[i] = --prevVal;
                    prevVal = inversePermutationIP[i];
                }
                index++;
            }
            // Инициализация функции расширения E
            extensionFuncE = new List<int>() { 31, 0 };
            var stepsForRepeat = 4;
            for (var i = 1; i < 32; i++)
            {
                extensionFuncE.Add(i);
                stepsForRepeat--;
                if (stepsForRepeat == 0)
                {
                    stepsForRepeat = 5;
                    i -= 1;
                    extensionFuncE.Add(i);
                }
            }
            extensionFuncE.Add(0);
            // Инициализация матрицы первоначальной подготовки ключа
            keyStartPreparationG = new List<int>();
            initializeHelper = new List<int>() { 56, 57, 58, 59, 62, 61, 60, 27 };
            foreach (var num in initializeHelper)
            {
                keyStartPreparationG.Add(num);
                var temp = keyStartPreparationG[^1] - 8;
                while (temp >= 0 && keyStartPreparationG.Count < 56)
                {
                    keyStartPreparationG.Add(temp);
                    if (temp == 35)
                        break;
                    temp -= 8;
                }
            }
            // Инициализация ключа K
            this.key = CreateKey(key);
            KeyPrep(key.Replace(" ", ""));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        /// <summary>
        /// Получить битовые отрезки
        /// </summary>
        /// <param name="msg">Исходная строка</param>
        /// <returns></returns>
        protected IEnumerable<List<char>> GetBitArrays(string msg, int bitsCount)
        {
            // Получить массив байт из исходной строки
            byte[] bytes = Encoding.GetEncoding(1251).GetBytes(msg);
            // Получить битовую строку и нормализировать её
            var bitArray = new List<char>();
            foreach (var _byte in bytes)
            {
                var tempBits = Convert.ToString(_byte, 2).ToList();
                while (tempBits.Count % 8 != 0)
                    tempBits.Insert(0, '0');
                bitArray.AddRange(tempBits);
            }
            // Проверка на лишние нулевые биты вначале
            var test = bitArray.GetRange(0, bitArray.Count % bitsCount);
            var findOneBit = false;
            foreach (var bit in test)
            {
                if (bit == '1')
                {
                    findOneBit = true;
                    break;
                }
            }
            if (!findOneBit)
            {
                bitArray = bitArray.GetRange(test.Count, bitArray.Count - test.Count);
            }
            while (bitArray.Count % bitsCount != 0)
                bitArray.Insert(0, '0');
            // Вернуть отрезки
            for (var i = 0; i < bitArray.Count; i += bitsCount)
                yield return bitArray.GetRange(i, bitsCount);
        }
        /// <summary>
        /// Алгоритм шифрования и расшифрования сообщений
        /// </summary>
        /// <param name="bitArray">Блок битов</param>
        /// <param name="isEncrypt">true - шифрование, false - расшифрование</param>
        /// <param name="resultInBits">вернуть результат в виде строки бит</param>
        /// <returns></returns>
        protected virtual string EncDecProcess(List<char> bitArray, bool isEncrypt, bool resultInBits)
        {
            //Debug.WriteLine("Вход: " + string.Join("", bitArray.ToArray()) + '\n');
            // Если шифрование, то начальная перестановка
            // Если дешифрование, то обратная конечная перестановка
            var matrix = isEncrypt ? initialPermutationIP : inversePermutationIP;
            var permutatedBits = new List<char>();
            for (var i = 0; i < matrix.Count; i++)
                permutatedBits.Add(bitArray[isEncrypt ? matrix[i] : matrix.IndexOf(i)]);
            //Debug.WriteLine("Перемешивание: " + string.Join("", permutatedBits.ToArray()) + '\n');
            // Шифрующие преобразования
            // Если шифрование, то начинаем с ключа 1
            // Если дешифрование, то начинаем с ключа 16
            var keyNum = isEncrypt ? 0 : 15;
            while (keyNum < 16 && keyNum >= 0)
            {
                permutatedBits = BitsTransform(permutatedBits.GetRange(0, 32), permutatedBits.GetRange(32, 32), keyNum, isEncrypt);
                if (isEncrypt)
                    keyNum++;
                else
                    keyNum--;
            }
            //Debug.WriteLine("Преобразование: " + string.Join("", permutatedBits.ToArray()) + '\n');
            // Если шифрование, то конечная перестановка
            // Если дешифрование, то обратная начальная перестановка
            var tempBits = permutatedBits.ToList();
            matrix = isEncrypt ? inversePermutationIP : initialPermutationIP;
            for (var i = 0; i < matrix.Count; i++)
                permutatedBits[i] = tempBits[isEncrypt ? matrix[i] : matrix.IndexOf(i)];
            //Debug.WriteLine("Перемешивание: " + string.Join("", permutatedBits.ToArray()) + '\n');
            var bitStr = new StringBuilder(); // Строка бит
            if (resultInBits)
            {
                // Возвращаем результат в виде строки бит
                foreach (var bit in permutatedBits)
                    bitStr.Append(bit);
                return bitStr.ToString();
            }
            else
            {
                // Разделение на байты
                byte[] bytes = new byte[8];
                for (var i = 0; i < 8; i++)
                {
                    foreach (var bit in permutatedBits.GetRange(8 * i, 8))
                        bitStr.Append(bit);
                    var symbol = Convert.ToInt32(bitStr.ToString(), 2);
                    bytes[i] = (byte)symbol;
                    bitStr.Clear();
                }
                //Debug.WriteLine("Байты: " + string.Join(", ", bytes.ToArray()) + '\n');
                // Получаем сообщение
                return Encoding.GetEncoding(1251).GetString(bytes);
            }
        }
        /// <summary>
        /// Шифрующие преобразования
        /// </summary>
        /// <param name="bitsL">Старшие биты</param>
        /// <param name="bitsR">Младшие биты</param>
        /// <param name="step">Шаг итерации</param>
        /// <param name="isEncrypt">true - шифрование, false - расшифрование</param>
        /// <returns></returns>
        protected List<char> BitsTransform(List<char> bitsL, List<char> bitsR, int step, bool isEncrypt)
        {
            // Подготовка ключа
            var newKey = transformedKeys[step];
            // Расширение битов
            // Если шифрование, то bitsR
            // Если дешифрование, то bitsL
            var bitsToExtension = isEncrypt ? bitsR : bitsL;
            var extensionBits = new List<char>();
            for (var i = 0; i < extensionFuncE.Count; i++)
                extensionBits.Add(bitsToExtension[extensionFuncE[i]]);
            // Сложение по модулю 2 с ключом
            for (var i = 0; i < newKey.Length; i++)
                extensionBits[i] = (extensionBits[i] ^ newKey[i]).ToString()[0];
            // S преобразование
            var transformedBits = "";
            for (var i = 0; i < 8; i++)
                transformedBits += TranformFuncSHelper(extensionBits.GetRange(6 * i, 6), i);
            // Перестановка битов
            var permutatedBits = new List<char>();
            for (var i = 0; i < permutationFuncP.Count; i++)
                permutatedBits.Add(transformedBits[permutationFuncP[i]]);
            // Вычисление новых битов
            // Если шифрование, то вычисление bitsR
            // Если дешифрование, то вычисление bitsL
            var newBits = new List<char>();
            for (var i = 0; i < permutatedBits.Count; i++)
                newBits.Add((permutatedBits[i] ^ (isEncrypt ? bitsL[i] : bitsR[i])).ToString()[0]);
            // Формирование битовой комбинации
            var resultBits = isEncrypt ? bitsR : newBits;
            if (isEncrypt)
                resultBits.AddRange(newBits);
            else
                resultBits.AddRange(bitsL);
            return resultBits;
        }
        /// <summary>
        /// S преобразование
        /// </summary>
        /// <param name="bits">6-битовый блок</param>
        /// <param name="step">Шаг итерации</param>
        /// <returns>Новое число</returns>
        private string TranformFuncSHelper(List<char> bits, int step)
        {
            // Получить новое значение из таблицы transformFuncS
            // Биты 0 и 5 определяют номер строки (в данном случае вычисляется сдвиг)
            // Биты 1 - 4 определяют номер столбца (+ к сдвигу)
            var decimalNum = transformFuncS[step][16 * Convert.ToInt32(bits[0].ToString() + bits[5], 2) +
                    Convert.ToInt32(bits[1].ToString() + bits[2] + bits[3] + bits[4], 2)];
            return Convert.ToString(decimalNum, 2).PadLeft(4, '0');
        }
        /// <summary>
        /// Подготовка ключа
        /// </summary>
        /// <param name="key">Ключ</param>
        protected void KeyPrep(string key)
        {
            // Начальная подготовка ключа
            var binaryKey = new StringBuilder();
            for (var i = 0; i < 16; i++)
            {
                binaryKey.Append(Convert.ToString(Convert.ToInt64(key[i].ToString(), 16), 2).PadLeft(4, '0'));
            }
            var newKey = new List<char>();
            for (var i = 0; i < keyStartPreparationG.Count; i++)
                newKey.Add(binaryKey[keyStartPreparationG[i]]);
            var cKey = new List<char>();
            var dKey = new List<char>();
            transformedKeys = new List<string>();
            for (var i = 0; i < 16; i++)
            {
                // Сдвиг влево
                if (cKey.Count == 0 && dKey.Count == 0)
                {
                    cKey = KeyShift(newKey.GetRange(0, 28), i);
                    dKey = KeyShift(newKey.GetRange(28, 28), i);
                }
                else
                {
                    cKey = KeyShift(cKey, i);
                    dKey = KeyShift(dKey, i);
                }
                var tempKey = new List<char>();
                tempKey = [.. cKey, .. dKey];
                // Конечная подготовка ключа
                newKey = new List<char>();
                for (var j = 0; j < keyEndPreparationH.Count; j++)
                    newKey.Add(tempKey[keyEndPreparationH[j]]);
                transformedKeys.Add(new string(newKey.ToArray()));
            }
        }
        /// <summary>
        /// Сдвиг ключа
        /// </summary>
        /// <param name="bits">Биты ключа</param>
        /// <param name="step">Шаг итерации</param>
        /// <returns></returns>
        private List<char> KeyShift(List<char> bits, int step)
        {
            var tempKey = bits.ToList();
            for (var i = 0; i < tempKey.Count; i++)
            {
                var index = i + keyShift[step];
                if (index >= tempKey.Count)
                    index = index - tempKey.Count;
                bits[i] = tempKey[index];
            }
            return bits;
        }
        /// <summary>
        /// Создание ключа
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>JSON ключ</returns>
        protected virtual string CreateKey(string key)
        {
            var jsonKey = new Dictionary<string, string>() { { "key", key } };
            return JsonSerializer.Serialize(jsonKey, new JsonSerializerOptions() { WriteIndented = true });
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
        public virtual void SetKey(string jsonKey)
        {
            var deserializedKey = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonKey);
            if (deserializedKey.ContainsKey("key"))
            {
                var hexKey = deserializedKey["key"].Replace(" ", "");
                var regex = new Regex(@"^[0-9A-F\r\n]+$");
                if (hexKey.Length == 16 && regex.Match(hexKey).Success)
                {
                    key = jsonKey;
                    KeyPrep(hexKey);
                }
            }
        }
        // Заглушка, нужен оверрайд
        public virtual string Encrypt(string msg) { return string.Empty; }
        // Заглушка, нужен оверрайд
        public virtual string Decrypt(string enc) { return string.Empty; }
        // Заглушка, не нужно в текущей лабе
        public bool JsonEditorValidater(char ch) { return true; }
        // Заглушка, не нужно в текущей лабе
        public bool IsCharInDict(char ch) { return true; }
        // Заглушка, не нужно в текущей лабе
        public void GenerateKey() { }
    }
}
