using inf_prot;

namespace inf_prot
{
    public partial class MainWindow : Form
    {
        IEncBaseLab currentLab;
        private EncBaseLab1 encBaseLab1 = new EncBaseLab1("01", 5);
        private EncBaseLab2 encBaseLab2 = new EncBaseLab2(
            "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz", 15);
        private EncBaseLab3 encBaseLab3 = new EncBaseLab3(5, 51, 13);
        private EncBaseLab4 encBaseLab4 = new EncBaseLab4("ABCD ABCD ABCD ABCD");
        private EncBaseLab5 encBaseLab5 = new EncBaseLab5("ABCD ABCD ABCD ABCD", "ABCD ABCD ABCD ABCD");
        private EncBaseLab6 encBaseLab6 = new EncBaseLab6("ABCD ABCD ABCD ABCD", "ABCD ABCD ABCD ABCD", 30);
        private EncBaseLab7 encBaseLab7 = new EncBaseLab7("ABCD ABCD ABCD ABCD", "ABCD ABCD ABCD ABCD", 30);
        private EncBaseLab8 encBaseLab8 = new EncBaseLab8("Основные понятия стеганографии и обобщенная структурная схема стеганографической системы приведены в тексте лекций.\r\nМетод изменения интервала между предложениями основан на размещении одного или двух пробелов после каждого символа завершения предложения. При этом, обычно, единичным пробелом кодируется бит «1», а двойным пробелом – бит «0». Признаком завершения предложения можно считать точку, вопросительный и/или восклицательный знаки, после которых следует пробел. Скрываемое сообщение в двоичном формате встраивается в контейнер-текст путем размещения соответствующего каждому биту числа пробелов после каждого предложения. \r\nПоскольку контейнер имеет существенно больший объем, чем сообщение, протокол стеганографического обмена должен предусматривать некоторый маркер завершения сообщения. Этот маркер дописывается в конец сообщения на этапе предварительной обработки, до поступления сообщения в стеганокодер. При извлечении сообщения из контейнера, обнаружение маркера позволяет прервать обработку и не анализировать оставшуюся часть контейнера.\r\nНа практике, стеганографические системы, реализующие рассмотренный и подобные методы, осуществляют предварительную обработку пустого контейнера. В ходе предобработки из текста контейнера удаляются «лишние» пробелы, которые могли оказаться в конце предложений на этапе создания контейнера: это может произойти из-за особенностей конкретного текстового редактора, системы распознавания сканированного текста, или действий человека, набиравшего текст.\r\nВ данной лабораторной работе необходимо программно реализовать алгоритмы прямого и обратного стеганографического преобразования. \r\nПустой и заполненный текстовые контейнеры должны задаваться пользователем в виде файлов либо через графический интерфейс – стандартный диалог выбора файла и/или поле ввода имени файла, либо через параметры командной строки. Выбор вида интерфейса (графический или командной строки) – на усмотрение студента, выполняющего работу. Аналогичным образом задается скрываемое сообщение. \r\nПеред прямым стеганографическим преобразованием должна выполняться предварительная обработка пустого контейнера для замены двух и более пробелов на один пробел после окончания предложения, а также добавление к сообщению маркера окончания. Маркер окончания выбирается студентом самостоятельно таким образом, чтобы выбранный маркер не являлся частью исходного сообщения. До начала стеганографического преобразования предобработчик должен выводить пользователю информацию о максимальной емкости контейнера и возможности размещения заданного сообщения в выбранном контейнере.\r\nДля простоты реализации можно принять, что сообщение начинает размещаться в контейнере с первой возможной позиции (то есть, после первого же предложения) и размещается последовательно; при этом стеганографический ключ отсутствует.");
        private EncBaseLab9 encBaseLab9 = new EncBaseLab9("Основные понятия стеганографии и обобщенная структурная схема стеганографической системы приведены в тексте лекций.\r\nМетод изменения интервала между предложениями основан на размещении одного или двух пробелов после каждого символа завершения предложения. При этом, обычно, единичным пробелом кодируется бит «1», а двойным пробелом – бит «0». Признаком завершения предложения можно считать точку, вопросительный и/или восклицательный знаки, после которых следует пробел. Скрываемое сообщение в двоичном формате встраивается в контейнер-текст путем размещения соответствующего каждому биту числа пробелов после каждого предложения. \r\nПоскольку контейнер имеет существенно больший объем, чем сообщение, протокол стеганографического обмена должен предусматривать некоторый маркер завершения сообщения. Этот маркер дописывается в конец сообщения на этапе предварительной обработки, до поступления сообщения в стеганокодер. При извлечении сообщения из контейнера, обнаружение маркера позволяет прервать обработку и не анализировать оставшуюся часть контейнера.\r\nНа практике, стеганографические системы, реализующие рассмотренный и подобные методы, осуществляют предварительную обработку пустого контейнера. В ходе предобработки из текста контейнера удаляются «лишние» пробелы, которые могли оказаться в конце предложений на этапе создания контейнера: это может произойти из-за особенностей конкретного текстового редактора, системы распознавания сканированного текста, или действий человека, набиравшего текст.\r\nВ данной лабораторной работе необходимо программно реализовать алгоритмы прямого и обратного стеганографического преобразования. \r\nПустой и заполненный текстовые контейнеры должны задаваться пользователем в виде файлов либо через графический интерфейс – стандартный диалог выбора файла и/или поле ввода имени файла, либо через параметры командной строки. Выбор вида интерфейса (графический или командной строки) – на усмотрение студента, выполняющего работу. Аналогичным образом задается скрываемое сообщение. \r\nПеред прямым стеганографическим преобразованием должна выполняться предварительная обработка пустого контейнера для замены двух и более пробелов на один пробел после окончания предложения, а также добавление к сообщению маркера окончания. Маркер окончания выбирается студентом самостоятельно таким образом, чтобы выбранный маркер не являлся частью исходного сообщения. До начала стеганографического преобразования предобработчик должен выводить пользователю информацию о максимальной емкости контейнера и возможности размещения заданного сообщения в выбранном контейнере.\r\nДля простоты реализации можно принять, что сообщение начинает размещаться в контейнере с первой возможной позиции (то есть, после первого же предложения) и размещается последовательно; при этом стеганографический ключ отсутствует.");
        private ISetKeyForm _form;

