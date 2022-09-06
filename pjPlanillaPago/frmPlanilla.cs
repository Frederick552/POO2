using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjPlanillaPago
{
    public partial class frmPlanilla : Form
    {
        public frmPlanilla()
        {
            InitializeComponent();
        }

        private void frmPlanilla_Load(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
            mostrarFecha();
            mostrarMesActual();
        }


        // IMPLEMENTACIÓN DE MÉTODOS
        // 1. Mostrar la fecha Actual
        private void mostrarFecha()
        {
            lblFechaActual.Text = DateTime.Now.ToShortDateString();
        }

        private void mostrarMesActual()
        {
            txtMes.Text = DateTime.Now.Month.ToString();
        }

        private void dtFechaIngreso_ValueChanged(object sender, EventArgs e)
        {
            txtAños.Text = (DateTime.Now.Year - dtFechaIngreso.Value.Year).ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtEmpleado.Clear();
            dtFechaIngreso.Value = DateTime.Now;
            cboCargo.Text = "(Seleccione cargo)";
            txtMes.Clear();
            txtAños.Clear();

            lblBasico.Text = (0).ToString("C");
            lblGratificacion.Text = (0).ToString("C");
            lblComision.Text = (0).ToString("C");
            lblCooperativa.Text = (0).ToString("C");
            lblSeguro.Text = (0).ToString("C");

            lblTotalAportaciones.Text = (0).ToString("C");
            lblTotalDescuentos.Text = (0).ToString("C");
            lblTotalIngresos.Text = (0).ToString("C");
            lblTotal.Text = (0).ToString("C");

            txtEmpleado.Focus();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            // Capturando datos
            string empleado = txtEmpleado.Text;
            string cargo = cboCargo.Text;
            DateTime fechaIngreso = dtFechaIngreso.Value;
            int años = int.Parse(txtAños.Text);

            // Objeto de la clase Planilla
            Planilla objP = new Planilla();
            objP.empleado = empleado;
            objP.cargo = cargo;
            objP.fechaIngreso = fechaIngreso;
            objP.años = años;

            // Realizando la impresión de valores
            txtMes.Text = objP.mesConsultado();
            lblBasico.Text = objP.DeterminaBasico().ToString("C");
            lblGratificacion.Text = objP.DeterminaGratificacion().ToString("C");
            lblComision.Text = objP.DeterminaComision().ToString("C");
            lblCooperativa.Text = objP.DeterminaDescuentoCooperativa().ToString("C");
            lblSeguro.Text = objP.DeterminaAportacionSeguro().ToString("C");

            lblTotalIngresos.Text = objP.CalculaTotalIngresos().ToString("C");
            lblTotalDescuentos.Text = objP.CalculaTotalDescuentos().ToString("C");
            lblTotalAportaciones.Text = objP.CalculaTotalAportaciones().ToString("C");

            lblTotal.Text = objP.DeterminaNeto().ToString("C");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Está seguro de salir?", "Planilla",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
                this.Close();
        }
    }
}
