using System.Text;

namespace inf_prot
{
    /// <summary>
    /// Алгоритмом DES в режиме CFB
    /// </summary>
    internal class EncBaseLab6 : DES
    {
        // Начальный вектор
        private string initialVector;

        // Кол-во бит блока (<= 64)
        private int bitsCount;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="initialVector">Начальный вектор</param>
        public EncBaseLab6(string key, string initialVector, int bitsCount) : base(key)
        {
            var inVect = initialVector.Replace(" ", "");
            var initVector = new StringBuilder();

            for (var i = 0; i < 16; i++)
            {
                initVector.Append(Convert.ToString(Convert.ToInt64(inVect[i].ToString(), 16), 2).PadLeft(4, '0'));
            }

            this.initialVector = initVector.ToString();
            this.bitsCount = bitsCount;
        }

        /// <summary>
        /// Зашифровать сообщение
        /// </summary>
        /// <param name="msg">Исходное сообщение</param>
        /// <returns></returns>
        public override string Encrypt(string msg)
        {
            var encryptBits = new StringBuilder();
            var tempVector = initialVector.ToList();

            foreach (var bitArray in GetBitArrays(msg, bitsCount))
            {
                // Временный вектор шифруется по алгоритму DES
                var desResult = EncDecProcess(tempVector, true, true);

                // xor первых t (bitsCount) битов результата с блоком исходного текста
                var xorBits = XorBits(bitArray, desResult);
                encryptBits.Append(xorBits);

                // Сдвиг временного вектора на t (bitsCount) битов влево, заполняя справа xor битами
                var vector = tempVector.ToList();
                var index = 63;
                for (var i = bitsCount - 1; i >= 0; i--)
                {
                    tempVector[index--] = xorBits[i];
                }
                var vectorIndex = 63;
                while (index >= 0)
                {
                    tempVector[index--] = vector[vectorIndex--];
                }
            }

            return GetMsgFromBits(encryptBits.ToString()).TrimStart(['\0']);
        }

        /// <summary>
        /// Расшифровать сообщение
        /// </summary>
        /// <param name="enc">Зашифрованное сообщение</param>
        /// <returns></returns>
        public override string Decrypt(string enc)
        {
            var decryptBits = new StringBuilder();
            var tempVector = initialVector.ToList();

            foreach (var bitArray in GetBitArrays(enc, bitsCount))
            {
                // Временный вектор шифруется по алгоритму DES
                var desResult = EncDecProcess(tempVector, true, true);

                // xor первых t (bitsCount) битов результата с блоком исходного текста
                var xorBits = XorBits(bitArray, desResult);
                decryptBits.Append(xorBits);

                // Сдвиг временного вектора на t (bitsCount) битов влево, заполняя справа bitArray битами
                var vector = tempVector.ToList();
                var index = 63;
                for (var i = bitsCount - 1; i >= 0; i--)
                {
                    tempVector[index--] = bitArray[i];
                }
                var vectorIndex = 63;
                while (index >= 0)
                {
                    tempVector[index--] = vector[vectorIndex--];
                }
            }

            return GetMsgFromBits(decryptBits.ToString()).TrimStart(['\0']);
        }

        /// <summary>
        /// xor бит
        /// </summary>
        /// <param name="bitsList">Блок битов List</param>
        /// <param name="bitsStr">Блок битов string</param>
        /// <returns></returns>
        private string XorBits(List<char> bitsList, string bitsStr)
        {
            var xor = new StringBuilder();

            for (var i = 0; i < bitsCount; i++)
            {
                xor.Append(bitsList[i] == bitsStr[i] ? '0' : '1');
            }

            return xor.ToString();
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
    }
}
