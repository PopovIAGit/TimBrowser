using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.Download.Model;
using TimBrowser.DataCore.DownloadBLE;
using TimBrowser.DataCore.DownloadBLE.Events;
using TimBrowser.DataCore.DownloadBLE.Model;
using TimBrowser.DataCore.Model;
using TimBrowser.DataCore.Transform;

namespace TimBrowser.DataCore.Services
{
    public class DataServiceBLE
    {

        public DataServiceBLE() { }

        public void DownloadAsyncBLE(IBLECommunication timCommunication, Action<FuncDownloadDataBLE> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = DownloadBLE(timCommunication);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                FuncDownloadDataBLE funcDownloadData = (FuncDownloadDataBLE)e.Result;

                if (onComplete != null)
                    onComplete(funcDownloadData);
            };

            worker.RunWorkerAsync();
        }

        public FuncDownloadDataBLE DownloadBLE(IBLECommunication timCommunication)
        {

            DownloadInformationModule downloadInfModule = new DownloadInformationModule(timCommunication);
            downloadInfModule.DownloadPropgressChanged += OnDownloadProgressChangedBLE;

            FuncDownloadDataBLE funcDownloadData = downloadInfModule.Download();

            if (funcDownloadData == null)
            {
                Debug.WriteLine("Ошибка считывания информационного модуля");
            }

            return funcDownloadData;
        }

        public void TransformFuncDownloadAsync(FuncDownloadDataBLE funcDownloadData,
           Action<InformationModuleData> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = TransformFuncDownloadBLE(funcDownloadData);
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
        public InformationModuleData TransformFuncDownloadBLE(FuncDownloadDataBLE funcDownloadData)
        {
            TransformDownloadDataBLE transformDownloadData = new TransformDownloadDataBLE();
            InformationModuleData im = transformDownloadData.Transform(funcDownloadData);

            return im;
        }

        private void OnDownloadProgressChangedBLE(object sender, DownloadProgressChangedEventArgs e)
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
