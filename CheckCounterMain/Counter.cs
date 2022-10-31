using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckCounterMain
{



    public class Counter
    {        
        private string counterID = String.Empty;
        private string? counterSystem = String.Empty;
        private string? counterType = String.Empty;
        private IList<CounterData> counterData = new List<CounterData>();
        private bool checkResult = true;
        private IList<Error> checkResultErrors = new List<Error>();
        private String checkResultMessage = String.Empty;
        private List<string> _majorParamsHeat = new List<string>() { "Q", "G1", "G2", "T1", "T2"};
        private List<string> _majorParamsWater = new List<string>() { "V1", "V2", "T1", "T2" };

        // Типы счетчика
        public List<string> CounterTypes = new List<string>{
                "Элтеко",
                "Elteco",
                "КМ",
                "KM",
                "KМ",
                "КM",
                "ТСРВ",
                "ТС",
                "TC",
                "TС",
                "ТC",
                "ВИС.Т"
            };

        // Система счетчика
        public List<string> CounterSystems = new List<string>{
            "Отопление",
            "Горячее водоснабжение",
            "Вентиляция",
            "Индивидуальный тепловой пункт"};
        
        // Наименования параметров счетчика
        public static IDictionary<string, IList<string>> CounterParams = new Dictionary<string, IList<string>>{
            {"потребитель", new List<string>() { "потребител", "организаци" } },
            {"адрес", new List<string>() { "адрес" }},
            {"тип", new List<string>() { "тип т/сч", "тип теплосчётчика", "тепловычислитель" } },
            {"номер", new List<string>() { "номер т/сч", "номер теплосчётчика", "сер.ном" } },
            {"система", new List<string>() { "система" } }
        };

        public string? CounterSystem { get => counterSystem; set => counterSystem = value; }
        public IList<CounterData> CounterData { get => counterData; set => counterData = value; }
        public string? CounterID { get => counterID; set => counterID = value; }
        public string? CounterType { get => counterType; set => counterType = value; }
        public bool CheckResult { get => checkResult; set => checkResult = value; }
        public IList<Error> CheckResultErrors { get => checkResultErrors; set => checkResultErrors = value; }
        public string CheckResultMessage { get => checkResultMessage; set => checkResultMessage = value; }

        public void CheckGV(string _paramName, double _limit)
        {
            var _counterData = this.CounterData.Last();

            if (_counterData.ParamValues.ContainsKey(_paramName + "1") &&
                _counterData.ParamValues.ContainsKey(_paramName + "2"))
            {
                double param1 = _counterData.ParamValues[_paramName + "1"].GetValueOrDefault();
                double param2 = _counterData.ParamValues[_paramName + "2"].GetValueOrDefault();
                if (param1 != 0 && param2 != 0)
                {
                    double paramMin = Math.Min(param1, param2);
                    // п. 8 исключен из перечня ошибок
                    if ((Math.Abs(param1 - param2) / paramMin * 100 >= _limit) && _paramName != "V")
                    {
                        _counterData.CheckResult = false;
                        _counterData.CheckResultError.Add(new Error(_paramName + "Diff"));
                    }
                }

                var T1 = _counterData.ParamValues["T1"];
                var T2 = _counterData.ParamValues["T2"];

                if (T1 != 0 && T2 != 0 && param1 <= 0 && param2 <= 0)
                {
                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(_paramName + "Null"));

                }
            }


        }
        public void CheckPodmes(CounterData _counterData)
        {          

            if (_counterData.ParamValues.ContainsKey("V1") &&
                _counterData.ParamValues.ContainsKey("V2"))
            {
                if (_counterData.ParamValues["V2"] > _counterData.ParamValues["V1"])
                {
                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(ErrorClass.VPodmes));
                }
            }
        }
        
        public void CheckQ(CounterData _counterData)
        {
            if (_counterData.ParamValues.ContainsKey("T1") &&
                _counterData.ParamValues.ContainsKey("T2") &&
                _counterData.ParamValues.ContainsKey("G1") &&
                _counterData.ParamValues.ContainsKey("G2") &&
                _counterData.ParamValues.ContainsKey("Q") )
            {
                if (_counterData.ParamValues["T1"] != 0 &&
                    _counterData.ParamValues["T2"] != 0 &&
                    _counterData.ParamValues["G1"] != 0 &&
                    _counterData.ParamValues["G2"] != 0 &&
                    _counterData.ParamValues["Q"] == 0)
                {

                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(ErrorClass.QNull));
                }
            }
        }

        public void CheckWorkTime()
        {
            var _counterData = this.CounterData.Last();
            var _numberOfDays = this.CounterData.Count() - 1;
            var _TpMin = 24 * 0.5 * _numberOfDays;
            if (_counterData.ParamValues["Tp"] < _TpMin)
            {
                _counterData.CheckResult = false;
                _counterData.CheckResultError.Add(new Error(ErrorClass.WorkTimeMin));
            }

            if (this.CounterData.Where(t => t.ParamValues["Tp"].Equals(0) &&
            Enumerable.Range(_numberOfDays - 2, _numberOfDays).
            Contains(this.CounterData.IndexOf(t))).Count() > 0)
            {
                _counterData.CheckResult = false;
                _counterData.CheckResultError.Add(new Error(ErrorClass.ConnectionLost));
            };
        }

        public bool CheckFill(CounterData _counterData)
        {

            foreach (var pair in _counterData.ParamValues)
            {
                if (pair.Value == 0 && _majorParamsHeat.Contains(pair.Key))
                {
                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(ErrorClass.ValueNull));

                    return true;

                }
            }


            return false;
        }

        public bool CheckConnLost(CounterData _counterData)
        {
            if (this.counterSystem != null)
            {
                List<string> _params = new List<string>();
                if (this.counterSystem.Equals("Отопление") || this.counterSystem.Equals("Вентиляция")
                    || this.counterSystem.Equals("Индивидуальный тепловой пункт"))
                {
                    _params = _majorParamsHeat;
                }
                else
                {
                    _params = _majorParamsWater;
                }

                int _paramsCount = 0;
                foreach (var _param in _params)
                {
                    if (_counterData.ParamValues.Keys.Contains(_param))
                    {
                        if (_counterData.ParamValues[_param] != null)
                        {
                            _paramsCount++;
                        }
                    }

                }

                // Если не считано ни одного важного параметра
                if (_paramsCount == 0)
                {
                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(ErrorClass.ConnectionLost));

                    return true;
                }
                
                //foreach (var pair in _counterData.ParamValues)
                //{
                //    if ((pair.Value == null && _majorParamsHeat.Any(pair.Key.Contains) &&
                //        (this.counterSystem.Equals("Отопление") || this.counterSystem.Equals("Вентиляция"))) ||
                //        (pair.Value == null && _majorParamsWater.Any(pair.Key.Contains) && this.counterSystem.Equals("Горячее водоснабжение")))
                //    {
                //        _counterData.CheckResult = false;
                //        _counterData.CheckResultError.Add(new Error(ErrorClass.ConnectionLost));

                //        return true;

                //    }
                //}
            }


            return false;

        }

        public void CheckTemperatureChange(string _paramName)
        {
            double _value = double.MinValue;
            foreach (var _counterData in this.CounterData)
            {
                if (_counterData.ParamValues.ContainsKey(_paramName) && 
                    _counterData.ParamValues[_paramName] != null)
                {
                    _value = _counterData.ParamValues[_paramName].GetValueOrDefault();
                    break;
                }
            }

            if (_value != double.MinValue)
            {
                var _counter = 0;
                foreach (var _counterData in this.CounterData)
                {
                    if (_counterData.ParamValues.ContainsKey(_paramName) &&
                        _counterData.ParamValues[_paramName] != null)
                    {
                        if (_counter >= 10)
                        {
                            this.CounterData.Last().CheckResult = false;
                            this.CounterData.Last().CheckResultError.Add(new Error(_paramName + "NotChange"));
                            break;
                        }
                        if (_value == _counterData.ParamValues[_paramName].GetValueOrDefault())
                        {
                            _counter++;
                        }
                        else
                        {
                            _counter = 1;
                            _value = _counterData.ParamValues[_paramName].GetValueOrDefault();
                        }
                    }
                }




            }


        }
        public void CheckTemperatureJump(CounterData _counterData1, CounterData _counterData2, string _paramName, double _limit)
        {

            if (_counterData1.ParamValues.ContainsKey(_paramName) && _counterData2.ParamValues.ContainsKey(_paramName))
            {

                if (Math.Abs(_counterData1.ParamValues[_paramName].GetValueOrDefault() - _counterData2.ParamValues[_paramName].GetValueOrDefault()) >= _limit)
                {

                    _counterData1.CheckResult = false;
                    _counterData1.CheckResultError.Add(new Error(_paramName + "Jump"));
                    _counterData2.CheckResult = false;
                    _counterData2.CheckResultError.Add(new Error(_paramName + "Jump"));
                }
            }
        }

        public void CheckTemperatureDiff(CounterData _counterData, string _paramName, double _limit)
        {
            if (_counterData.ParamValues.ContainsKey(_paramName + "1") && _counterData.ParamValues.ContainsKey(_paramName + "2"))
            {
                if (Math.Abs(_counterData.ParamValues[_paramName + "1"].GetValueOrDefault() - (double)_counterData.ParamValues[_paramName + "2"].GetValueOrDefault()) < _limit)
                {

                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(ErrorClass.TDiff));
                }
            }
        }



    }
}
