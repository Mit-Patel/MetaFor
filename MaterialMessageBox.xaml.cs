﻿using System.Windows;

namespace MetaFor_Demo
{
    /// <summary>
    /// Interaction logic for MaterialMessageBox.xaml
    /// </summary>
    public partial class MaterialMessageBox : Window
    {
        public MaterialMessageBox(string Message, MessageType Type, MessageButtons Buttons, bool isProgress = false)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            switch (Type)
            {
                case MessageType.Info:
                    txtTitle.Text = "Info";
                    break;
                case MessageType.Confirmation:
                    txtTitle.Text = "Confirmation";
                    break;
                case MessageType.Success:
                    txtTitle.Text = "Success";
                    break;
                case MessageType.Warning:
                    txtTitle.Text = "Warning";
                    break;
                case MessageType.Error:
                    txtTitle.Text = "Error";
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.OkCancel:
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.Ok:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.None:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed; btnNo.Visibility = Visibility.Collapsed;
                    //this.DialogResult = false;
                    break;
            }
            if (isProgress) btnClose.Visibility = Visibility.Collapsed;
        }
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
    public enum MessageType
    {
        Info,
        Confirmation,
        Success,
        Warning,
        Error,
    }
    public enum MessageButtons
    {
        OkCancel,
        YesNo,
        Ok,
        None,
    }
}
