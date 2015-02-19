using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.DownloadM
{
    public partial class DownloadInformationModule
    {

        private FuncFourData FuncFourDownload(FuncOneData funcOneData, FuncThreeData funcThreeData)
        {
            byte funcCodeTransmit = 4;
            int logsNum = funcOneData.LogsNumber;
            ushort[] txData = new ushort[3];
            ushort[] rxData = new ushort[50];

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
                    txData[0] = (ushort)funcCodeTransmit;// Передаем код функции
                    txData[1] = (ushort)logType;// Передаем тип журнала
                    txData[2] = (ushort)cellIndex;// Передаем номер ячейки
                    TransmitBytes(_communication.DeviceAddress, 65123, txData);
                    System.Threading.Thread.Sleep(150);
                    
                    byte funcCodeReceive = 0;// ReceiveByte();                           // Считываем ответный код функции
                    byte logTypeReceive = 0;//ReceiveByte();                           // Считываем ответный тип журнала
                    byte logCellReceive = 0;//ReceiveByte();                           // Считываем ответный номер ячейки
                    byte funcDataSize = 0;// ReceiveByte();                              // Считываем размер ответного пакета
                    rxData = ReceiveBytes(_communication.DeviceAddress, 65123, 50);
                    funcCodeReceive = (byte)rxData[0];
                    logTypeReceive = (byte)rxData[1];
                    logCellReceive = (byte)rxData[2];
                    funcDataSize = (byte)rxData[3];
                    

                    if (funcCodeTransmit != funcCodeReceive)                        // Проверяем ответный код функции
                    {
                        System.Threading.Thread.Sleep(50);
                        cellIndex--;
                        continue;
                    }
                    else if (logTypeReceive != logType)                             // Проверяем ответный тип журнала
                    {
                        System.Threading.Thread.Sleep(50);
                        cellIndex--;
                        continue;
                    }
                    else if (logCellReceive != cellIndex)                           // Проверяем ответный номер ячейки
                    {
                        System.Threading.Thread.Sleep(50);
                        cellIndex--;
                        continue;
                    }

                    // Умножаем на 2, т.к. посылается старшийи  младший байты
                    // Индекс массива - 1
                    byte fieldNum = (byte)(funcThreeData.LogsData[logTypeIndex].LogCellFieldNumber[(cellIndex - 1)] * 2);

                    // Валидация размера ответного пакета
                    if (funcDataSize != fieldNum)
                    {
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }
                    else if (funcDataSize <= 0)
                    {
                        System.Threading.Thread.Sleep(400);
                        cellIndex--;
                        continue;
                    }

                    List<int> logFieldsAddress = new List<int>();

                    // Инкрементируем на 2, т.к. извлекаем сразу старший и младший биты
                    for (int i = 0; i < funcDataSize; i += 2)
                    {
                        // Извлекаем адрес текущего поля
                        int cellFieldAddress;
                        /*if (logType==3)
                        {*/
                            cellFieldAddress = (int)((rxData[(int)FuncFourIndicies.CellFieldAddressMsb + 4 + i] << 8) |
                            rxData[(int)FuncFourIndicies.CellFieldAddressLsb+4 + i]);
                        /*} else {
                            cellFieldAddress = (int)((rxData[(int)FuncFourIndicies.CellFieldAddressMsb+2 + i] << 8) |
                            rxData[(int)FuncFourIndicies.CellFieldAddressLsb+2 + i]);
                        }*/

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
