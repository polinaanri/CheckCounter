namespace CheckCounterMain
{
    public class CounterData
    {
        private DateTime date;
        private bool checkResult = true;
        private IList<Error> checkResultError = new List<Error>();
        private IDictionary<string, double?> paramValues = new Dictionary<string, double?>();

        // Наименования показателей по типам счетчиков
        // Основные - Q, G1, G2, V1, V2, T1, T2, Tp
        private IDictionary<string, string> paramNames = new Dictionary<string, string>()
        {
            // Элтеко
            // Отопление, вентиляция
            {   "Eц-цо,Гкал", "Q"},
            {   "Mц,т", "G1"},
            {   "Mцо,т", "G2"},
            {   "dMц-цо,т", "dG1"},
            {   "dMцо-ц,т", "dG2"},
            {   "Tц,°C", "T1"},
            {   "Tцо,°C", "T2"},
            {   "pц,МПа", "P1"},
            {   "pцо,МПа", "P2"},
            {   "Chц-цо,час", "Tp"},

            // ГВС
            {   "Eг,Гкал", "Q"},
            {   "Mг,т", "M1"},
            {   "Mго,т", "M2"},
            {   "Vг,м3", "V1"},
            {   "Vго,м3", "V2"},
            {   "dVг-го,м3", "dV"},
            {   "Tг,°C", "T1"},
            {   "Tго,°C", "T2"},
            {   "Tх,°C", "Tx"},
            {   "pг,МПа", "P1"},
            {   "pго,МПа", "P2"},
            {   "Chг,час", "Tp"},

            // КМ-5
            // Отопление, вентиляция
            {   "Q, Гкал", "Q"},
            {   "Q", "Q"},
            {   "M1", "G1"},
            {   "M2", "G2"},
            {   "M1-M2утечка", "dG1"},
            {   "M1-M2разбор", "dG1"},
            {   "M2-M1подмес", "dG2"},
            {   "t1", "T1"},
            {   "t1, °C", "T1"},
            {   "t2", "T2"},
            {   "t2, °C", "T2"},
            {   "t1-t2", "Tc"},
            {   "P1", "P1"},
            {   "P2", "P2"},
            {   "НерабTн", "Tн"},
            {   "РаботTр", "Tp"},

            // ГВС
            {   "V1", "V1"},
            {   "V1, м3", "V1"},
            {   "V2", "V2"},
            {   "V2, м3", "V2"},
            {   "V1-V2разбор", "dV"},
            {   "tхв", "Tx"},

            // ТСРВ
            // Отопление, вентиляция
            {   "Q,Гкал", "Q"},
            {   "M1,т", "G1"},
            {   "M2,т", "G2"},
            {   "M1-M2,т", "dG1"},
            {   "M2-M1,т", "dG2"},
            {   "t1,°C", "T1"},
            {   "t2,°C", "T2"},
            {   "t1-t2,°C", "Tc"},
            {   "P1,МПа", "P1"},
            {   "P2,МПа", "P2"},
            {   "Tр,ч", "Tp"},

            // ГВС
            {   "V1,м3", "V1"},
            {   "V2,м3", "V2"},
            {   "V1-V2,м3", "dV"},

            // ВИС.Т ТС
            // Отопление, вентиляция
            {   "Qтеп[Гкал]", "Q"},
            {   "Gпод[тонн]", "G1"},
            {   "Gобр[тонн]", "G2"},
            {   "Gп[тонн]", "Gп"},
            {   "Gпод-Gобр[тонн]", "dG1"},
            {   "tпод[°C]", "T1"},
            {   "tобр[°C]", "T2"},
            {   "pпод[ат]", "P1"},
            {   "pобр[ат]", "P2"},
            {   "Tнар[час]", "Tp"},

            // ГВС
            {   "Vпод[м3]", "V1"},
            {   "Vобр[м3]", "V2"},
            {   "Vпод-Vобр[м3]", "dV"},
            {   "tп[°C]", "Tп"}
        };


        public DateTime Date { get => date; set => date = value; }
        public bool CheckResult { get => checkResult; set => checkResult = value; }
        public IDictionary<string, double?> ParamValues { get => paramValues; set => paramValues = value; }
        public IDictionary<string, string> ParamNames { get => paramNames; set => paramNames = value; }
        public IList<Error> CheckResultError { get => checkResultError; set => checkResultError = value; }



    }
}
