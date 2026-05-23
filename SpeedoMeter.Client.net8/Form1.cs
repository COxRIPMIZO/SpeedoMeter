namespace SpeedoMeter.Client.net8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

            timer.Tick += (sender, e) =>
            {
                speedometerControl1.Value += 1;
            };
            timer.Interval = 100; // Set the interval to 1 second (1000 milliseconds)
            timer.Start();
        }
    }
}
