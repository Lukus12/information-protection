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
        private EncBaseLab8 encBaseLab8 = new EncBaseLab8("Российская империя — государство, существовавшее в период с 1721 года до Февральской революции 1917 года и провозглашения Временным правительством в сентябре того же года республики. В 1913 году Российская империя занимала вторую по площади территорию в мире (после Британской империи). Крупнейшая континентальная империя Нового времени располагалась в Восточной и Северной Европе, Северной и Средней Азии, а также в Закавказье. До 1867 года она имела владения и в Северной Америке.\r\n\r\nИмперия была провозглашена 22 октября (2 ноября) 1721 года по окончании Северной войны. По прошению сенаторов Пётр I принял титулы Императора Всероссийского и Отца Отечества. Столицей Российской империи с 1721 по 1728 год был Санкт-Петербург. Затем столицей стала Москва, которая сохраняла почётное звание «первопрестольной» столицы.\r\nПосле 1732 года столицей снова стал Санкт-Петербург. В 1914—1917 годах город назывался Петроград. Во главе государства стоял император из династии Романовых. До 17 (30) октября 1905 года император обладал абсолютной властью. Исключением был краткий период в начале правления императрицы Анны Иоанновны. Тогда действовали навязанные ей членами Верховного тайного совета кондиции, которые сильно ограничивали её власть.\r\n\r\nРоссийская империя сыграла важную роль в мировой истории. Она была одной из крупнейших держав своего времени. Её влияние ощущалось как в Европе, так и в Азии. Империя активно участвовала в международных конфликтах и дипломатических переговорах.\r\nКультурное наследие Российской империи огромно. В этот период были созданы великие произведения литературы, музыки и искусства. Империя способствовала развитию науки и образования. Она стала родиной многих выдающихся деятелей культуры и науки.\r\nИстория Российской империи полна противоречий. С одной стороны, это было время расцвета и мощи государства. С другой стороны, в империи существовали серьёзные социальные проблемы. Они стали одной из причин Февральской революции 1917 года.");
        private EncBaseLab9 encBaseLab9 = new EncBaseLab9("Российская империя — государство, существовавшее в период с 1721 года до Февральской революции 1917 года и провозглашения Временным правительством в сентябре того же года республики. В 1913 году Российская империя занимала вторую по площади территорию в мире (после Британской империи). Крупнейшая континентальная империя Нового времени располагалась в Восточной и Северной Европе, Северной и Средней Азии, а также в Закавказье. До 1867 года она имела владения и в Северной Америке.\r\n\r\nИмперия была провозглашена 22 октября (2 ноября) 1721 года по окончании Северной войны. По прошению сенаторов Пётр I принял титулы Императора Всероссийского и Отца Отечества. Столицей Российской империи с 1721 по 1728 год был Санкт-Петербург. Затем столицей стала Москва, которая сохраняла почётное звание «первопрестольной» столицы.\r\nПосле 1732 года столицей снова стал Санкт-Петербург. В 1914—1917 годах город назывался Петроград. Во главе государства стоял император из династии Романовых. До 17 (30) октября 1905 года император обладал абсолютной властью. Исключением был краткий период в начале правления императрицы Анны Иоанновны. Тогда действовали навязанные ей членами Верховного тайного совета кондиции, которые сильно ограничивали её власть.\r\n\r\nРоссийская империя сыграла важную роль в мировой истории. Она была одной из крупнейших держав своего времени. Её влияние ощущалось как в Европе, так и в Азии. Империя активно участвовала в международных конфликтах и дипломатических переговорах.\r\nКультурное наследие Российской империи огромно. В этот период были созданы великие произведения литературы, музыки и искусства. Империя способствовала развитию науки и образования. Она стала родиной многих выдающихся деятелей культуры и науки.\r\nИстория Российской империи полна противоречий. С одной стороны, это было время расцвета и мощи государства. С другой стороны, в империи существовали серьёзные социальные проблемы. Они стали одной из причин Февральской революции 1917 года..");
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

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
