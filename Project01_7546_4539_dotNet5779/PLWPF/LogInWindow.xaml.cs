﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            DataContext = this;
            InitializeComponent();
        }
        IBL bl = Bl_imp.GetBl();
        private string id;
        private DateTime birth;
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            id = idTextBox.Text;
            Trainee a = bl.GetTraineeById(id);
            birth = birthdayDatePicker.DisplayDate;
           // MessageBox.Show(id + "\n" + birth.ToString("d"));
            if (bl.CheckIdValidation(id))
            {
                if (a != null)
                {
                    TraneeMenu traneeMenu = new TraneeMenu(a);
                    traneeMenu.Show();
                    this.Close();
                }
                else
                {
                    AddOrUpdateTrainee addOrUpdateTrainee=new AddOrUpdateTrainee(id);
                    addOrUpdateTrainee.ShowDialog();
                }
            }
        }

        

        private void idTextBox_keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OkButton_Click(sender,e);
            }
            if ((e.Key< Key.D0|| e.Key > Key.D9)&& (e.Key < Key.NumPad0|| e.Key > Key.NumPad9)&& e.Key !=Key.Tab)//to handled
            {
                idTextBox.BorderBrush = Brushes.Red;
                e.Handled = true;

            }
            idTextBox.BorderBrush = Brushes.Gray;

        }

        private void LogInWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            idTextBox.Focus();
        }
    }

}