using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NIKA_CPS_V1
{
    public partial class CalibrationCharts : Form
    {

        CalibrationData calData;

        private System.Windows.Forms.ToolTip toolTip;
        public CalibrationCharts()
        {
            calData = CalibrationForm.getActualCals();
            InitializeComponent();
            InitializeToolTip();
        }

        private void InitializeToolTip()
        {
            toolTip = new System.Windows.Forms.ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 100;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;

            // Подписываемся на события мыши
            chPowers.MouseMove += Chart_MouseMove;
            chPowers.MouseLeave += Chart_MouseLeave;
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            // Получаем результат hit test
            HitTestResult result = chPowers.HitTest(e.X, e.Y);

            // Проверяем, попали ли мы на точку данных
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // Получаем точку данных
                DataPoint point = result.Series.Points[result.PointIndex];

                // Формируем информацию о точке
                string info = $"{point.YValues[0]}";

                // Если есть дополнительные значения
                if (point.YValues.Length > 1)
                {
                    info += $"\n{point.YValues[1]}";
                }

                // Отображаем ToolTip
                toolTip.Show(info, chPowers, e.X + 10, e.Y + 10);
            }
            
            else
            {
                // Скрываем ToolTip, если мышь не над точкой
                toolTip.Hide(chPowers);
            }
        }


        private void Chart_MouseLeave(object sender, EventArgs e)
        {
            // Скрываем ToolTip при уходе мыши с графика
            toolTip.Hide(chPowers);
        }


        private void CalibrationCharts_Activated(object sender, EventArgs e)
        {
            calData = CalibrationForm.getActualCals();
            foreach (Series series in chPowers.Series)
            {
                series.Points.Clear();

            }
            chPowers.ChartAreas["caVHF"].AxisX.CustomLabels.Clear();
            chPowers.ChartAreas["caUHF"].AxisX.CustomLabels.Clear();
            chPowers.Series["srVHFPow0"].LegendText = "Мощность 100 мВт";
            chPowers.Series["srVHFPow1"].LegendText = "Мощность 250 мВт";
            chPowers.Series["srVHFPow2"].LegendText = "Мощность 500 мВт";
            chPowers.Series["srVHFPow3"].LegendText = "Мощность 750 мВт";
            chPowers.Series["srVHFPow4"].LegendText = "Мощность 1 Вт";
            chPowers.Series["srVHFPow5"].LegendText = "Мощность 5 Вт";
            chPowers.Series["srVHFPow6"].LegendText = "Мощность 10 Вт";
            chPowers.Series["srVHFPow7"].LegendText = "Мощность 25 Вт";
            chPowers.Series["srVHFPow8"].LegendText = "Мощность 40 Вт";
            double[] labelPositionsVHF = new double[] { 0, 1, 2, 3, 4 };
            double[] labelPositionsUHF = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < 5; i++)
            {
                string v = CalibrationForm.getVHFFreq(i);
                double position = labelPositionsVHF[i];
                CustomLabel label = new CustomLabel(position - 0.5, position + 0.5, v, 0, LabelMarkStyle.Box);
                chPowers.ChartAreas["caVHF"].AxisX.CustomLabels.Add(label);
                chPowers.Series["srVHFPow0"].Points.AddXY(i, calData.GetPowerVHF(0, i));
                chPowers.Series["srVHFPow1"].Points.AddXY(i, calData.GetPowerVHF(1, i));
                chPowers.Series["srVHFPow2"].Points.AddXY(i, calData.GetPowerVHF(2, i));
                chPowers.Series["srVHFPow3"].Points.AddXY(i, calData.GetPowerVHF(3, i));
                chPowers.Series["srVHFPow4"].Points.AddXY(i, calData.GetPowerVHF(4, i));
                chPowers.Series["srVHFPow5"].Points.AddXY(i, calData.GetPowerVHF(5, i));
                chPowers.Series["srVHFPow6"].Points.AddXY(i, calData.GetPowerVHF(6, i));
                chPowers.Series["srVHFPow7"].Points.AddXY(i, calData.GetPowerVHF(7, i));
                chPowers.Series["srVHFPow8"].Points.AddXY(i, calData.GetPowerVHF(8, i));
            }
            chPowers.Series["srUHFPow0"].LegendText = "Мощность 100 мВт";
            chPowers.Series["srUHFPow1"].LegendText = "Мощность 250 мВт";
            chPowers.Series["srUHFPow2"].LegendText = "Мощность 500 мВт";
            chPowers.Series["srUHFPow3"].LegendText = "Мощность 750 мВт";
            chPowers.Series["srUHFPow4"].LegendText = "Мощность 1 Вт";
            chPowers.Series["srUHFPow5"].LegendText = "Мощность 5 Вт";
            chPowers.Series["srUHFPow6"].LegendText = "Мощность 10 Вт";
            chPowers.Series["srUHFPow7"].LegendText = "Мощность 25 Вт";
            chPowers.Series["srUHFPow8"].LegendText = "Мощность 40 Вт";
            for (int i = 0; i < 9; i++)
            {
                string v = CalibrationForm.getUHFFreq(i);
                double position = labelPositionsUHF[i];
                CustomLabel label = new CustomLabel(position - 0.5, position + 0.5, v, 0, LabelMarkStyle.Box);
                chPowers.ChartAreas["caUHF"].AxisX.CustomLabels.Add(label);
                chPowers.Series["srUHFPow0"].Points.AddXY(i, calData.GetPowerUHF(0, i));
                chPowers.Series["srUHFPow1"].Points.AddXY(i, calData.GetPowerUHF(1, i));
                chPowers.Series["srUHFPow2"].Points.AddXY(i, calData.GetPowerUHF(2, i));
                chPowers.Series["srUHFPow3"].Points.AddXY(i, calData.GetPowerUHF(3, i));
                chPowers.Series["srUHFPow4"].Points.AddXY(i, calData.GetPowerUHF(4, i));
                chPowers.Series["srUHFPow5"].Points.AddXY(i, calData.GetPowerUHF(5, i));
                chPowers.Series["srUHFPow6"].Points.AddXY(i, calData.GetPowerUHF(6, i));
                chPowers.Series["srUHFPow7"].Points.AddXY(i, calData.GetPowerUHF(7, i));
                chPowers.Series["srUHFPow8"].Points.AddXY(i, calData.GetPowerUHF(8, i));
            }
            chPowers.Show();
        }
    }
}
