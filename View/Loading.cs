using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DGSW_POS.View;

namespace DGSW_POS
{
    public partial class Loading : Form
    {
        
        int tct = 1; //메인 폼으로 넘어갈 시간 카운팅을 위한 변수
        


        public Loading()
        {
            InitializeComponent();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (this.progressBar1.Maximum==this.progressBar1.Value)
            { 
                this.progressBar1.Update();
                this.timer1.Stop();
                Main m = new Main();
                m.Show();
                this.Close();
            }

            this.progressBar1.PerformStep();
        }
    }
}
