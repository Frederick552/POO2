using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;

namespace pjBoletaVenta
{
    public partial class frmBoleta : Form
    {
        // Variables globales
        int num;
        ListViewItem item;

        // Objeto de la clase Boleta
        Boleta objB = new Boleta();

        public frmBoleta()
        {
            InitializeComponent();
        }

        private void frmBoleta_Load(object sender, EventArgs e)
        {
            num++;
            lblNumero.Text = num.ToString("D5");
            txtFecha.Text = DateTime.Now.ToShortDateString();
            lblTotal.Text = (0).ToString("C");
        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            objB.Producto = cboProducto.Text;
            txtPrecio.Text = objB.DeterminaPrecio().ToString("C");
        }

        // Capturar los datos del formulario
        private void CapturaDatos()
        {
            objB.Numero = int.Parse(lblNumero.Text);
            objB.Cliente = txtCliente.Text;
            objB.Direccion = txtDireccion.Text;
            objB.Fecha = DateTime.Parse(txtFecha.Text);
            objB.Cedula = txtCedula.Text;
            objB.Producto = cboProducto.Text;
            objB.Cantidad = int.Parse(txtCantidad.Text);
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            if (Valida() == "")
            {
                // Capturar los datos
                CapturaDatos();

                // Determinar los cálculos de la aplicación
                double precio = objB.DeterminaPrecio();
                double importe = objB.Calculalmporte();

                // Imprimir el detalle de la venta
                ImprimirDetalle(precio, importe);

                // Imprimir el total acumulado
                lblTotal.Text = DeterminaTotal().ToString("C");
            }
            else
                MessageBox.Show("El error se encuentra en " + Valida());
        }

        // Método que calcula el monto acumulado de los importes
        private double DeterminaTotal()
        {
            double total = 0;
            for(int i = 0; i < lvDetalle.Items.Count; i++)
            {
                total += double.Parse(lvDetalle.Items[i].SubItems[3].Text);
            }
            return total;
        }

        private void ImprimirDetalle(double precio, double importe)
        {
            ListViewItem fila = new ListViewItem(objB.Cantidad.ToString());
            fila.SubItems.Add(objB.Producto);
            fila.SubItems.Add(precio.ToString("0.00"));
            fila.SubItems.Add(importe.ToString("0.00"));
            lvDetalle.Items.Add(fila);
        }


        // Validar el ingreso de los datos
        private string Valida()
        {
            if(txtCliente.Text.Trim().Length == 0)
            {
                txtCliente.Focus();
                return "nombre del cliente";
            }
            else if (txtDireccion.Text.Trim().Length == 0)
            {
                txtDireccion.Focus();
                return "dirección del cliente";
            }
            else if (txtCedula.Text.Trim().Length == 0)
            {
                txtCedula.Focus();
                return "cédula del cliente";
            }
            else if(cboProducto.SelectedIndex == -1)
            {
                cboProducto.Focus();
                return "descripción del producto";
            }
            else if (txtCantidad.Text.Trim().Length == 0)
            {
                txtCantidad.Focus();
                return "cantidad comprada";
            }
            return "";
        }

        private void lvDetalle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            item = lvDetalle.GetItemAt(e.X, e.Y);
            string producto = lvDetalle.Items[item.Index].SubItems[1].Text;
            DialogResult r = MessageBox.Show("¿Está seguro de eliminar el producto " +
                                             "> " + producto + "?", "Boleta",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(r == DialogResult.Yes)
            {
                lvDetalle.Items.Remove(item);
                lblTotal.Text = DeterminaTotal().ToString("C");
                MessageBox.Show("¡Detalle eliminado correctamente!");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ListViewItem fila = new ListViewItem(int.Parse(lblNumero.Text).ToString("D5"));
            fila.SubItems.Add(txtFecha.Text);
            fila.SubItems.Add(TotalCantidad().ToString("0.00"));
            fila.SubItems.Add(DeterminaTotal().ToString("C"));
            lvEstadisticas.Items.Add(fila);
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            num++;
            lblNumero.Text = num.ToString("D5");
            txtCliente.Clear();
            txtDireccion.Clear();
            txtCedula.Clear();
            cboProducto.Text = "(Seleccione)";
            txtPrecio.Clear();
            txtCantidad.Clear();
            lblTotal.Text = (0).ToString("C");
            lvDetalle.Items.Clear();
        }

        // Total de productos por boleta
        private int TotalCantidad()
        {
            int total = 0;
            for (int i = 0; i < lvDetalle.Items.Count; i++)
            {
                total += int.Parse(lvDetalle.Items[i].SubItems[0].Text);
            }
            return total;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Está seguro de salir?","Boleta",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(r == DialogResult.Yes)
                this.Close();
        }
    }
}
