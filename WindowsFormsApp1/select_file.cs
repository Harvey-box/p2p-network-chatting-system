using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class select_file : Form
    {
        string str;
        public select_file()
        {
            InitializeComponent();
            this.Visible = false;
            label1.DragEnter += act;
            label1.DragDrop += get_add;
        }


        private void act(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }



        private void get_add(object sender, DragEventArgs e)
        {
            str = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            Console.WriteLine(str);
            label1.Text = "Here is a file";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            chat_private form = (chat_private)this.Owner;
            form.adr = str;
            this.Close();
        }
    }
}
