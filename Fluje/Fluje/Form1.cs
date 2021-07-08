using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fluje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            decimal inversion = Decimal.Parse(txtInversion.Text);
            int plazo = Int32.Parse(txtPlazo.Text);
            float tasa = float.Parse(txtTasa.Text);
            float inflacion = float.Parse(txtInflacion.Text);
            decimal vs = Decimal.Parse(txtVS.Text);
            float primer_ingreso = float.Parse(txtPrimerIngreso.Text);

            AddHeaders(plazo, vs);

            dgvFlujo.Rows[0].Cells[1].Value = inversion;
            dgvFlujo.Rows[1].Cells[1].Value = primer_ingreso;
            calculoIngresos(primer_ingreso, inflacion, plazo);

            calculoDepreciacion(inversion, plazo);
            calculoUAI();

        }

        private void calculoUAI()
        {
            for (int  i = 2;  i < dgvFlujo.ColumnCount;  i++)
            {
                float ingresos = float.Parse(dgvFlujo.Rows[1].Cells[i].Value.ToString());
                float depreciacion = float.Parse(dgvFlujo.Rows[3].Cells[i].Value.ToString());
                float vs = float.Parse(dgvFlujo.Rows[4].Cells[i].Value.ToString());
                dgvFlujo.Rows[5].Cells[i].Value = ingresos - depreciacion + vs;
            }
        }

        private void calculoDepreciacion(decimal inversion, int plazo)
        {
            for (int i = 2; i < dgvFlujo.ColumnCount; i++)
            {
                dgvFlujo.Rows[3].Cells[i].Value = inversion / plazo;
            }
        }

        private void calculoIngresos(float primer_ingreso, float inflacion, int plazo)
        {
            float celda = 0;
            for (int i = 2; i < dgvFlujo.ColumnCount; i++)
            {
                

                if (i==2)
                {
                    dgvFlujo.Rows[1].Cells[i].Value = primer_ingreso + (primer_ingreso * (inflacion / 100));
                    celda = float.Parse(dgvFlujo.Rows[1].Cells[2].Value.ToString());
                    continue;
                }
                dgvFlujo.Rows[1].Cells[i].Value = celda + (primer_ingreso * (inflacion / 100));
                celda = float.Parse(dgvFlujo.Rows[1].Cells[i].Value.ToString());
            }
        }

        public void AddHeaders(int plazo, decimal vs) {
            dgvFlujo.ColumnCount = plazo + 2;

            for (int i = 0; i < plazo + 2; i++)
            {
                if (i == 0)
                {
                    dgvFlujo.Columns[0].Name = "";


                    continue;
                }
                dgvFlujo.Columns[i].Name = " " + (i - 1);
            }

            dgvFlujo.RowCount = 10;
            dgvFlujo.Rows[0].Cells[0].Value = "Inversión";
            dgvFlujo.Rows[1].Cells[0].Value = "Ingresos";
            dgvFlujo.Rows[2].Cells[0].Value = "Egresos";
            dgvFlujo.Rows[3].Cells[0].Value = "Depreciación";
            dgvFlujo.Rows[4].Cells[0].Value = "V.S";
            dgvFlujo.Rows[5].Cells[0].Value = "UAI";
            dgvFlujo.Rows[6].Cells[0].Value = "IR";
            dgvFlujo.Rows[7].Cells[0].Value = "UDI";
            dgvFlujo.Rows[8].Cells[0].Value = "Depreciación";
            dgvFlujo.Rows[9].Cells[0].Value = "FNE";

            for (int i = 2; i < dgvFlujo.ColumnCount; i++)
            {
                dgvFlujo.Rows[4].Cells[i].Value = vs;
            }
        }
    }
}
