using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGSW_POS
{
    public partial class FoodButton : UserControl
    {


        public FoodButton()
        {
            InitializeComponent();
        }

        public string setnamelabel
        {
            set { this.namelabel.Text = value; }
        }

        public string setpricelabel
        {
            set { this.pricelabel.Text = value; }
        }

        public string setpicture
        {
            set { this.picturebtn.BackgroundImage = System.Drawing.Image.FromFile(value); }
        }
    }
}
