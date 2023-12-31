﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Services;
using TimBrowser.DataCore.Communication;
using TimBrowser.Services;
using Caliburn.Micro;
using System.Windows;
using TimBrowser.Messages;
using System.Windows.Forms;

namespace TimBrowser.ViewModels
{
    public class DownloadInformationModuleViewModel : PropertyChangedBase
    {
        public DownloadInformationModuleViewModel(TimDownloadService timDownloadService, TimFileService timFileService,
            TimErrorService timErrorService)
        {
            _timDownloadService = timDownloadService;
            _timFileService = timFileService;
            _timDownloadService.ProgressChangedAction = ProgressAction;
            _timErrorService = timErrorService;
            _timErrorService.TimErrorEvent = OnTimError;

            _downloadButtonVisibility = Visibility.Visible;
            _downloadVisibility = Visibility.Collapsed;
        }

        #region Fields

        //private DataManager _dataManager;
        private TimDownloadService _timDownloadService;
        private TimFileService _timFileService;
        private TimErrorService _timErrorService;
        private ITimCommunication _communication;
        private ICommunicationSource _communicationM;
        private IBLECommunication _communicationBLE;
        private Visibility _downloadVisibility;
        private Visibility _downloadButtonVisibility;
        public int TypeComm;


        private string _progressString;
        private bool _isDownloadError;

        public System.Action<bool> DownloadAction;
        

        #endregion

        #region Methods

        public void Deactivate()
        {
            _downloadVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Инициирует процесс считывания
        /// ТОЧКА ВХОДА
        /// </summary>
        //todo Добавить точку входа для БЛЕ 

        public void DownloadStart()
        {
            switch (TypeComm)
            {
                case 1: // блютуз
                    if (_communication != null)
                    RaiseDownloadAction(true);

                    _timDownloadService.DownloadAsync(_communication,
                        () =>
                        {
                            RaiseDownloadAction(false);
                        });
                    break;
                case 2: // модбас
                    if (_communicationM != null)
                    RaiseDownloadAction(true);

                    _timDownloadService.DownloadAsyncM(_communicationM,
                        () =>
                        {
                            RaiseDownloadAction(false);
                        });
                    
                    break;
                case 3: // БЛЕ
                    if (_communicationBLE != null)
                        RaiseDownloadAction(true);
                    _timDownloadService.DownloadAsyncBLE(_communicationBLE,
                        () =>
                        {
                            RaiseDownloadAction(false);
                        });

                    break;
            }
            
        }

        private void RaiseDownloadAction(bool isDownloading)
        {
            DownloadButtonVisibility = (isDownloading) ?
                Visibility.Collapsed : Visibility.Visible;

            if (!isDownloading && !_isDownloadError)
                ProgressString = "Считывание завершено";

            if (DownloadAction != null)
                DownloadAction(isDownloading);
        }

        public void ActivateCommunication(ITimCommunication communication)
        {
            _communication = communication;

            if (_communication != null)
                DownloadVisibility = Visibility.Visible;
            else
                DownloadVisibility = Visibility.Collapsed;
        }

        public void ActivateCommunicationM(ICommunicationSource communication)
        {
            _communicationM = communication;

            if (_communicationM != null)
                DownloadVisibility = Visibility.Visible;
            else
                DownloadVisibility = Visibility.Collapsed;
        }

        //todo change interface
        public void ActivateCommunicationBLE(IBLECommunication communication)
        {
            _communicationBLE = communication;

            if (_communicationBLE != null)
                DownloadVisibility = Visibility.Visible;
            else
                DownloadVisibility = Visibility.Collapsed;
        }

        private void ProgressAction(string s)
        {
            ProgressString = "Считывание " + s;
        }

        private string TransformError(string str)
        {
            string fileName = str + Helper.Constants.FILE_SAVE_EXT;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;

            _timFileService.SaveFile(fileName, filePath);

            return filePath;
        }

        /// <summary>
        /// Обработчик события, который отображенает ошибки при считывании информационного модуля
        /// </summary>
        private void OnTimError(object sender, TimErrorEventArgs e)
        {
            if (e.Source != ServiceSources.DownloadService)
                return;

            if (e.MessageType == ErrorMessageTypes.DownloadImError)
            {
                _isDownloadError = true;
                ProgressString = "Ошибка считывания";
            }
            else if (e.MessageType == ErrorMessageTypes
                .TransformImError)
            {
                _isDownloadError = true;
                string errFilePath = TransformError(e.AddString);
                ProgressString = "Ошибка преобразования " + errFilePath;
            }
        }

        #endregion



        #region Properties

        public string ProgressString
        {
            get { return _progressString; }
            set
            {
                _progressString = value;
                NotifyOfPropertyChange("ProgressString");
            }
        }

        public Visibility DownloadVisibility
        {
            get { return _downloadVisibility; }
            set
            {
                _downloadVisibility = value;
                NotifyOfPropertyChange("DownloadVisibility");
            }
        }

        public Visibility DownloadButtonVisibility
        {
            get { return _downloadButtonVisibility; }
            set
            {
                _downloadButtonVisibility = value;
                NotifyOfPropertyChange("DownloadButtonVisibility");
            }
        }

        #endregion



    }
}
