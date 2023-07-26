using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.DownloadBLE
{
    public partial class DownloadInformationModule
    {

        // нужно добавить подсчета прогресса считывания
        // int progress = CalculateProgress(indexRec, logRecsNum, 100);

        private FuncFiveData FuncFiveDownload(FuncOneData funcOneData, FuncTwoData funcTwoData, FuncThreeData funcThreeData)
        {
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

                    TransmitByte(funcCodeTransmit);                                     // Передаем код функции
                    System.Threading.Thread.Sleep(30);
                    TransmitByte(logType);                                              // Передаем тип журнала
                    System.Threading.Thread.Sleep(30);
                    TransmitByte((byte)(logRecsNum >> 8));                              // Передаем старший байт номера текущей записи
                    System.Threading.Thread.Sleep(30);
                    TransmitByte((byte)(logRecsNum & 255));                             // Передаем младший байт номера текущей записи
                    System.Threading.Thread.Sleep(30);

                    byte funcCodeReceive = ReceiveByte();                               // Считываем ответный код функции
                    //System.Threading.Thread.Sleep(30);
                    byte logTypeReceive = ReceiveByte();                                // Считываем ответный тип журнала
                    //System.Threading.Thread.Sleep(30);

                    if (funcCodeTransmit != funcCodeReceive)                            // Проверяем ответный код функции
                    {
                        //ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFiveTransmitFuncCode, true);
                        //return null;
                        for (int i = 0; i < 90; i++) ReceiveByte();
                        logTypeIndex--;
                        continue;
                    }
                    else if (logTypeReceive != logType)                                 // Проверяем ответный тип журнала
                    {
                        //ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFiveLogType, true);
                        //return null;
                        for (int i = 0; i < 90; i++) ReceiveByte();
                        logTypeIndex--;
                        continue;
                    }

                    byte logRecCntReceiveMsb = ReceiveByte();
                    //System.Threading.Thread.Sleep(20);
                    byte logRecCntReceiveLsb = ReceiveByte();
                    //System.Threading.Thread.Sleep(20);

                    List<FuncFiveCells> funcFiveCellsList = new List<FuncFiveCells>();

                    for (int indexRec = 0; indexRec < logRecsNum; indexRec++)           // Считываем данные для каждой записи журнала
                    {
                        byte recordNumberMsb = ReceiveByte();                           // Считываем старший байт текущий записи
                        //System.Threading.Thread.Sleep(20);
                        byte recordNumberLsb = ReceiveByte();                           // Считываем младший байт текущеий записи
                        //System.Threading.Thread.Sleep(20);

                        // Склеиваем старший и младший биты номера записи
                        int recordNumber = (int)((recordNumberMsb << 8) | (recordNumberLsb & 255));

                        if (recordNumber != indexRec)                                   // Проверяем считанный номер записи
                        {
                            //ThrowDownloadException(DownloadErrorCode.FuncFiveError);
                            //SendAlgorythmMessage(DataManagerRes.AlgFuncFiveRecordNumber, true);
                            //return null;
                            for (int i = 0; i < 100; i++) ReceiveByte();
                            System.Threading.Thread.Sleep(400);
                            indexRec--;
                            continue;
                        }

                        byte funcDataSize = (byte)(recordSize * 2);                     // Размер ответного пакета больше в 2 раза размера записи, т.к. считываются отдельно старший и младший байты

                        byte[] funcData = ReceiveBytes(funcDataSize);                   // Считываем пакет данных


                        List<FuncFiveValues> funcFiveValuesList = new List<FuncFiveValues>();

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
