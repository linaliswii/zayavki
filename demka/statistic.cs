using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Aspose.Pdf.Text;

namespace demka
{
    public partial class statistic : Form
    {
        public statistic()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string equipmentName = textBoxEquipmentName.Text;
            string quantity = textBoxQuantity.Text;
            string cost = textBoxCost.Text;
            string comment = textBoxComment.Text;

            // Создаем экземпляр класса ReportGenerator
            ReportGenerator reportGenerator = new ReportGenerator();

            // Создаем список для хранения данных и добавляем в него строки
            List<string> data = new List<string>();
            data.Add("Наименование: " + equipmentName);
            data.Add("Количество: " + quantity);
            data.Add("Стоимость: " + cost + " рублей");
            data.Add("Комментарий: " + comment );

            // Вызываем метод GenerateReport, передавая ему список данных
            reportGenerator.GenerateReport(data);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string resourceName = textBoxEquipmentName.Text;
            int quantity = Convert.ToInt32(textBoxQuantity.Text);
            decimal cost = Convert.ToDecimal(textBoxCost.Text);

            ListViewItem item = new ListViewItem(resourceName);
            item.SubItems.Add(quantity.ToString());
            item.SubItems.Add(cost.ToString());

            listView1.Items.Add(item);
        }

    }
}
