namespace inf_prot
{
    /// <summary>
    /// Механизм шифрования сообщений
    /// </summary>
    public interface IEncBaseLab
    {
        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        string Encrypt(string msg);

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Зашифрованное сообщение</param>
        /// <returns></returns>
        string Decrypt(string enc);

        /// <summary>
        /// Проверяет присутсвие символа в алфавите шифруемых сообщений
        /// </summary>
        /// <param name="ch">Символ для проверки</param>
        /// <returns>true - есть в алфавите, false - нет</returns>
        bool IsCharInDict(char ch);

        /// <summary>
        /// Генерация ключа
        /// </summary>
        void GenerateKey();

        /// <summary>
        /// Вернуть представление ключа в виде строки
        /// </summary>
        /// <returns>JSON ключ в виде строки</returns>
        string GetKey();

        /// <summary>
        /// Задать ключ из json
        /// </summary>
        /// <param name="jsonKey">Ключ в формате JSON</param>
        void SetKey(string jsonKey);

        /// <summary>
        /// Валидатор для JSON редактора
        /// </summary>
        /// <param name="ch">Введенный символ</param>
        /// <returns></returns>
        bool JsonEditorValidater(char ch);
    }
}
