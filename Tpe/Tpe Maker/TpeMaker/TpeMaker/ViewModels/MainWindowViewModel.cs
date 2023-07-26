using System;
using TpeMaker.Services;
using TpeMaker.Helper;
using System.Windows;

namespace TpeMaker.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            _cryptographer = new Cryptographer();
        }

        private Cryptographer _cryptographer;

        public void OpenXmlFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = Constants.IN_FILE_EXT;
            openFileDialog.Filter = Constants.IN_FILE_FILTER;

            if (openFileDialog.ShowDialog() == true)
            {
                string inFilePath = openFileDialog.FileName;
                string outFilePath = Utils.StringUtils.MakeOutFilePath(inFilePath);

                _cryptographer.BeginCryptXmlDocument(inFilePath, outFilePath, (result) =>
                    {
                        bool error = _cryptographer.EndCryptXmlDocument(result);

                        if (error)
                            MessageBox.Show("Ошибка преобразования шаблона");
                        else
                            MessageBox.Show("Преобразование завершено");
                    });
                
            }
        }
        
    }
}