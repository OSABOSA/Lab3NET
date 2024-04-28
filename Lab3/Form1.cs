using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private Bitmap? img;

        //private void buttonLoad_Click(object sender, EventArgs e)
        //{
        //    openFileDialog1.ShowDialog();
        //    var file = openFileDialog1.FileName;
        //    if (file != null)
        //    {
        //        img = new Bitmap(file);
        //        pictureBox1.Image = img;
        //    }
        //}

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.png, *.bmp)|*.jpg; *.png; *.bmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    img = new Bitmap(openFileDialog.FileName);
                    pictureBox1.Image = img;
                }
            }
        }

        private void applyFiltersButton_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show("Please open an image first.");
                return;
            }

            // Create copies of the original image for each filter
            Bitmap grayscaleImage = new Bitmap(img);
            Bitmap thresholdImage = new Bitmap(img);
            Bitmap inverseImage = new Bitmap(img);
            Bitmap mirrorImage = new Bitmap(img);

            // Apply filters to each copy of the original image
            ApplyGrayscaleFilter(grayscaleImage);
            ApplyThresholdFilter(thresholdImage);
            InversionFilter(inverseImage);
            MirrorFilter(mirrorImage);

            // Display each filtered image in separate PictureBoxes
            pictureBox1.Image = img; // Original image
            pictureBox2.Image = grayscaleImage; // Image after grayscale filter
            pictureBox3.Image = thresholdImage; // Image after threshold filter
            pictureBox4.Image = inverseImage; // Image after edge detection filter
            pictureBox5.Image = mirrorImage; // Image after mirror filter
        }


        private void ApplyGrayscaleFilter(Bitmap image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    image.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
        }

        private void ApplyThresholdFilter(Bitmap image)
        {
            int threshold = 128;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int avg = (pixel.R + pixel.G + pixel.B) / 3;
                    Color newColor = avg < threshold ? Color.Black : Color.White;
                    image.SetPixel(x, y, newColor);
                }
            }
        }

        private void InversionFilter(Bitmap image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    Color newColor = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    image.SetPixel(x, y, newColor);
                }
            }
        }

        private void MirrorFilter(Bitmap image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width / 2; x++)
                {
                    Color temp = image.GetPixel(x, y);
                    image.SetPixel(x, y, image.GetPixel(image.Width - x - 1, y));
                    image.SetPixel(image.Width - x - 1, y, temp);
                }
            }
        }
    }
}
