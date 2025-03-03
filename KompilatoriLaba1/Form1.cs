using System;
using System.IO;
using System.Windows.Forms;

namespace KompilatoriLaba1
{
    public partial class Compiler : Form
    {
        private bool isTextChanged = false;
        private string currentFilePath = string.Empty;

        public Compiler()
        {
            InitializeComponent();
        }

        private void Compiler_Load(object sender, EventArgs e) { }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
        }

        // 🔹 Функция проверки несохранённых изменений перед важными действиями
        private bool CheckForUnsavedChanges()
        {
            if (!isTextChanged) return true; // Если изменений нет – выходим

            DialogResult result = MessageBox.Show(
                "Сохранить изменения перед продолжением?",
                "Несохранённые изменения",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                return SaveFile();
            }
            else if (result == DialogResult.No)
            {
                return true;
            }

            return false; // "Отмена" – прерываем действие
        }

        // 🔹 Функция сохранения файла
        private bool SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                return SaveFileAs(); // Если нет пути, вызываем "Сохранить как"
            }

            try
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                isTextChanged = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 🔹 Функция "Сохранить как"
        private bool SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Сохранить файл"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = saveFileDialog.FileName;
                return SaveFile();
            }

            return false; // Пользователь отменил сохранение
        }

        // 🔹 Открытие файла с проверкой изменений
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckForUnsavedChanges()) return;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Открыть текстовый файл"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentFilePath = openFileDialog.FileName;
                    richTextBox1.Text = File.ReadAllText(currentFilePath);
                    isTextChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 🔹 Создание нового файла с проверкой изменений
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckForUnsavedChanges()) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files|*.txt",
                Title = "Создать новый текстовый файл",
                FileName = "Новый файл.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Create(saveFileDialog.FileName).Close();
                    currentFilePath = saveFileDialog.FileName;
                    richTextBox1.Clear();
                    isTextChanged = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 🔹 Сохранение изменений перед выходом из программы
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckForUnsavedChanges())
            {
                Close();
            }
        }

        // 🔹 Проверка перед закрытием формы
        private void Compiler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckForUnsavedChanges())
            {
                e.Cancel = true; // Отменяем выход, если пользователь передумал
            }
        }

        // 🔹 Сохранение файла
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        // 🔹 Сохранение файла как
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        // 🔹 Функции редактирования текста
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Undo();
        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Redo();
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Cut();
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Copy();
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Paste();
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectedText = string.Empty;
        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.SelectAll();

        // 🔹 Открытие справки и информации о программе
        private void button10_Click(object sender, EventArgs e)
        {
            var AboutProgramm = new AboutProgramm();
            AboutProgramm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var Spravka = new Spravka();
            Spravka.Show();
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace KompilatoriLaba1
//{
//    public partial class Compiler : Form
//    {
//        public Compiler()
//        {
//            InitializeComponent();
//        }
//        private bool isTextChanged = false;

//        private string currentFilePath = string.Empty;
//        private void richTextBox1_TextChanged(object sender, EventArgs e)
//        {
//            isTextChanged = true;
//        }

//        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            richTextBox1.Undo(); // Отменить
//        }

//        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            richTextBox1.Redo(); // Повторить
//        }

//        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            richTextBox1.Cut(); // Вырезать
//        }

//        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            richTextBox1.Copy(); // Копировать
//        }

//        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            richTextBox1.Paste(); // Вставить
//        }

//        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            richTextBox1.SelectedText = string.Empty; // Удалить
//        }

//        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//           richTextBox1.SelectAll(); // Выделить все
//        }

//        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Close();
//        }

//        private void Compiler_Load(object sender, EventArgs e)
//        {

//        }

//        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
//            {
//                MessageBox.Show("Текстовое поле пусто. Нечего сохранять.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            SaveFileDialog saveFileDialog = new SaveFileDialog
//            {
//                Filter = "Text Files|*.txt",
//                Title = "Сохранить текст в файл"
//            };

//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
//                    MessageBox.Show("Файл успешно сохранён.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog
//            {
//                Filter = "Text Files|*.txt",
//                Title = "Открыть текстовый файл"
//            };

//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    currentFilePath = openFileDialog.FileName;
//                    richTextBox1.Text = File.ReadAllText(currentFilePath);
//                    MessageBox.Show("Файл успешно загружен.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
//            {
//                MessageBox.Show("Текстовое поле пусто. Нечего сохранять.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (string.IsNullOrEmpty(currentFilePath)) // Если файл еще не был открыт или сохранен ранее
//            {
//                SaveFileDialog saveFileDialog = new SaveFileDialog
//                {
//                    Filter = "Text Files|*.txt",
//                    Title = "Сохранить текст в файл"
//                };

//                if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                {
//                    currentFilePath = saveFileDialog.FileName;
//                }
//                else
//                {
//                    return; // Если пользователь отменил сохранение, выходим из функции
//                }
//            }

//            try
//            {
//                File.WriteAllText(currentFilePath, richTextBox1.Text);
//                MessageBox.Show("Файл успешно сохранён.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog
//            {
//                Filter = "Text Files|*.txt",
//                Title = "Создать новый текстовый файл",
//                FileName = "Новый файл.txt" // Стандартное имя файла
//            };

//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    File.Create(saveFileDialog.FileName).Close(); // Создаем пустой файл
//                    MessageBox.Show("Файл успешно создан!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка при создании файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void button10_Click(object sender, EventArgs e)
//        {
//            var AboutProgramm = new AboutProgramm();
//            AboutProgramm.Show();
//        }

//        private void button11_Click(object sender, EventArgs e)
//        {
//            var Spravka = new Spravka();
//            Spravka.Show();
//        }

//        //private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
//        //{

//        //}

//        //private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
//        //{

//        //}
//    }
//}
