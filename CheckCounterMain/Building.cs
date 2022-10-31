using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckCounterMain
{
    [Serializable]
    public struct BuildingReport
    {
        public string number;
        public string address;
        public string error;
    }
    public class Building
    {
        private string? address;
        private IList<Counter> counters = new List<Counter>();
        private IList<Contract> contracts = new List<Contract>();
        private bool checkResult = true;
        public string Address { get => address; set => address = value; }
        public IList<Counter> Counters { get => counters; set => counters = value; }
        public bool CheckResult { get => checkResult; set => checkResult = value; }
        public IList<Contract> Contracts { get => contracts; set => contracts = value; }

        // Проверка на превышение лимита ТЭ/ГВС
        // counterSystem - список проверямых видов систем
        // contractSystem - наименование вида договорного объема (ГВС/ТЭ)
        // _paramNames - список наименований параметров для проверки
        public void CheckContractVolumes(string[]counterSystem, string contractSystem, string[] _paramNames)
        {            
            var _counters = counters.ToList().Where(c => counterSystem.Contains(c.CounterSystem)).ToList();
            if (_counters.Count == 0) return;
            var _contract = Contracts.ToList().Find(c => c.CounterSystem.Equals(contractSystem));


            IList<DateTime> _dates = new List<DateTime>();

            _counters.ForEach(l => l.CounterData.ToList().ForEach(d => _dates.Add(d.Date)));
            var _datesUnique = _dates.Distinct().ToList();

            foreach (var _date in _datesUnique)
            {
                if (!_date.Equals(DateTime.MinValue))
                    CheckContractVolume(counterSystem, _counters, _contract, _paramNames, _date);
            }

        }

        // Проверка на превышение лимита ТЭ/ГВС на дату
        // counterSystem - список проверямых видов систем
        // _counters - список проверяемых счетчиков для данного адреса
        // _contract - найденные договорные объемы для данного адреса
        // _paramNames - список наименований параметров для проверки
        // _date - дата проверки
        public void CheckContractVolume(string[] counterSystem, IList<Counter> _counters, 
            Contract _contract, string[] _paramNames, DateTime _date)
        {


            ContractVolume _contractVolume = null;
            if (_contract != null)
            {
                _contractVolume = _contract.ContractVolumes.
                    Where(v => v.Mounth.Equals(new DateTime(_date.Year, _date.Month, 1))).FirstOrDefault();
            }

            ContractVolumeDate _contractVolumeDate = null;
            if (_contractVolume != null)
            {
                _contractVolumeDate = _contractVolume.ContractVolumesDates.
                    Where(v => v.Date.Equals(_date)).First();
            }

            string _error = String.Empty;

            if (_contract == null || _contractVolume == null || _contractVolumeDate == null || _contractVolumeDate.Value == 0)
            {
                _error = "NoLimit";

            }
            else 
            {
                double? _sum = 0;

                // найти сумму показаний счетчика на день
                foreach (var counter in _counters)
                {
                    // Итоговые значения dV или Q
                    var _counterData = counter.CounterData.Where(c => c.Date.Equals(_date)).First();
                    var _param = String.Empty;


                    if (_paramNames.Count() > 1 && counterSystem.Contains("Горячее водоснабжение"))
                    {
                        if (_counterData.ParamValues.ContainsKey(_paramNames[0]) &&
                            _counterData.ParamValues.ContainsKey(_paramNames[1]))
                        {
                            _sum += _counterData.ParamValues[_paramNames[0]] - _counterData.ParamValues[_paramNames[1]];
                        }

                    }
                    else if(_paramNames.Count() > 0)
                    {
                        if (_counterData.ParamValues.ContainsKey(_paramNames[0]))
                        {
                            _sum += _counterData.ParamValues[_paramNames[0]];
                        }

                    }


                        
                }

                if (_sum > _contractVolumeDate.Value) _error = "Excess";

            }

            if (_error != String.Empty)
            {
                foreach (var counter in _counters)
                {
                    var _counterData = counter.CounterData.Where(c => c.Date.Equals(_date)).First();
                    _counterData.CheckResult = false;
                    _counterData.CheckResultError.Add(new Error(_error));
                }
            }


        }

    }
}