        public MainWindow()
        {
            InitializeComponent();
            changeLabComboBox.SelectedIndex = 0;
            currentLab = encBaseLab1;
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            keyErrorLabel.Visible = false;

            try
            {
                var encryptResult = currentLab.Encrypt(sourceMsgTextBox.Text);
                sourceMsgLabel.ForeColor = Color.Black;
                encryptMsgLabel.ForeColor = Color.Black;
                encryptMsgTextBox.Text = encryptResult.Replace("\0", string.Empty);
            }
            catch (Exception ex)
            {
                sourceMsgLabel.ForeColor = Color.Red;
                if (ex.Message != "")
                {
                    sourceMsgTextBox.Text = ex.Message;
                }
            }
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            keyErrorLabel.Visible = false;

            try
            {
                var decryptResult = currentLab.Decrypt(encryptMsgTextBox.Text);
                sourceMsgLabel.ForeColor = Color.Black;
                encryptMsgLabel.ForeColor = Color.Black;
                sourceMsgTextBox.Text = decryptResult.Replace("\0", string.Empty);
            }
            catch (Exception ex)
            {
                encryptMsgLabel.ForeColor = Color.Red;
            }
        }

        private void EditKeyButton_Click(object sender, EventArgs e)
        {
            keyErrorLabel.Visible = false;

            if (_form == null)
                _form = new SetKeyForm();

            _form.SetCurrentLab(currentLab);
            
            if (_form.ShowDialog(this) == DialogResult.OK)
            {
                var newkey = _form.Key;
                try
                {
                    currentLab.SetKey(newkey);
                }
                catch (Exception ex)
                {
                    keyErrorLabel.Visible = true;
                }
            }
        }

        private void ChangeLabComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (changeLabComboBox.SelectedIndex)
            {
                case 0:
                    currentLab = encBaseLab1;
                    break;

                case 1:
                    currentLab = encBaseLab2;
                    break;

                case 2:
                    currentLab = encBaseLab3;
                    break;

                case 3:
                    currentLab = encBaseLab4;
                    break;

               case 4:
                    currentLab = encBaseLab5;
                    break;

                case 5:
                    currentLab = encBaseLab6;
                    break;

                case 6:
                    currentLab = encBaseLab7;
                    break;

                case 7:
                    currentLab = encBaseLab8;
                    break;

                case 8:
                    currentLab = encBaseLab9;
                    break;
            }

            sourceMsgTextBox.Text = "";
            encryptMsgTextBox.Text = "";
            sourceMsgLabel.ForeColor = Color.Black;
            encryptMsgLabel.ForeColor = Color.Black;
            keyErrorLabel.Visible = false;
        }

        private void MsgTextBox_TextChanged(object sender, EventArgs e)
        {
            keyErrorLabel.Visible = false;

            if (sender is not TextBox textBox)
                return;

            var text = textBox.Text;

            var validText = new String(text.Where(currentLab.IsCharInDict).ToArray());
            var cursorPos = validText.Length == text.Length ? textBox.SelectionStart : textBox.SelectionStart - 1;

            textBox.Text = validText;
            textBox.SelectionStart = cursorPos;
        }
    }
}
