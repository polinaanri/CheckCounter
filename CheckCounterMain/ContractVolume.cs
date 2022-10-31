using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckCounterMain
{
    public class ContractVolume
    {
        private DateTime mounth;
        private double value = 0;
        private IList<ContractVolumeDate> contractVolumesDates = new List<ContractVolumeDate>();

        public ContractVolume(DateTime _mounth, double _value)
        {
            this.mounth = _mounth;
            this.value = _value;

            var _daysInMounth = GetDates(_mounth);
            foreach (var _date in _daysInMounth)
            {
                var diff = (_date - _daysInMounth.First()).TotalDays+1;
                this.contractVolumesDates.Add(new ContractVolumeDate(_date,
                    _value/_daysInMounth.Count*diff));
            }
        }
        public static List<DateTime> GetDates(DateTime _date)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(_date.Year, _date.Month))  
                             .Select(day => new DateTime(_date.Year, _date.Month, day)) 
                             .ToList(); // Load dates into a list
        }

        public DateTime Mounth { get => mounth; set => mounth = value; }
        public double Value { get => value; set => this.value = value; }
        public IList<ContractVolumeDate> ContractVolumesDates { get => contractVolumesDates; set => contractVolumesDates = value; }
    }

    public class ContractVolumeDate
    {
        private DateTime date;
        private double value = 0;

        public ContractVolumeDate(DateTime _date, double _value)
        {
            this.date = _date;
            this.value = _value;
        }

        public DateTime Date { get => date; set => date = value; }
        public double Value { get => value; set => this.value = value; }
    }
}
