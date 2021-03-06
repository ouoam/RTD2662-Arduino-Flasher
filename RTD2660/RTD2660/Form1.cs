﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace RTD2660
{
    public partial class Form1 : Form
    {
        SerialPort port;
        public Form1()
        {
            InitializeComponent();

            FillSerialPorts();

            port = new SerialPort();

            // Allow the user to set the appropriate properties.
            port.BaudRate = 250000;
            // Set the read/write timeouts
            port.ReadTimeout = 2000;
            port.WriteTimeout = 2000;

        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (port.IsOpen)
                {
                    buttonOpen.Text = "Open";
                    serialPortcComboBox.Enabled = true;
                    buttonInfo.Enabled = false;
                    buttonErase.Enabled = false;
                    buttonFlash.Enabled = false;
                    buttonSave.Enabled = false;
                    port.Close();
                }
                else
                {
                    port.PortName = (string) serialPortcComboBox.SelectedItem;
                    port.Open();
                    buttonOpen.Text = "Close";
                    serialPortcComboBox.Enabled = false;
                    buttonInfo.Enabled = true;
                    buttonErase.Enabled = true;
                    buttonFlash.Enabled = true;
                    buttonSave.Enabled = true;
                }
            }
            catch (System.SystemException ex)
            {
                debugTextBox.AppendText("Error opening Port\n");
            }

            if (port.IsOpen)
            {
                try
                {
                    char[] cmd = { 'I' };
                    port.Write(cmd, 0, 1);
                    string info = port.ReadLine();
                    debugTextBox.AppendText(info + "\n");
                }
                catch (System.SystemException ex)
                {
                    debugTextBox.AppendText("No device response!\n");
                }
            }
        }

        private void FillSerialPorts()
        {

            foreach (string s in SerialPort.GetPortNames())
            {
                serialPortcComboBox.Items.Add(s);
            }
            serialPortcComboBox.SelectedIndex = serialPortcComboBox.Items.Count - 1;
        }

        private void buttonErase_Click(object sender, EventArgs e)
        {
            char[] cmd = { 'E' };
            port.Write(cmd, 0, 1);
            string info = port.ReadLine();
            debugTextBox.AppendText(info + "\n");
            label2.Visible = true;
            label2.Text = "";
        }

        private void buttonFlash_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;

            char[] cmd = { 'W' };
            port.Write(cmd, 0, 1);

            string info = port.ReadLine();
            debugTextBox.AppendText(info + "\n");

            byte[] bytes = new byte[256];
            try
            {
                using (FileStream fsSource = new FileStream(openFileDialog1.FileName,
                    FileMode.Open, FileAccess.Read))
                {
                    // Read the source file into a byte array.
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;

                    progressBar.Maximum = numBytesToRead;
                    while (numBytesToRead > 0)
                    {
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bytes[i] = 0xFF;
                        }
                        // Read may return anything from 0 to numBytesToRead.
                        int n = fsSource.Read(bytes, 0, bytes.Length);

                        numBytesRead += n;

                        if (n > 0) n = 1;

                        byte[] b = { (byte)n };
                        port.Write(b, 0, 1);

                        if (n == 0)
                        {
                            break;
                        }

                        bool error = false;

                        try
                        {
                            char answer = (char)port.ReadChar();
                            error = answer != '1';
                        }
                        catch (System.TimeoutException ex)
                        {
                            error = true;
                            debugTextBox.AppendText("Time\r\n");
                        }

                        if (error)
                        {
                            debugTextBox.AppendText(String.Format("Errror at {0}\r\n", numBytesRead));
                            break;
                        }
                        else
                        {
                            label2.Text = String.Format("Flashed {0} of {1}", numBytesRead, fsSource.Length);
                            progressBar.Value = numBytesRead;
                        }

                        Application.DoEvents();
                        if (error)
                            break;

                        port.Write(bytes, 0, bytes.Length);
                    }
                    fsSource.Close();
                }
            }
            catch (FileNotFoundException ioEx)
            {
                byte[] b = { 0 };
                port.Write(b, 0, 1);

                char answer = (char)port.ReadChar();
                Console.WriteLine(ioEx.Message);
            }

            info = port.ReadLine();
            debugTextBox.AppendText(info + "\n");
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileInputTextBox.Text = openFileDialog1.FileName;
                fileInputTextBox.SelectionStart = fileInputTextBox.Text.Length;
                fileInputTextBox.SelectionLength = 0;
            }
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            char[] cmd = { 'C' };
            port.Write(cmd, 0, 1);
            string info = port.ReadLine();
            debugTextBox.AppendText(info + "\n");
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileSaveTextBox.Text = saveFileDialog1.FileName;
                fileSaveTextBox.SelectionStart = fileSaveTextBox.Text.Length;
                fileSaveTextBox.SelectionLength = 0;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;

            char[] cmd = { 'S' };
            port.Write(cmd, 0, 1);

            string info = port.ReadLine();
            debugTextBox.AppendText(info + "\n");

            info = port.ReadLine();
            UInt32 len = UInt32.Parse(info);

            progressBar.Maximum = (int)len;

            byte[] bytes = new byte[256];
            uint gCrc = 0;
            try
            {
                using (FileStream fsSource = new FileStream(saveFileDialog1.FileName,
                    FileMode.OpenOrCreate, FileAccess.Write))
                {
                    int numBytesRead = 0;
                    
                    while (true)
                    {
                        bool error = false;
                        try
                        {
                            char answer = (char)port.ReadChar();
                            if (answer == 'f') break;
                            error = answer != 'r';
                        }
                        catch (System.TimeoutException ex)
                        {
                            error = true;
                        }

                        char[] b = { 'g' };
                        port.Write(b, 0, 1);

                        if (error)
                        {
                            debugTextBox.AppendText(String.Format("Errror at {0}\r\n", numBytesRead));
                            break;
                        }

                        //port.Read(bytes, 0, 128);
                        for (int i = 0; i < 256; i++)
                        {
                            int a = -1;
                            while (a == -1)
                            {
                                a = port.ReadByte();
                            }
                            bytes[i] = (byte)a;

                            gCrc ^= (uint)bytes[i] << 8;
                            for (int j = 8; j != 0; j--)
                            {
                                if ((gCrc & 0x8000) != 0)
                                    gCrc ^= (0x1070 << 3);
                                gCrc <<= 1;
                            }
                        }

                        numBytesRead += 256;

                        label2.Text = String.Format("Loaded {0}", numBytesRead);
                        progressBar.Value = numBytesRead;

                        Application.DoEvents();

                        fsSource.Write(bytes, 0, 256);
                    }
                    fsSource.Close();
                }
            }
            catch (FileNotFoundException ioEx)
            {
                byte[] b = { 0 };
                port.Write(b, 0, 1);

                char answer = (char)port.ReadChar();
                Console.WriteLine(ioEx.Message);
            }

            info = port.ReadLine();
            debugTextBox.AppendText(info + String.Format(" Program CRC {0:X}", (uint)(gCrc >> 8)) + "\r\n");
        }
    }
}
