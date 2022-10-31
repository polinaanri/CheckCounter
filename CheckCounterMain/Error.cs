using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace CheckCounterMain
{
    [Serializable]
    public struct ErrorDescription
    {
        public int index;
        public string errorClass;
        public bool major;
        public string errorName;
        public string errorMessage;
        public bool checkedState;
    }
    public enum ErrorClass
    {
        [Description("Не меняется T1")]
        T1NotChange,
        [Description("Не меняется T2")]
        T2NotChange,
        [Description("Скачки T1")]
        T1Jump,
        [Description("Скачки T2")]
        T2Jump,
        [Description("Превышен порог T1-T2")]
        TDiff,
        [Description("Превышен порог G1-G2")]
        GDiff,
        [Description("G меньше или равен 0")]
        GNull,
        [Description("V меньше или равен 0")]
        VNull,
        [Description("Подмес V")]
        VPodmes,
        [Description("T работы меньше минимума")]
        WorkTimeMin,
        [Description("Потеря связи")]
        ConnectionLost,
        [Description("Q равно 0")]
        QNull,
        [Description("Не считаны показания")]
        ValueNull,
        [Description("Превышены лимиты")]
        Excess,
        [Description("Не считаны лимиты")]
        NoLimit
    }


    public class Error
    {
        public static string errorTextDgv = "По адресу обнаружены ошибки в показаниях...";
        public static string errorTypeWarning = "Предупреждение";
        public static string errorTypeCriticalError = "Критическая ошибка";
        public static string errorTypeConLost = "Потеря связи";
        private ErrorClass errorClass;
        private bool major;
        private String errorMessage = String.Empty;
        private String errorName = String.Empty;

        public static IList<ErrorDescription> errorPatterns = new List<ErrorDescription>()
        {
            { new ErrorDescription { index = 0, errorClass = "T1NotChange", errorName = "Не меняется T1", major = false, errorMessage = "Не меняется температура T1.", checkedState = true } },
            { new ErrorDescription { index = 1, errorClass = "T2NotChange", errorName = "Не меняется T2", major = false, errorMessage = "Не меняется температура T2.", checkedState = true } },
            { new ErrorDescription { index = 2, errorClass = "T1Jump", errorName = "Скачки T1", major = false, errorMessage = "Резкие скачки температуры T1.", checkedState = true } },
            { new ErrorDescription { index = 3, errorClass = "T2Jump", errorName = "Скачки T2", major = false, errorMessage = "Резкие скачки температуры T2.", checkedState = true } },
            { new ErrorDescription { index = 4, errorClass = "TDiff", errorName = "Превышен порог T1-T2", major = false, errorMessage = "Превышено максимальное значение разницы температур T1 и T2.", checkedState = true } },
            { new ErrorDescription { index = 5, errorClass = "GDiff", errorName = "Превышен порог G1-G2", major = true, errorMessage = "Превышено значение разницы G.", checkedState = true } },
            { new ErrorDescription { index = 6, errorClass = "GNull", errorName = "G меньше или равен 0", major = true, errorMessage = "Итоговые значения G меньше или равны нулю.", checkedState = true } },
            { new ErrorDescription { index = 8, errorClass = "VNull", errorName = "V меньше или равен 0", major = true, errorMessage = "Итоговые значения V меньше или равны нулю.", checkedState = true } },
            { new ErrorDescription { index = 9, errorClass = "VPodmes", errorName = "Подмес V", major = true, errorMessage = "Обнаружен подмес V.", checkedState = true } },            
            { new ErrorDescription { index = 10, errorClass = "WorkTimeMin", errorName = "T работы меньше минимума", major = true, errorMessage = "Время работы меньше минимального значения.", checkedState = true } },
            { new ErrorDescription { index = 11, errorClass = "ConnectionLost", errorName = "Потеря связи", major = true, errorMessage = "Потеряна связь с прибором.", checkedState = true } },
            { new ErrorDescription { index = 12, errorClass = "QNull", errorName = "Q равно 0", major = false, errorMessage = "Теплопотребление равно нулю.", checkedState = true } },
            { new ErrorDescription{ index = 13, errorClass = "ValueNull", errorName = "Не считаны показания", major = true, errorMessage = "Показания равны нулю.", checkedState = true } },
            { new ErrorDescription { index = 14, errorClass = "Excess", errorName = "Превышены лимиты", major = true, errorMessage = "Превышен лимит по договору.", checkedState = true } },
            { new ErrorDescription { index = 15, errorClass = "NoLimit", errorName = "Не считаны лимиты", major = false, errorMessage = "Не считан лимит по договору.", checkedState = true } },

        };

        public Error(ErrorClass _errorClass)
        {
            errorClass = _errorClass;
            InitializeError();
        }

        public Error(string _errorClass)
        {
            errorClass = (ErrorClass)Enum.Parse(typeof(ErrorClass), _errorClass);
            InitializeError();
        }

        public Error(string _errorClass, string _errorName, bool _major, string _errorMessage)
        {
            errorClass = (ErrorClass)Enum.Parse(typeof(ErrorClass), _errorClass);
            errorName = _errorName;
            major = _major;
            errorMessage = _errorMessage;
        }

        private void InitializeError()
        {
            var errorPattern = UserSettings.userSettings.reportErrorPatterns.Where(e=>e.errorClass.
            Equals(this.errorClass.ToString())).First();

            errorMessage = errorPattern.errorMessage;
            major = errorPattern.major;
            errorName = errorPattern.errorName;

        }

        /// <summary>
        /// Приведение значения перечисления в удобочитаемый формат.
        /// </summary>
        /// <remarks>
        /// Для корректной работы необходимо использовать атрибут [Description("Name")] для каждого элемента перечисления.
        /// </remarks>
        /// <param name="enumElement">Элемент перечисления</param>
        /// <returns>Название элемента</returns>
        public static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }

        public ErrorClass ErrorClass { get => errorClass; set => errorClass = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
        public string ErrorName { get => errorName; set => errorName = value; }
        public bool Major { get => major; set => major = value; }

    }
}
