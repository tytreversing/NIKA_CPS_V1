using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NIKA_CPS_V1
{
    public partial class CalibrationCharts : Form
    {
        public CalibrationCharts(CalibrationData calData)
        {
            InitializeComponent();
            chPowers.Series["srVHFPow0"].LegendText = "Мощность 100 мВт";
            chPowers.Series["srVHFPow1"].LegendText = "Мощность 250 мВт";
            chPowers.Series["srVHFPow2"].LegendText = "Мощность 500 мВт";
            chPowers.Series["srVHFPow3"].LegendText = "Мощность 750 мВт";
            chPowers.Series["srVHFPow4"].LegendText = "Мощность 1 Вт";
            chPowers.Series["srVHFPow5"].LegendText = "Мощность 5 Вт";
            chPowers.Series["srVHFPow6"].LegendText = "Мощность 10 Вт";
            chPowers.Series["srVHFPow7"].LegendText = "Мощность 25 Вт";
            chPowers.Series["srVHFPow8"].LegendText = "Мощность 40 Вт";
            for (int i = 0; i < 5; i++)
            {
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
