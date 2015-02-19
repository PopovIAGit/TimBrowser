using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.Download
{
    public partial class DownloadInformationModule
    {

        private FuncFourData FuncFourDownload(FuncOneData funcOneData, FuncThreeData funcThreeData)
        {
            byte funcCodeTransmit = 4;
            int logsNum = funcOneData.LogsNumber;

            if (logsNum == 0)                                                       // Проверяем количество журналов в устройстве       
            {
                ThrowDownloadException(DownloadErrorCode.FuncFourError);

                //SendAlgorythmMessage(DataManagerRes.AlgFuncFourLogsNum, true);
                return null;
            }

            List<FuncFourCells> funcFourLogsList = new List<FuncFourCells>();

            for (int logTypeIndex = 0; logTypeIndex
                < logsNum; logTypeIndex++)      // Считываем данные для каждого типа журнала
            {
                byte logType = funcOneData.LogsTypes[logTypeIndex];
                int logCellNum = funcThreeData.LogsData[logTypeIndex].LogCellNumber;

                List<FuncFourFieldsAddress> funcFourCellsList = new List<FuncFourFieldsAddress>();

                for (byte cellIndex = 1; cellIndex <= logCellNum; cellIndex++)      // Считываем данные для каждой ячейка у текущего типа журнала
                {

                    if (cellIndex == 7)
                    {
                        cellIndex = 7;
                    }

                    TransmitByte(funcCodeTransmit);                                 // Передаем код функции
                    System.Threading.Thread.Sleep(30);
                    TransmitByte(logType);                                          // Передаем тип журнала
                    System.Threading.Thread.Sleep(30);
                    TransmitByte(cellIndex);                                        // Передаем номер ячейки
                    System.Threading.Thread.Sleep(30);

                    byte funcCodeReceive = ReceiveByte();                           // Считываем ответный код функции
                    byte logTypeReceive = ReceiveByte();                           // Считываем ответный тип журнала
                    byte logCellReceive = ReceiveByte();                           // Считываем ответный номер ячейки
                    

                    if (funcCodeTransmit != funcCodeReceive)                        // Проверяем ответный код функции
                    {
                        //ThrowDownloadException(DownloadErrorCode.FuncFourError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFourTransmitFuncCode, true);
                        //return null;

                        for (int i = 0; i < 50; i++) ReceiveByte();
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }
                    else if (logTypeReceive != logType)                             // Проверяем ответный тип журнала
                    {
                        //ThrowDownloadException(DownloadErrorCode.FuncFourError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFourLogType, true);
                        //return null;
                        for (int i = 0; i < 50; i++) ReceiveByte();
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }
                    else if (logCellReceive != cellIndex)                           // Проверяем ответный номер ячейки
                    {
                        //ThrowDownloadException(DownloadErrorCode.FuncFourError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFourLogCell, true);
                        //return null;
                        for (int i = 0; i < 50; i++) ReceiveByte();
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }

                    // Умножаем на 2, т.к. посылается старшийи  младший байты
                    // Индекс массива - 1
                    byte fieldNum = (byte)(funcThreeData.LogsData[logTypeIndex].LogCellFieldNumber[(cellIndex - 1)] * 2);

                    byte funcDataSize = ReceiveByte();                              // Считываем размер ответного пакета

                    // Валидация размера ответного пакета
                    if (funcDataSize != fieldNum)
                    {
                        //ThrowDownloadException(DownloadErrorCode.FuncFourError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFourFieldNum, true);
                        //return null;
                        for (int i = 0; i < 50; i++) ReceiveByte();
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }
                    else if (funcDataSize <= 0)
                    {
                       // ThrowDownloadException(DownloadErrorCode.FuncFourError);
                        //SendAlgorythmMessage(DataManagerRes.AlgFuncFourFuncDataSize, true);
                        //return null;
                        for (int i = 0; i < 50; i++) ReceiveByte();
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }

                    byte[] funcData = ReceiveBytes(funcDataSize);                   // Считываем ответный пакет

                    List<int> logFieldsAddress = new List<int>();

                    // Инкрементируем на 2, т.к. извлекаем сразу старший и младший биты
                    for (int i = 0; i < funcDataSize; i += 2)
                    {
                        // Извлекаем адрес текущего поля
                        int cellFieldAddress = (int)((funcData[(int)FuncFourIndicies.CellFieldAddressMsb + i] << 8) |
                            funcData[(int)FuncFourIndicies.CellFieldAddressLsb + i]);

                        logFieldsAddress.Add(cellFieldAddress);                    // Добавляем адрес поля в список
                    }

                    // Добавляем список с адресами полей текущей ячейки
                    funcFourCellsList.Add(new FuncFourFieldsAddress(logFieldsAddress));
                }

                // Добавляем список с ячейками у текущего типа журнала
                funcFourLogsList.Add(new FuncFourCells(funcFourCellsList));

            }

            return new FuncFourData(funcFourLogsList);                              // Возвращаем объект четвертой функции

        }
    }
}
