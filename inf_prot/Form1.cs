using inf_prot;

namespace inf_prot
{
    public partial class MainWindow : Form
    {
        IEncBaseLab currentLab;
        private EncBaseLab1 encBaseLab1 = new EncBaseLab1("01", 5);
        private EncBaseLab2 encBaseLab2 = new EncBaseLab2(
            "��������������������������������abcdefghijklmnopqrstuvwxyz", 15);
        private EncBaseLab3 encBaseLab3 = new EncBaseLab3(5, 51, 13);
        private EncBaseLab4 encBaseLab4 = new EncBaseLab4("ABCD ABCD ABCD ABCD");
        private EncBaseLab5 encBaseLab5 = new EncBaseLab5("ABCD ABCD ABCD ABCD", "ABCD ABCD ABCD ABCD");
        private EncBaseLab6 encBaseLab6 = new EncBaseLab6("ABCD ABCD ABCD ABCD", "ABCD ABCD ABCD ABCD", 30);
        private EncBaseLab7 encBaseLab7 = new EncBaseLab7("ABCD ABCD ABCD ABCD", "ABCD ABCD ABCD ABCD", 30);
        private EncBaseLab8 encBaseLab8 = new EncBaseLab8("�������� ������� ������������� � ���������� ����������� ����� ������������������ ������� ��������� � ������ ������.\r\n����� ��������� ��������� ����� ������������� ������� �� ���������� ������ ��� ���� �������� ����� ������� ������� ���������� �����������. ��� ����, ������, ��������� �������� ���������� ��� �1�, � ������� �������� � ��� �0�. ��������� ���������� ����������� ����� ������� �����, �������������� �/��� ��������������� �����, ����� ������� ������� ������. ���������� ��������� � �������� ������� ������������ � ���������-����� ����� ���������� ���������������� ������� ���� ����� �������� ����� ������� �����������. \r\n��������� ��������� ����� ����������� ������� �����, ��� ���������, �������� ������������������� ������ ������ ��������������� ��������� ������ ���������� ���������. ���� ������ ������������ � ����� ��������� �� ����� ��������������� ���������, �� ����������� ��������� � ������������. ��� ���������� ��������� �� ����������, ����������� ������� ��������� �������� ��������� � �� ������������� ���������� ����� ����������.\r\n�� ��������, ������������������ �������, ����������� ������������� � �������� ������, ������������ ��������������� ��������� ������� ����������. � ���� ������������� �� ������ ���������� ��������� ������� �������, ������� ����� ��������� � ����� ����������� �� ����� �������� ����������: ��� ����� ��������� ��-�� ������������ ����������� ���������� ���������, ������� ������������� �������������� ������, ��� �������� ��������, ����������� �����.\r\n� ������ ������������ ������ ���������� ���������� ����������� ��������� ������� � ��������� ������������������� ��������������. \r\n������ � ����������� ��������� ���������� ������ ���������� ������������� � ���� ������ ���� ����� ����������� ��������� � ����������� ������ ������ ����� �/��� ���� ����� ����� �����, ���� ����� ��������� ��������� ������. ����� ���� ���������� (����������� ��� ��������� ������) � �� ���������� ��������, ������������ ������. ����������� ������� �������� ���������� ���������. \r\n����� ������ ������������������ ��������������� ������ ����������� ��������������� ��������� ������� ���������� ��� ������ ���� � ����� �������� �� ���� ������ ����� ��������� �����������, � ����� ���������� � ��������� ������� ���������. ������ ��������� ���������� ��������� �������������� ����� �������, ����� ��������� ������ �� ������� ������ ��������� ���������. �� ������ ������������������� �������������� �������������� ������ �������� ������������ ���������� � ������������ ������� ���������� � ����������� ���������� ��������� ��������� � ��������� ����������.\r\n��� �������� ���������� ����� �������, ��� ��������� �������� ����������� � ���������� � ������ ��������� ������� (�� ����, ����� ������� �� �����������) � ����������� ���������������; ��� ���� ������������������ ���� �����������.");
        private EncBaseLab9 encBaseLab9 = new EncBaseLab9("�������� ������� ������������� � ���������� ����������� ����� ������������������ ������� ��������� � ������ ������.\r\n����� ��������� ��������� ����� ������������� ������� �� ���������� ������ ��� ���� �������� ����� ������� ������� ���������� �����������. ��� ����, ������, ��������� �������� ���������� ��� �1�, � ������� �������� � ��� �0�. ��������� ���������� ����������� ����� ������� �����, �������������� �/��� ��������������� �����, ����� ������� ������� ������. ���������� ��������� � �������� ������� ������������ � ���������-����� ����� ���������� ���������������� ������� ���� ����� �������� ����� ������� �����������. \r\n��������� ��������� ����� ����������� ������� �����, ��� ���������, �������� ������������������� ������ ������ ��������������� ��������� ������ ���������� ���������. ���� ������ ������������ � ����� ��������� �� ����� ��������������� ���������, �� ����������� ��������� � ������������. ��� ���������� ��������� �� ����������, ����������� ������� ��������� �������� ��������� � �� ������������� ���������� ����� ����������.\r\n�� ��������, ������������������ �������, ����������� ������������� � �������� ������, ������������ ��������������� ��������� ������� ����������. � ���� ������������� �� ������ ���������� ��������� ������� �������, ������� ����� ��������� � ����� ����������� �� ����� �������� ����������: ��� ����� ��������� ��-�� ������������ ����������� ���������� ���������, ������� ������������� �������������� ������, ��� �������� ��������, ����������� �����.\r\n� ������ ������������ ������ ���������� ���������� ����������� ��������� ������� � ��������� ������������������� ��������������. \r\n������ � ����������� ��������� ���������� ������ ���������� ������������� � ���� ������ ���� ����� ����������� ��������� � ����������� ������ ������ ����� �/��� ���� ����� ����� �����, ���� ����� ��������� ��������� ������. ����� ���� ���������� (����������� ��� ��������� ������) � �� ���������� ��������, ������������ ������. ����������� ������� �������� ���������� ���������. \r\n����� ������ ������������������ ��������������� ������ ����������� ��������������� ��������� ������� ���������� ��� ������ ���� � ����� �������� �� ���� ������ ����� ��������� �����������, � ����� ���������� � ��������� ������� ���������. ������ ��������� ���������� ��������� �������������� ����� �������, ����� ��������� ������ �� ������� ������ ��������� ���������. �� ������ ������������������� �������������� �������������� ������ �������� ������������ ���������� � ������������ ������� ���������� � ����������� ���������� ��������� ��������� � ��������� ����������.\r\n��� �������� ���������� ����� �������, ��� ��������� �������� ����������� � ���������� � ������ ��������� ������� (�� ����, ����� ������� �� �����������) � ����������� ���������������; ��� ���� ������������������ ���� �����������.");
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
