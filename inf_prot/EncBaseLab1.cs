using System.Text;
using System.Text.Json;

namespace inf_prot
{
    /// <summary>
    /// Механизм шифрования сообщений подстановками
    /// </summary>
    internal class EncBaseLab1 : EncBase, IEncBaseLab
    {
        // Алфавит исходных - шифрованных комбинаций
        protected Dictionary<string, string> srcToEncAlphabet { get; set; }

        // Алфавит шифрованных - исходных комбинаций
        private Dictionary<string, string> encToSrcAlphabet { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="alph">Алфавит шифруемых сообщений</param>
        /// <param name="alphLength">Длина блока открытого текста и блока шифрограммы</param>
        public EncBaseLab1(string alph, int alphLength) : base(alph, alphLength)
        {
            srcToEncAlphabet = new Dictionary<string, string>();
            encToSrcAlphabet = new Dictionary<string, string>();

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
        /// <param name="enc">Расшифрованное сообщение</param>
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
            return alphabet.Contains(ch);
        }

        /// <summary>
        /// Валидатор для JSON редактора
        /// </summary>
        /// <param name="ch">Введенный символ</param>
        /// <returns></returns>
        public bool JsonEditorValidater(char ch)
        {
            if (IsCharInDict(ch))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Генерация алфавита
        /// </summary>
        /// <returns>Словарь необходимых алфавитов</returns>
        private Dictionary<string, Dictionary<string, string>> GenerateAlphabets()
        {
            var srcLst = new List<string>();
            var alph = alphabet.ToList();

            // Задает символы из алфавита шифруемых сообщений в определенных позициях
            // Начинаем с первых символов алфавита - 0 и с минимальной длины
            var valueInPos = new List<int>() { 0 };

            var isInit = false;
            while (!isInit)
            {
                // Добавление возможной комбинации в исходный алфавит
                var srcStr = new StringBuilder();
                foreach (var index in valueInPos)
                {
                    srcStr.Append(alph[index]);
                }
                srcLst.Add(srcStr.ToString());

                // Изменение возможной комбинации
                var posIndex = valueInPos.Count - 1;
                valueInPos[posIndex]++;

                // Нормализация возможной комбинации
                var isNormalized = false;
                while (!isNormalized)
                {
                    // Посимвольный перебор справа налево в пределах заданных символов
                    if (valueInPos[posIndex] > alph.Count - 1)
                    {
                        if (posIndex != 0)
                        {
                            valueInPos[posIndex] = 0;
                            valueInPos[--posIndex]++;
                        }
                        else
                        {
                            isNormalized = true;

                            if (valueInPos.Count < combinationLength)
                            {
                                // Добавить комбинации другой длины
                                for (var i = 0; i < valueInPos.Count; i++)
                                    valueInPos[i] = 0;
                                valueInPos.Add(0);
                            }
                            else
                                isInit = true;
                        }
                    }
                    else
                        isNormalized = true;
                }
            }

            // Инициализации алфавита зашифрованных комбинаций
            var encLst = new List<string>();
            var tempLst = new List<string>();
            foreach (var key in srcLst)
            {
                if (tempLst.Count == 0 || tempLst.Count != 0 && key.Length == tempLst[^1].Length)
                    tempLst.Add(key);
                else
                {
                    // Если следующая комбинация больше по длине, то перемешиваем текущий темповый словарь
                    Shuffle(tempLst);
                    encLst.AddRange(tempLst);

                    tempLst = new List<string>() { key };
                }
            }

            Shuffle(tempLst);
            encLst.AddRange(tempLst);

            // Формирование словарей
            var srcToEncAlph = new Dictionary<string, string>();
            var encToSrcAlph = new Dictionary<string, string>();
            for (var i = 0; i < srcLst.Count; i++)
            {
                srcToEncAlph.Add(srcLst[i], encLst[i]);
                encToSrcAlph.Add(encLst[i], srcLst[i]);
            }

            return new Dictionary<string, Dictionary<string, string>>()
            {
                { "srcToEncAlphabet", srcToEncAlph },
                { "encToSrcAlphabet", encToSrcAlph }
            };
        }

        /// <summary>
        /// Алгоритм шифрования и расшифрования сообщений
        /// </summary>
        /// <param name="msg">Введенное сообщение</param>
        /// <param name="isEncrypt">true - шифрование, false - расшифрование</param>
        /// <returns></returns>
        private string EncDecProcess(string msg, bool isEncrypt) {
            var resultStr = new StringBuilder(msg.Length);
            var firstSymb = 0;
            var isProcessEnd = false;
            var alph = isEncrypt ? srcToEncAlphabet : encToSrcAlphabet;

            while (!isProcessEnd)
            {
                // Проверка на длину строки
                var length = combinationLength;
                if (firstSymb + length >= msg.Length)
                {
                    length = msg.Length - firstSymb;
                    isProcessEnd = true;
                }

                resultStr.Append(alph[msg.Substring(firstSymb, length)]);

                firstSymb = firstSymb + combinationLength;
            }

            return resultStr.ToString();
        }

        /// <summary>
        /// Создание ключа
        /// </summary>
        /// <returns>JSON ключ</returns>
        private string CreateKey()
        {
            return SerializeKey(srcToEncAlphabet);
        }

        /// <summary>
        /// Генерация алфавита и ключа
        /// </summary>
        public void GenerateKey()
        {
            var alphabets = GenerateAlphabets();

            srcToEncAlphabet = alphabets["srcToEncAlphabet"];
            encToSrcAlphabet = alphabets["encToSrcAlphabet"];

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
            var deserializedKey = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonKey);

            if (deserializedKey != null)
            {
                srcToEncAlphabet = deserializedKey;
                encToSrcAlphabet = new Dictionary<string, string>();

                var tempLength = 0;
                foreach (var alph in srcToEncAlphabet)
                {
                    encToSrcAlphabet.Add(alph.Value, alph.Key);
                    if (alph.Value.Length > tempLength)
                        tempLength = alph.Value.Length;
                }

                combinationLength = tempLength;
                key = jsonKey;
            }
            else
                throw new Exception();
        }
    }
}
