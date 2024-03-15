using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace demka
{
    public class ReportGenerator
    {
        public void GenerateReport(List<string> data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    string dest = saveFileDialog.FileName;

                    try
                    {
                        Document pdfDocument = new Document();
                        Page page = pdfDocument.Pages.Add();

                        TextFragment title = new TextFragment("Отчет по заявке");
                        title.TextState.FontSize = 18;
                        title.TextState.FontStyle = FontStyles.Bold;
                        page.Paragraphs.Add(title);

                        // Добавление данных из списка в отчет
                        foreach (string item in data)
                        {
                            TextFragment text = new TextFragment(item);
                            page.Paragraphs.Add(text);
                        }

                        pdfDocument.Save(dest);
                        MessageBox.Show("Отчет успешно создан и сохранен.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при создании отчета: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
