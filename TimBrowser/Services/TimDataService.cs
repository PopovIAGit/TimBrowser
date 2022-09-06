using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TimBrowser.Model;
using TimBrowser.DataCore.Services;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.Services
{
    public class TimDataService
    {
        public TimDataService(TimErrorService timErrorService)
        {
            _coreDataService = new DataService();
            _timErrorService = timErrorService;
        }

        #region Fields

        public const string STOP_CMD_WORD = "Stop";
        public const string CLOSE_CMD_WORD = "Close";
        public const string OPEN_CMD_WORD = "Open";

        private readonly DataService _coreDataService;

        private TimErrorService _timErrorService;
        private FuncDownloadData _currentFuncDownloadData;
        private InformationModuleData _currentInformationModule;

        /// <summary>
        /// Событие, возникающее при обновлении данных
        /// </summary>
        public EventHandler OnTimDataChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Помещаем считанные данные информационного модуля
        /// </summary>
        /// <param name="funcDownloadData">Новые считанные данные информационного модуля</param>
        public void PutFuncDownloadData(ServiceSources source, FuncDownloadData funcDownloadData)
        {
            _currentFuncDownloadData = funcDownloadData;

            if (_currentFuncDownloadData != null)
            {
                // запускаем асинхронный процесс преобразования информационного модуля
                _coreDataService.TransformFuncDownloadAsync(
                    _currentFuncDownloadData, (im) =>
                        {
                            if (im != null)
                            {
                                _currentInformationModule = im;
                                PutNewInformModuleData(_currentInformationModule);
                            }
                            else
                            {
                                string addStr = _currentFuncDownloadData.FuncOne.IdOfDevice.ToString() +
                                    " считано " + "(" +
                                    _currentFuncDownloadData.DateTimeDownload
                                    .ToString(Helper.Constants.DATE_TIME_FILE_FORMAT_STRING) + ")";

                                _timErrorService.SendErrorMessage(source, ErrorMessageTypes.TransformImError, addStr);
                            }
                        });
            }
        }

        /// <summary>
        /// Добавление новых преобразованных данных информационного модуля
        /// </summary>
        /// <param name="im">Новые данные информационного модуля</param>
        public void PutNewInformModuleData(InformationModuleData im)
        {
            if (im == null)
                return;

            _currentInformationModule = im;

            MapDataAsync();
        }

        private void MapDataAsync()
        {
            BackgroundWorker asyncWorker = new BackgroundWorker();
            asyncWorker.DoWork += (s, e) =>
            {
                LogEvents = Mapper.LogMapper.MapLogEv(_currentInformationModule.DeviceLogs.EventLog);
                LogCmd = Mapper.LogMapper.MapLogCmd(_currentInformationModule.DeviceLogs.CommandLog);
                LogEventsAndCmd = Mapper.LogMapper.MapLogEvAndCmd(_currentInformationModule.DeviceLogs.EventAndCmdLog);
                LogParameter = Mapper.LogMapper.MapLogParameter(_currentInformationModule.DeviceLogs.ChangeParameterLog);
                LogSim = Mapper.LogMapper.MapLogSim(_currentInformationModule.DeviceLogs.SimLog);
                GroupOfParameters = Mapper.ParameterMapper.MapGroupsOfParameters(_currentInformationModule.CurrentParameters.Groups);

                CalculateMovementTime();
            };

            asyncWorker.RunWorkerCompleted += (s, e) =>
            {
                RaiseTimDataChangedEvent();
            };

            asyncWorker.RunWorkerAsync();

        }



        private void CalculateMovementTime()
        {
            if (_currentInformationModule.DeviceLogs.CommandLog != null)
            {
                bool waitNextCmdStop = false;
                DateTime startDateTime = new DateTime();
                DateTime stopDateTime = new DateTime();

                int moveSec = 0;
                int moveMin = 0;
                int moveHr = 0;

                foreach (var cmdRec in _currentInformationModule.DeviceLogs.CommandLog)
                {
                    var cmdValue = cmdRec.Command.ValueDescription.Fields
                        .Where(f => f.BitValue == (int)cmdRec.Command.DValue).FirstOrDefault();

                    if (cmdValue != null)
                    {
                        if (cmdValue.SpecialDescription == CLOSE_CMD_WORD ||
                            cmdValue.SpecialDescription == OPEN_CMD_WORD)
                        {
                            waitNextCmdStop = true;
                            startDateTime = cmdRec.DateAndTime;
                        }
                        else if (cmdValue.SpecialDescription == STOP_CMD_WORD &&
                            waitNextCmdStop)
                        {
                            waitNextCmdStop = false;
                            stopDateTime = cmdRec.DateAndTime;

                            if (stopDateTime > startDateTime)
                            {
                                TimeSpan ts = stopDateTime - startDateTime;

                                moveSec += ts.Seconds;
                                moveMin += ts.Minutes;
                                moveHr += (int)ts.TotalHours;
                            }
                        }
                        else
                        {
                            waitNextCmdStop = false;
                        }
                    }
                }

                int moveSeconds = moveSec % 60;
                int moveMinutes = (moveSec / 60) % 60;
                int moveHours = ((moveSec / 60) / 60) + moveHr;

                MoveTimeString = moveHours.ToString("00") + ":" + moveMinutes.ToString("00") + ":" +
                         moveSeconds.ToString("00");
            }
        }

        private void RaiseTimDataChangedEvent()
        {
            if (OnTimDataChanged != null)
                OnTimDataChanged(this, new EventArgs());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Преобразованные данные журнала событий
        /// </summary>
        public ObservableCollection<TimLogEvRecItem> LogEvents
        {
            get;
            private set;
        }

        /// <summary>
        /// Преобразованные данные журнала событий и комманд
        /// </summary>
        public ObservableCollection<TimLogEvAndCmdRecItem> LogEventsAndCmd
        {
            get;
            private set;
        }

        /// <summary>
        /// Преобразованные данные журнала команд
        /// </summary>
        public ObservableCollection<TimLogCmdRecItem> LogCmd
        {
            get;
            private set;
        }

        /// <summary>
        /// Преобразованные данные журнала изменения параметров
        /// </summary>
        public ObservableCollection<TimLogParamRecItem> LogParameter
        {
            get;
            private set;
        }

        /// <summary>
        /// Преобразованные данные групп с параметрами
        /// </summary>
        public ObservableCollection<TimParametersGroup> GroupOfParameters
        {
            get;
            private set;
        }

        /// <summary>
        /// Преобразованные данные журнала команд
        /// </summary>
        public ObservableCollection<TimLogSimRecItem> LogSim
        {
            get;
            private set;
        }

        /// <summary>
        /// Строковое значение времени движения
        /// </summary>
        public string MoveTimeString
        {
            get;
            private set;
        }

        /// <summary>
        /// Текущий объект считанных данных
        /// </summary>
        public FuncDownloadData CurrentFuncDownloadData
        {
            get { return _currentFuncDownloadData; }
            set { _currentFuncDownloadData = value; }
        }

        /// <summary>
        /// Текущий объект преобразованного информационного модуля
        /// </summary>
        public InformationModuleData CurrentInformationModule
        {
            get { return _currentInformationModule; }
        }

        #endregion

    }
}
