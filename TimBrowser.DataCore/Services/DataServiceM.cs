using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM;
using TimBrowser.DataCore.Communication;
using TimBrowser.DataCore.DownloadM.Model;
using System.Diagnostics;
using System.ComponentModel;
using TimBrowser.DataCore.Model;
using TimBrowser.DataCore.DownloadM.Events;
using TimBrowser.DataCore.Transform;

namespace TimBrowser.DataCore.Services
{
    public class DataServiceM
    {
        public DataServiceM()
        {

        }

        public void DownloadAsyncM(ICommunicationSource timCommunication, 
            Action<FuncDownloadDataM> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, e) =>
            {
                e.Result = DownloadM(timCommunication);
            };

            worker.RunWorkerCompleted += (s, e) =>
            {
                FuncDownloadDataM funcDownloadData = (FuncDownloadDataM)e.Result;

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
        public FuncDownloadDataM DownloadM(ICommunicationSource timCommunication)
        {
            DownloadInformationModule downloadInfModule = new DownloadInformationModule(timCommunication);
            downloadInfModule.DownloadPropgressChanged += OnDownloadProgressChangedM;

            FuncDownloadDataM funcDownloadData = downloadInfModule.Download();

            if (funcDownloadData == null)
            {
                Debug.WriteLine("Ошибка считывания информационного модуля");
            }

            return funcDownloadData;
        }

        public void TransformFuncDownloadAsyncM(FuncDownloadDataM funcDownloadData,
            Action<InformationModuleData> onComplete)
        {
            BackgroundWorker worker = new BackgroundWorker();
            
            worker.DoWork += (s, e) =>
            {
                e.Result = TransformFuncDownloadM(funcDownloadData);
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
        public InformationModuleData TransformFuncDownloadM(FuncDownloadDataM funcDownloadData)
        {
            TransformDownloadDataM transformDownloadData = new TransformDownloadDataM();

            InformationModuleData im = transformDownloadData.Transform(funcDownloadData);

            return im;
        }
        
        private void OnDownloadProgressChangedM(object sender, DownloadProgressChangedEventArgs e)
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
