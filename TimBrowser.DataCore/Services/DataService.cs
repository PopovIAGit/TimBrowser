using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.Download.Model;
using System.Diagnostics;
using System.ComponentModel;
using TimBrowser.DataCore.Model;
using TimBrowser.DataCore.Transform;
using TimBrowser.DataCore.Download.Events;

namespace TimBrowser.DataCore.Services
{
    public class DataService
    {
        public DataService()
        {

        }

        public void DownloadAsync(ITimCommunication timCommunication, 
            Action<FuncDownloadData> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = Download(timCommunication);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                FuncDownloadData funcDownloadData = (FuncDownloadData)e.Result;

                if (onComplete != null)
                    onComplete(funcDownloadData);
            };

            worker.RunWorkerAsync();
            
        }

        /// <summary>
        /// Считывание данных информационного модуля с помощью объекта ITimCommunication (любой коммуникацинный интерфейс)
        /// </summary>
        /// <param name="timCommunication">Реализация интерфейса ITimCommunication</param>
        /// <returns>Данные информационного модуля</returns>
        public FuncDownloadData Download(ITimCommunication timCommunication)
        {
            DownloadInformationModule downloadInfModule = new DownloadInformationModule(timCommunication);
            downloadInfModule.DownloadPropgressChanged += OnDownloadProgressChanged;

            FuncDownloadData funcDownloadData = downloadInfModule.Download();

            if (funcDownloadData == null)
            {
                Debug.WriteLine("Ошибка считывания информационного модуля");
            }

            return funcDownloadData;
        }

        public void TransformFuncDownloadAsync(FuncDownloadData funcDownloadData,
            Action<InformationModuleData> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();
            
            worker.DoWork += (s, e) =>
            {
                e.Result = TransformFuncDownload(funcDownloadData);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                InformationModuleData im = (InformationModuleData)e.Result;

                if (onComplete != null)
                    onComplete(im);
            };

            worker.RunWorkerAsync();   
        }

        /// <summary>
        /// Преобразование данных информационного модуля
        /// </summary>
        /// <param name="funcDownloadData">Данные информационного модуля</param>
        /// <returns>Данные информационного модуля в виде таблиц</returns>
        public InformationModuleData TransformFuncDownload(FuncDownloadData funcDownloadData)
        {
            TransformDownloadData transformDownloadData = new TransformDownloadData();

            InformationModuleData im = transformDownloadData.Transform(funcDownloadData);

            return im;
        }
        
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (ProgressChangedAction != null)
            {
                string progressString = e.CurrentIndex.ToString() + " из " + e.MaxIndex.ToString() + ": " +
                    e.Progress.ToString() + "%";

                ProgressChangedAction(progressString);
            }
        }

        public Action<string> ProgressChangedAction;

    }
}
