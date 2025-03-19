using System.Text;

namespace inf_prot
{
    /// <summary>
    /// Алгоритмом DES в режиме ECB
    /// </summary>
    internal class EncBaseLab4 : DES
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="key">Ключ</param>
        public EncBaseLab4(string key) : base(key) {}

        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        public override string Encrypt(string msg)
        {
            var decryptMsg = new StringBuilder();

            foreach (var bitArray in GetBitArrays(msg, 64))
            {
                decryptMsg.Append(EncDecProcess(bitArray, true, false));
            }

            return decryptMsg.ToString().TrimStart(['\0']);
        }

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Зашифрованное сообщение</param>
        /// <returns></returns>
        public override string Decrypt(string enc)
        {
            var encryptMsg = new StringBuilder();

            foreach (var bitArray in GetBitArrays(enc, 64))
            {
                encryptMsg.Append(EncDecProcess(bitArray, false, false));
            }

            return encryptMsg.ToString().TrimStart(['\0']);
        }
    }
}
