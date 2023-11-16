using System;
using System.IO;
using System.Windows;

namespace CriFsLib.GUI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private async void Application_Startup(object sender, StartupEventArgs e)
    {
        MainWindow mainWindow = new();
        if (e.Args.Length == 1)
        {
            mainWindow.Show();
            string cpkpath = Path.GetFullPath(e.Args[0]);
            if (File.Exists(cpkpath))
                mainWindow.ViewModel.OpenCpk(cpkpath);
            else if (Directory.Exists(cpkpath))
                MessageBox.Show("This program does not currently support packing CPKs.");
        }
        if (e.Args.Length > 1)
        {
            string cpkpath = Path.GetFullPath(e.Args[0]);
            if (File.Exists(cpkpath))
            {
                mainWindow.ViewModel.OpenCpk(cpkpath);
                string outpath = Path.GetFullPath(e.Args[1]);
                Directory.CreateDirectory(outpath);
                Console.WriteLine("Extracting");
                await mainWindow.ViewModel.ExtractAllAsync(outpath);
                mainWindow.Close();
            }
            mainWindow.Show();
        }
        else mainWindow.Show();
    }
}
