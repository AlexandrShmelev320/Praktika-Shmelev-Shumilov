using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika123
{
    public partial class Form1 : Form
    {
        Bitmap pic;
        Bitmap pic1;
        string mode;
        int x1, y1;
        int xclick, yclick;

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
        SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4;    //По умолчанию будет выбрано последнее расширение*.png
            SaveDlg.ShowDialog();

            if (SaveDlg.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();

                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg); break;

                    case 2:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Bmp); break;

                    case 3:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Gif); break;

                    case 4:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Png); break;
                }
                fs.Close();
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
         openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                pic = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = pic;
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
          if (pictureBox1.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка?", "Предупреждение", MessageBoxButtons.YesNoCancel);

                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: СохранитьToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                }
            }
            pictureBox1.Image = null;
            pic = new Bitmap(1000, 1000);
            pic1 = new Bitmap(1000, 1000);
        }

        public Form1()
        {
            mode = "Линия";
            InitializeComponent();
            pic = new Bitmap(1000, 1000);
            pic1 = new Bitmap(1000, 1000);
            SolidBrush b = new SolidBrush(Color.White);
            Graphics.FromImage(pic).FillRectangle(b, 0, 0, pic.Width, pic.Height);
            Graphics.FromImage(pic1).FillRectangle(b, 0, 0, pic1.Width, pic1.Height);
            x1 = y1 = 0;
            pictureBox1.Image = pic;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen p;
            p = new Pen(button4.BackColor, trackBar1.Value);
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics g;
            g = Graphics.FromImage(pic);

            Graphics g1;
            g1 = Graphics.FromImage(pic1);
            if (e.Button == MouseButtons.Left)
            {
                if (mode == "Линия")
                {
                    g.DrawLine(p, x1, y1, e.X, e.Y);
                }
                if (mode == "Прямоугольник")
                {
                    g1.Clear(Color.White);
                    g1.DrawRectangle(p, xclick, yclick, Math.Abs(e.X - xclick), e.Y - yclick);
                }
                if (mode == "Овал")
                {
                    g1.Clear(Color.White);
                    g1.DrawEllipse(p, xclick, yclick, e.X - xclick, e.Y - yclick);
                }
                g.DrawImage(pic, 0, 0);

                pictureBox1.Image = pic;
            }
            x1 = e.X;
            y1 = e.Y;

        }

    }
}
