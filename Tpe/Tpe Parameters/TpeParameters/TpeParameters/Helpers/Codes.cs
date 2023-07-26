using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TpeParameters.Helpers
{

    public enum TpeXmlErrorCodes
    {
        NoErr = 0,
        TableNotFound = 1
    }

    /// <summary>
    /// Тип группы
    /// </summary>
    public enum GroupTypes
    {
        None = 0,
        Show = 1,
        User,
        Factory,
        Test,
        Command,
        Hide,
        Other
    }

    /// <summary>
    /// Тип параметра
    /// </summary>
    public enum ParamTypes
    {
        None = 0,
        Public = 1,
        Factory,
        Reserved,
        Hide
    }

    public enum ParamValueTypes
    {
        None = 0,
        Uns = 1,                                        // Беззнаковый int
        Int,                                            // Знаковый int
        Bin,
        Union,                                          // Редактируемый union
        List,                                           // Нередактируемый union - список
        Enum,                                           // Строковое перечисление (не побитовое) или enum
        Date,                                           // Дата
        Time,                                           // Время
    }

    public enum ParamAppointments
    {
        Regular = 0,
        ProductYear,
        FactoryNumber,
        Fault,
        Date,
        Time,
        Seconds,
        Status,
        StatusDigOut,
        LogCmdControlWord,
        Position
    }
}
