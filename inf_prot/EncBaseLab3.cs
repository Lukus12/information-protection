using System.Text;
using System.Text.Json;

namespace inf_prot
{
    /// <summary>
    /// Механизм линейного шифрования данных (гаммирование)
    /// </summary>
    internal class EncBaseLab3 : IEncBaseLab
    {
        // Необходимые для генерации гаммы значения
        private int a, b, c, t;

        // Ключ
        private string key;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="a">Нечетное число A (такое, что A mod 4 = 1)</param>
        /// <param name="c">Нечетное число C</param>
        /// <param name="t0">Порождающее число</param>
        public EncBaseLab3(int a, int c, int t0)
        {
            this.a = a;
            this.c = c;
            b = 256;
            t = t0;

            key = CreateKey();
        }

        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        public string Encrypt(string msg)
        {
            return EncDecProcess(msg);
        }

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Расшифрованное сообщение</param>
        /// <returns></returns>
        public string Decrypt(string enc)
        {
            return EncDecProcess(enc);
        }

        /// <summary>
        /// Валидатор для JSON редактора
        /// </summary>
        /// <param name="ch">Введенный символ</param>
        /// <returns></returns>
        public bool JsonEditorValidater(char ch)
        {
            if (int.TryParse(ch.ToString(), out int number))
                return true;

            switch (ch)
            {
                case 'A':
                case 'B':
                case 'C':
                case 'T':
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Алгоритм шифрования и расшифрования сообщений
        /// </summary>
        /// <param name="msg">Введенное сообщение</param>
        /// <returns></returns>
        private string EncDecProcess(string msg)
        {
            var resultStr = new StringBuilder();
            var gamma = t;

            for (var i = 0; i < msg.Length; i++)
            {
                gamma = (a * gamma + c) % b;
                resultStr.Append((char)(msg[i] ^ gamma));
            }

            return resultStr.ToString();
        }

        /// <summary>
        /// Создание ключа
        /// </summary>
        /// <returns>JSON ключ</returns>
        private string CreateKey()
        {
            // Создание словаря: параметр - значение
            var labParams = new Dictionary<string, int>()
            {
                { "A", a },
                { "C", c },
                { "B", b },
                { "T", t }
            };

            return JsonSerializer.Serialize(labParams, new JsonSerializerOptions() { WriteIndented = true });
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
            var deserializedKey = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonKey);

            if (deserializedKey != null &&
                deserializedKey.ContainsKey("A") && deserializedKey.ContainsKey("B") &&
                deserializedKey.ContainsKey("C") && deserializedKey.ContainsKey("T"))
            {
                foreach (var option in deserializedKey)
                {
                    switch (option.Key)
                    {
                        case "A":
                            a = option.Value;
                            break;

                        case "B":
                            b = option.Value;
                            break;

                        case "C":
                            c = option.Value;
                            break;

                        case "T":
                            t = option.Value;
                            break;
                    }
                }

                key = jsonKey;
            }
            else
                throw new Exception();
        }

        // Заглушка, не нужно в текущей лабе
        public bool IsCharInDict(char ch) { return true; }

        // Заглушка, не нужно в текущей лабе
        public void GenerateKey() { }
    }
}
