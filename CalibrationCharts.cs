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
            chPowers.Series["srVHFPow0"].LegendText = "Уровень мощности 1";
            chPowers.Series["srVHFPow1"].LegendText = "Уровень мощности 2";
            chPowers.Series["srVHFPow2"].LegendText = "Уровень мощности 3";
            chPowers.Series["srVHFPow3"].LegendText = "Уровень мощности 4";
            chPowers.Series["srVHFPow4"].LegendText = "Уровень мощности 5";
            chPowers.Series["srVHFPow5"].LegendText = "Уровень мощности 6";
            chPowers.Series["srVHFPow6"].LegendText = "Уровень мощности 7";
            chPowers.Series["srVHFPow7"].LegendText = "Уровень мощности 8";
            chPowers.Series["srVHFPow8"].LegendText = "Уровень мощности 9";
            for (int i = 0; i < 5; i++)
            {
                chPowers.Series["srVHFPow0"].Points.AddXY(i, calData.PowersVHFAs2D[0][i]);
                chPowers.Series["srVHFPow1"].Points.AddXY(i, calData.PowersVHFAs2D[1][i]);
                chPowers.Series["srVHFPow2"].Points.AddXY(i, calData.PowersVHFAs2D[2][i]);
                chPowers.Series["srVHFPow3"].Points.AddXY(i, calData.PowersVHFAs2D[3][i]);
                chPowers.Series["srVHFPow4"].Points.AddXY(i, calData.PowersVHFAs2D[4][i]);
                chPowers.Series["srVHFPow5"].Points.AddXY(i, calData.PowersVHFAs2D[5][i]);
                chPowers.Series["srVHFPow6"].Points.AddXY(i, calData.PowersVHFAs2D[6][i]);
                chPowers.Series["srVHFPow7"].Points.AddXY(i, calData.PowersVHFAs2D[7][i]);
                chPowers.Series["srVHFPow8"].Points.AddXY(i, calData.PowersVHFAs2D[8][i]);
            }
            chPowers.Series["srUHFPow0"].LegendText = "Уровень мощности 1";
            chPowers.Series["srUHFPow1"].LegendText = "Уровень мощности 2";
            chPowers.Series["srUHFPow2"].LegendText = "Уровень мощности 3";
            chPowers.Series["srUHFPow3"].LegendText = "Уровень мощности 4";
            chPowers.Series["srUHFPow4"].LegendText = "Уровень мощности 5";
            chPowers.Series["srUHFPow5"].LegendText = "Уровень мощности 6";
            chPowers.Series["srUHFPow6"].LegendText = "Уровень мощности 7";
            chPowers.Series["srUHFPow7"].LegendText = "Уровень мощности 8";
            chPowers.Series["srUHFPow8"].LegendText = "Уровень мощности 9";
            for (int i = 0; i < 9; i++)
            {
                chPowers.Series["srUHFPow0"].Points.AddXY(i, calData.PowersUHFAs2D[0][i]);
                chPowers.Series["srUHFPow1"].Points.AddXY(i, calData.PowersUHFAs2D[1][i]);
                chPowers.Series["srUHFPow2"].Points.AddXY(i, calData.PowersUHFAs2D[2][i]);
                chPowers.Series["srUHFPow3"].Points.AddXY(i, calData.PowersUHFAs2D[3][i]);
                chPowers.Series["srUHFPow4"].Points.AddXY(i, calData.PowersUHFAs2D[4][i]);
                chPowers.Series["srUHFPow5"].Points.AddXY(i, calData.PowersUHFAs2D[5][i]);
                chPowers.Series["srUHFPow6"].Points.AddXY(i, calData.PowersUHFAs2D[6][i]);
                chPowers.Series["srUHFPow7"].Points.AddXY(i, calData.PowersUHFAs2D[7][i]);
                chPowers.Series["srUHFPow8"].Points.AddXY(i, calData.PowersUHFAs2D[8][i]);
            }
            chPowers.Show();
        }
    }
}
