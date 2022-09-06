namespace Division
{
    public partial class frmDivision : Form
    {
        public frmDivision()
        {
            InitializeComponent();
        }

        private void frmDivision_Load(object sender, EventArgs e)
        {
            lblResultado.Text = "";
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturando datos
                int numerador = int.Parse(txtNumerador.Text);
                int denominador = int.Parse(txtDenominador.Text);

                // Realizando resultado
                double resultado = numerador / denominador;

                // Imprimiendo resultado
                lblResultado.Text = resultado.ToString();
            }
            catch (FormatException)
            {

                MessageBox.Show("Deve escribir dos enteros", "Formato de numero invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(DivideByZeroException dividebyZeroExceptionParameter)
            {
                MessageBox.Show(dividebyZeroExceptionParameter.Message,"intento de division por 0", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
    }
}