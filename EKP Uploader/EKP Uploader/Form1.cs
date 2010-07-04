using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EKP_Uploader
{
    public partial class UploaderForm : Form
    {
        public UploaderForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Upload(textBoxUserName.Text, textBoxPassword.Text, textBoxContentType.Text, textBoxFile.Text, new Uri(textBoxUrl.Text));
            Cursor = Cursors.Default;
            MessageBox.Show("Upload completed.", "Upload Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void UploadImsEnterprise(String path, String password, String baseUri)
        {
            Upload("dummy", password, "application/xml", path, new Uri(new Uri(baseUri), "contentHandler/imsEnterprise"));
        }

        private static void Upload(String userName, String password, String contentType, String path, Uri address)
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(userName, password);
            client.Headers.Add("Content-Type", contentType);

            using (FileStream input = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (Stream output = client.OpenWrite(address))
                {
                    Copy(input, output);
                }
            }
        }

        private static void Copy(Stream input, Stream output)
        {
            const int size = 4096;
            byte[] bytes = new byte[4096];
            int numBytes;
            while ((numBytes = input.Read(bytes, 0, size)) > 0)
                output.Write(bytes, 0, numBytes);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFile.Text = openFileDialog1.FileName;
            }
        }
    }
}
