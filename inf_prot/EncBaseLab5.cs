using System.Text;

namespace inf_prot
{
    /// <summary>
    /// Алгоритмом DES в режиме CBC
    /// </summary>
    internal class EncBaseLab5 : DES
    {
        // Начальный и временный вектор
        private string initialVector;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="initialVector">Начальный вектор</param>
        public EncBaseLab5(string key, string initialVector) : base(key)
        {
            var inVect = initialVector.Replace(" ", "");
            var initVector = new StringBuilder();

            for (var i = 0; i < 16; i++)
            {
                initVector.Append(Convert.ToString(Convert.ToInt64(inVect[i].ToString(), 16), 2).PadLeft(4, '0'));
            }

            this.initialVector = initVector.ToString();
        }

        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        public override string Encrypt(string msg)
        {
            var encryptMsg = new StringBuilder();
            var tempVector = initialVector;

            foreach (var bitArray in GetBitArrays(msg, 64))
            {
                var xorBits = XorBits(bitArray, tempVector);
                tempVector = EncDecProcess(xorBits, true, true);
                encryptMsg.Append(GetMsgFromBits(tempVector));
            }

            return encryptMsg.ToString().TrimStart(['\0']);
        }

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Зашифрованное сообщение</param>
        /// <returns></returns>
        public override string Decrypt(string enc)
        {
            var decryptMsg = new StringBuilder();
            var resultBitsStr = new StringBuilder();
            var tempBits = new List<char>();
            var tempVector = initialVector.ToList();

            foreach (var bitArray in GetBitArrays(enc, 64))
            {
                tempBits = bitArray;

                var resultBits = EncDecProcess(bitArray, false, true);
                var xorBits = XorBits(tempVector, resultBits);

                foreach (var bit in xorBits)
                {
                    resultBitsStr.Append(bit);
                }

                decryptMsg.Append(GetMsgFromBits(resultBitsStr.ToString()));
                resultBitsStr.Clear();

                tempVector = tempBits;
            }

            return decryptMsg.ToString().TrimStart(['\0']);
        }

        /// <summary>
        /// xor бит
        /// </summary>
        /// <param name="bitsList">Блок битов List</param>
        /// <param name="bitsStr">Блок битов string</param>
        /// <returns></returns>
        private static List<char> XorBits(List<char> bitsList, string bitsStr)
        {
            for (var i = 0; i < bitsList.Count; i++)
            {
                bitsList[i] = bitsList[i] == bitsStr[i] ? '0' : '1';
            }

            return bitsList;
        }

        /// <summary>
        /// Получить сообщение
        /// </summary>
        /// <param name="bits">Строка бит</param>
        /// <returns></returns>
        private static string GetMsgFromBits(string bits)
        {
            // Разделение на байты
            var startIndex = 0;
            byte[] bytes = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                var symbol = Convert.ToInt32(bits.Substring(startIndex, 8), 2);
                bytes[i] = (byte)symbol;

                startIndex += 8;
            }

            // Получаем сообщение
            return Encoding.GetEncoding(1251).GetString(bytes);
        }
    }
}
