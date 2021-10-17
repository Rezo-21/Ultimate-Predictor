using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string APP_NAME = "Ultimate Predictor";
        private readonly string PREDICTION_CONFIG_PATH = $"{Environment.CurrentDirectory}\\predictionsConfig.json";
        private string[] _predictions;
        private Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        #region info

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
#endregion
      
        private async void bPredict_Click(object sender, EventArgs e)
        {
            bPredict.Enabled = false;
            await Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(()=>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"{i}%";
                    }));
                    Thread.Sleep(20);
                }
            });

            var index = _random.Next(_predictions.Length);

            var prediction = _predictions[index];

            MessageBox.Show($"{prediction}!");

            progressBar1.Value = 0;
            this.Text = APP_NAME;
            bPredict.Enabled = true;
        }

        private void UpdateProgressBar(int i)
        {
            if (i == progressBar1.Maximum)
            {
                progressBar1.Maximum = i + 1;
                progressBar1.Value = i + 1;
                progressBar1.Maximum = i;
            }
            else
            {
                progressBar1.Value = i + 1;
            }

            progressBar1.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;

            try
            {
                string data = File.ReadAllText(PREDICTION_CONFIG_PATH, Encoding.GetEncoding(1251));

                _predictions = JsonConvert.DeserializeObject<string[]>(data);
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_predictions == null)
                {
                    Close();
                }
                else if (_predictions.Length == 0)
                {
                    MessageBox.Show("Error");
                    Close();
                }
            }
        }
    }
}
