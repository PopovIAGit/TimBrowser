using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.DownloadM
{
    public partial class DownloadInformationModule
    {

        // нужно добавить подсчета прогресса считывания
        // int progress = CalculateProgress(indexRec, logRecsNum, 100);

        private FuncFiveData FuncFiveDownload(FuncOneData funcOneData, FuncTwoData funcTwoData, FuncThreeData funcThreeData)
        {
            //ushort[] Data = new ushort[100];
            ushort[] txData = new ushort[4];
            ushort[] rxData = new ushort[200];
            int _repeat=0;

            byte funcCodeTransmit = 5;

            int logsNum = funcOneData.LogsNumber;

            if (logsNum == 0)
            {
                ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncFiveLogsNum, true);
                return null;
            }

            List<FuncFiveRecords> funcFiveRecordsList = new List<FuncFiveRecords>();

            for (int logTypeIndex = 0; logTypeIndex < logsNum; logTypeIndex++)              // Считывание данных для каждого типа журнала
            {
                //progressIndex++;

                byte logType = funcOneData.LogsTypes[logTypeIndex];
                int logCellNum = funcThreeData.LogsData[logTypeIndex].LogCellNumber;
                int logRecsNum = funcTwoData.LogsData[logTypeIndex].LogRecordsNumber;
                int logRecAddr = funcTwoData. LogsData[logTypeIndex].LogCurrentAddress;

                if (logRecsNum == 0)                                                        // Число сделанных записей у текущего типа журнала равно нулю
                {
                    // Создаем пустой экзепмляр журнала и переходим к следующему журналу
                    List<int> logFieldValuesList = new List<int>();
                    List<FuncFiveValues> funcFiveValuesList = new List<FuncFiveValues>();
                    funcFiveValuesList.Add(new FuncFiveValues(logFieldValuesList));
                    List<FuncFiveCells> funcFiveCellsList = new List<FuncFiveCells>();
                    funcFiveCellsList.Add(new FuncFiveCells(funcFiveValuesList));
                    funcFiveRecordsList.Add(new FuncFiveRecords(funcFiveCellsList));
                }
                else
                {
                    int recordSize = 0;

                    // Расчитываем размер записи: сумма размеров ячеек
                    for (int i = 0; i < logCellNum; i++)
                    {
                        int currentCellFieldCnt = funcThreeData.LogsData[logTypeIndex].LogCellFieldNumber[i];
                        recordSize += currentCellFieldCnt;
                    }

                    txData[0] = (ushort)funcCodeTransmit;                           // Передаем код функции
                    txData[1] = (ushort)logType;                                    // Передаем тип журнала
                    txData[2] = (ushort)(logRecsNum >> 8);                          // Передаем старший байт номера текущей записи
                    txData[3] = (ushort)(logRecsNum & 255);                         // Передаем младший байт номера текущей записи
                    TransmitBytes(_communication.DeviceAddress, 65123, txData);
                    System.Threading.Thread.Sleep(50);

                    byte funcCodeReceive = 0;
                    byte logTypeReceive = 0;
                    byte logRecCntReceiveMsb = 0;
                    byte logRecCntReceiveLsb = 0;


                    rxData = ReceiveBytes(_communication.DeviceAddress, 65124, 6);
                    System.Threading.Thread.Sleep(20);
                    funcCodeReceive = (byte)rxData[0];              // Считываем ответный код функции
                    logTypeReceive = (byte)rxData[1];               // Считываем ответный тип журнала
                    logRecCntReceiveMsb = (byte)rxData[2];
                    logRecCntReceiveLsb = (byte)rxData[3];


                    if (funcCodeTransmit != funcCodeReceive)                            // Проверяем ответный код функции
                    {
                        logTypeIndex--;
                        _repeat = 1;
                        continue;
                    }
                    else if (logTypeReceive != logType)                                 // Проверяем ответный тип журнала
                    {
                        logTypeIndex--;
                        _repeat = 1;
                        continue;
                    }
                    _repeat = 0;

                    List<FuncFiveCells> funcFiveCellsList = new List<FuncFiveCells>();

                    for (int indexRec = 0; indexRec < logRecsNum; indexRec++)           // Считываем данные для каждой записи журнала
                    {
                        byte recordNumberMsb = 0;
                        byte recordNumberLsb = 0;
                        //???
                        //rxData = ReceiveBytes(_communication.DeviceAddress, 65123, 2);

                        if (indexRec == 0)
                        {
                            recordNumberMsb = (byte)rxData[4];
                            recordNumberLsb = (byte)rxData[5];
                        }
                        else
                        {
                            if (logType == 1)
                            {
                                rxData = ReceiveBytes(_communication.DeviceAddress, 65124+_repeat, (byte)(recordSize + 2));
                            }
                            else
                            {
                                rxData = ReceiveBytes(_communication.DeviceAddress, 65124+_repeat, (byte)(recordSize * 2 + 2));
                            }
                            System.Threading.Thread.Sleep(20);
                            recordNumberMsb = (byte)rxData[0];
                            recordNumberLsb = (byte)rxData[1];
                        }

                        // Склеиваем старший и младший биты номера записи
                        int recordNumber = (int)((recordNumberMsb << 8) | (recordNumberLsb & 255));

                        if (recordNumber != indexRec)                                   // Проверяем считанный номер записи
                        {
                            System.Threading.Thread.Sleep(100);
                            indexRec--;
                            _repeat = 1;
                            continue;
                        } _repeat = 0;
                        _repeat = 0;

                        byte funcDataSize = (byte)(recordSize * 2);                     // Размер ответного пакета больше в 2 раза размера записи, т.к. считываются отдельно старший и младший байты

                        //ushort[] funcData = ReceiveBytes(_communication.DeviceAddress, 65123, funcDataSize); //ReceiveBytes(funcDataSize);                   // Считываем пакет данных
                        ushort[] funcData = new ushort[funcDataSize];
                        if (indexRec == 0)
                        {
                            if (logType == 1)
                            {
                                rxData = ReceiveBytes(_communication.DeviceAddress, 65124, funcDataSize/2);
                                System.Threading.Thread.Sleep(30);
                                for (int i = 0; i < funcDataSize/2; i++) { funcData[i] = rxData[i]; }
                                rxData = ReceiveBytes(_communication.DeviceAddress, 65124, funcDataSize/2);
                                for (int i = 0; i < funcDataSize / 2; i++) { funcData[i + funcDataSize/2] = rxData[i]; }
                            }
                            else
                            {
                                rxData = ReceiveBytes(_communication.DeviceAddress, 65124, funcDataSize);
                                System.Threading.Thread.Sleep(20);
                                for (int i = 0; i < funcDataSize; i++) { funcData[i] = rxData[i]; }
                            }
                        }
                        else
                        {
                            if (logType == 1)
                            {
                                for (int i = 0; i < funcDataSize / 2; i++) { funcData[i] = rxData[i+2]; }
                                rxData = ReceiveBytes(_communication.DeviceAddress, 65124, funcDataSize / 2);
                                for (int i = 0; i < funcDataSize / 2; i++) { funcData[i + funcDataSize / 2] = rxData[i]; }
                            }
                            else
                            {
                                for (int i = 0; i < funcDataSize; i++) { funcData[i] = rxData[i + 2]; }
                                //rxData = ReceiveBytes(_communication.DeviceAddress, 65124, funcDataSize);
                            }
                        }
                            
                        
                        
                        System.Threading.Thread.Sleep(20);
                        //for (int i = 0; i < funcDataSize / 2; i++) { funcData[i + funcDataSize / 2] = rxData[i]; }

                        List<FuncFiveValues> funcFiveValuesList = new List<FuncFiveValues>();
                        if (indexRec == 1)
                        {
                            indexRec = 1;
                        }
                        int cellSize = 0;

                        // Поскольку считалась вся запись целиком, то раскладываем на ячейки
                        for (int cellIndex = 0; cellIndex < logCellNum; cellIndex++)
                        {

                            int cellFielsSize = funcThreeData.LogsData[logTypeIndex].LogCellFieldNumber[cellIndex];

                            List<int> logFieldValuesList = new List<int>();             // Список со значениями в каждой ячейке

                            for (int i = 0; i < cellFielsSize; i++)
                            {
                                // int indexMsb = (int)FuncFiveIndicies.CellFieldValueMsb + (cellSize + (i * 2));
                                // int indexLsb = (int)FuncFiveIndicies.CellFieldValueLsb + (cellSize + (i * 2));

                                int indexMsb = (int)FuncFiveIndicies.CellFieldValueMsb + ((cellSize * 2) + (i * 2));
                                int indexLsb = (int)FuncFiveIndicies.CellFieldValueLsb + ((cellSize * 2) + (i * 2));

                                // Значение текущего поля в ячейке
                                int logFieldValue = (int)(funcData[indexMsb] << 8) | funcData[indexLsb];
                                logFieldValuesList.Add(logFieldValue);
                            }

                            funcFiveValuesList.Add(new FuncFiveValues(logFieldValuesList));     // Список ячеек со значениями

                            cellSize += cellFielsSize;
                        }

                        funcFiveCellsList.Add(new FuncFiveCells(funcFiveValuesList));           // Список записей с ячейками

                        int currentProgress = Utils.CalculateProgress(indexRec, logRecsNum, 100);

                        RaiseDownloadProgressChanged(logTypeIndex + 1, currentProgress);
                    }

                    funcFiveRecordsList.Add(new FuncFiveRecords(funcFiveCellsList));            // Список со списком записей

                }

            }

            return new FuncFiveData(funcFiveRecordsList);

        }



    }
}
