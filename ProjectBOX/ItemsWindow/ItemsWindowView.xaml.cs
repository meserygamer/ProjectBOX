﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectBOX.EntityFrameworkModelFiles;

namespace ProjectBOX.ItemsWindow
{
    /// <summary>
    /// Логика взаимодействия для ItemsWindowView.xaml
    /// </summary>
    public partial class ItemsWindowView : Window
    {
        //List<CompleteTask> completeTasks;

        public ItemsWindowView()
        {
            InitializeComponent();
            //GetAllCompleteTask();
        }

        //public async void GetAllCompleteTask()
        //{
        //    await Task.Run(() => {completeTasks = (new ProjectBoxDbContext().CompleteTasks.ToList()); });
        //    MainGrid.ItemsSource = completeTasks;
        //}
    }
}
