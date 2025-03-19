using inf_prot;

namespace inf_prot
{
    public interface ISetKeyForm
    {
        string Key { get; }

        void SetCurrentLab(IEncBaseLab lab);

        DialogResult ShowDialog(IWin32Window? owner);
    }

    /// <summary>
    /// Форма редактирования ключа
    /// </summary>
    public partial class SetKeyForm : Form, ISetKeyForm
    {
        public string Key { get; private set; }

        private IEncBaseLab currentLab { get; set; }

        public SetKeyForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Key = editKeyTextBox.Text;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            currentLab.GenerateKey();
            Key = currentLab.GetKey();

            editKeyTextBox.Text = Key;
        }

        private void EditKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            var text = textBox.Text;

            var validText = new String(text.Where((ch) => currentLab.JsonEditorValidater(ch) || IsJsonSymbol(ch)).ToArray());
            var cursorPos = validText.Length == text.Length ? textBox.SelectionStart : textBox.SelectionStart - 1;

            textBox.Text = validText;
            textBox.SelectionStart = cursorPos != -1 ? cursorPos : 0;
        }

        private bool IsJsonSymbol(char ch)
        {
            switch (ch)
            {
                case ':':
                case ',':
                case '{':
                case '}':
                case '"':
                case ' ':
                case '\n':
                case '\r':
                    return true;

                default:
                    return false;
            }
        }

        public void SetCurrentLab(IEncBaseLab lab)
        {
            currentLab = lab;
            Key = currentLab.GetKey();

            editKeyTextBox.Text = Key;

            switch (lab.GetType().Name)
            {
                case "EncBaseLab1":
                case "EncBaseLab2":
                    generateButton.Enabled = true;
                    break;

                case "EncBaseLab3":
                case "EncBaseLab4":
                case "EncBaseLab5":
                    generateButton.Enabled = false;
                    break;
            }
        }
    }
}
