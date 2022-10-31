using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;
using OfficeOpenXml;

namespace CheckCounterMain
{

    public partial class fMain : Form
    {
        List<Consumer> consumerList = new List<Consumer>();
        List<Contract> contractList = new List<Contract>();

        List<IList<string>> _data = new List<IList<string>>();
        DataTable _volumes = new DataTable();
        DataTable _report = new DataTable();
        DataTable _settings = new DataTable();
        String filePath = String.Empty;
        String folderPath = String.Empty;
        String contractPath = String.Empty;
        IList<string> _exceptions = new List<string>();
        int maxCol = 100;
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        #region Initialization
        public fMain()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            InitializeDataGridView();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }

        private void InitializeDataGridView()
        {
            dgvReport.AutoGenerateColumns = true;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReport.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvReport.BorderStyle = BorderStyle.Fixed3D;
            dgvReport.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvReport.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvReport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            

            DataGridViewTextBoxColumn classColumn = new DataGridViewTextBoxColumn();
            classColumn.Name = "Класс ошибки";
            classColumn.DataPropertyName = "cnClass";
            classColumn.ReadOnly = true;
            classColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            classColumn.Visible = false;
                      
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.Name = "Краткое название";
            nameColumn.DataPropertyName = "cnName";
            nameColumn.ReadOnly = false;
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewCheckBoxColumn typeColumn = new DataGridViewCheckBoxColumn();

            //foreach (var e in Enum.GetValues(typeof(ErrorType)).Cast<ErrorType>().ToList())
            //    typeColumn.Items.Add(Error.GetDescription(e));

            typeColumn.Name = "Важная ошибка";
            typeColumn.DataPropertyName = "cnType";
            typeColumn.ReadOnly = false;
            typeColumn.DefaultCellStyle.SelectionBackColor = Color.White;
            typeColumn.DefaultCellStyle.BackColor = Color.White;
           
            //typeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            //typeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            textColumn.Name = "Текст ошибки";
            textColumn.DataPropertyName = "cnText";
            textColumn.ReadOnly = false;
            textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //typeColumn.AutoComplete = true;
            typeColumn.FlatStyle = FlatStyle.Standard;

            dgvSettings.Columns.Add(classColumn);
            dgvSettings.Columns.Add(nameColumn);
            dgvSettings.Columns.Add(typeColumn);
            dgvSettings.Columns.Add(textColumn);

            //dgvSettings.AutoGenerateColumns = true;
            dgvSettings.BorderStyle = BorderStyle.Fixed3D;
            dgvSettings.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvSettings.GridColor = Color.DarkGray;
            dgvSettings.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvSettings.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSettings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvSettings.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;



        }

        #endregion

        #region Read files
        //Прочитать все данные в заданном файле
        // filePath - путь к файлу
        public List<IList<string>> ReadCSV(string _filePath)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding("windows-1251");

            var _file = File.ReadLines(_filePath, encoding);
            var dt = new List<IList<string>>();

            var i = 0;

            _file.Select(x => x.Split(';')).ToList().ForEach(delegate (string[] line)
            {

                Array.Resize(ref line, maxCol);
                dt.Add(line.ToList());
            });


            return dt;
        }

        // Чтение договорных объемов
        // _filePath - путь к файлу
        public List<Contract> readContractVolumes(string _filePath, IList<string> exceptions)
        {
            FileInfo fi = new FileInfo(_filePath);
            var _buildings = consumerList.ToList().SelectMany(c => c.Buildings.Select(b => b)).ToList();

            // Список договоров (по адресам)
            List <Contract> contracts = new List<Contract>();            

            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                CultureInfo culture = new CultureInfo("ru-RU");
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                foreach (var Worksheet in excelPackage.Workbook.Worksheets.Take(2))
                {
                    //ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[sheet];

                    var iMounth = 1;

                    var _counterSystem = Contract.CounterSystems.Find(t => Worksheet.Name.Contains(t));

                    if (_counterSystem == null || _counterSystem.Equals(String.Empty))
                    {
                        exceptions.Add("Неверное название листа в файле договорных объемов.");
                        continue;
                    }

                    var rowsCount = Worksheet.Dimension.End.Row;
                    var colsCount = Worksheet.Dimension.End.Column;

                    IDictionary<int, DateTime> _mounths = new Dictionary<int, DateTime>();

                    for (int i = 1; i <= rowsCount; i++)
                    {
                        //var row = dt.NewRow();
                        for (int j = 1; j <= colsCount; j++)
                        {
                            // Получить первую ячейку в строке 
                            if (Worksheet.Cells[i, j] == null || Worksheet.Cells[i, j].Value == null)
                                continue;
                            var _curString = Worksheet.Cells[i, j].Value.ToString();


                            DateTime dateTimeValue = DateTime.MinValue;

                            if (DateTime.TryParseExact(_curString, "MMMM",
                                culture, DateTimeStyles.None, out dateTimeValue))
                            {
                                _mounths.Add(j, dateTimeValue);
                            }

                            if (_mounths.Count.Equals(12))
                            {
                                break;
                            }

                        }
                        if (_mounths.Count.Equals(12))
                        {
                            iMounth = i;
                            break;
                        }
                    }

                    if (!_mounths.Count.Equals(12))
                    {
                        exceptions.Add("Неверное количество месяцев в файле договорных объемов на листе " + 
                            Worksheet.Name + ".");
                    }

                    for (int i = iMounth + 1; i <= rowsCount; i++)
                    {
                        if (Worksheet.Cells[i, 1] == null || Worksheet.Cells[i, 1].Value == null)
                            continue;
                        var _address = Worksheet.Cells[i, 1].Value.ToString();
                        var _building = _buildings.Where(b => b.Address.ToLower().Contains(_address.ToLower())).FirstOrDefault();


                        var contract = new Contract();

                        if (_building != null)
                            _building.Contracts.Add(contract);

                        contract.CounterSystem = _counterSystem;
                        contract.Address = _address;
                        contract.Building = _building;

                        foreach (int j in _mounths.Keys)
                        {
                            double _value = 0;


                            if (Worksheet.Cells[i, j] != null && Worksheet.Cells[i, j].Value != null)
                            {
                                Double.TryParse(Worksheet.Cells[i, j].Value.ToString(), out _value);
                            }

                            var newVolume = new ContractVolume(_mounths[j], _value);
                            contract.ContractVolumes.Add(newVolume);

                        }

                        contracts.Add(contract);

                    }
                }
            }

            return contracts;
        }

        // Чтение показаний счетчиков по потребителям
        public List<Consumer> FillData(IList<IList<string>> dt, IList<string> exceptions)
        {

            // Список потребителей
            List<Consumer> consumers = new List<Consumer>();
            int i = 0;
            while (i < dt.Count)
            {
                int j = 0;

                while (j < dt[i].Count)
                {

                    var curString = GetCell(dt, i, j);

                    // Чтение данных посуточной ведомости/месячного протокола
                    try
                    {
                        if (curString != null && (curString.Equals("Потребитель:") // Элтеко, ТСРВ
                            || curString.Equals("Организация:")                    // KM-5
                            || curString.Equals("Название потребителя")))          // ВИСТ
                        {

                            curString = GetCounterData(dt, i, j, Counter.CounterParams["потребитель"]);
                            if (curString == null || curString.Equals(String.Empty))
                            {
                                exceptions.Add("Не определен потребитель в строке " + i.ToString());
                                continue;
                            }

                            Consumer consumer;
                            var consumersSearch = consumers.Where(t => t.Name.Equals(curString, StringComparison.OrdinalIgnoreCase));
                            if (consumersSearch.Count() == 0)
                            {
                                consumer = new Consumer();
                                consumer.Name = curString;
                                consumers.Add(consumer);

                            }
                            else
                            {
                                consumer = consumersSearch.FirstOrDefault();
                            }

                            if (consumer != null)
                            {
                                var buildings = consumer.Buildings;

                                // чтение адреса потребителя

                                curString = GetCounterData(dt, i, j, Counter.CounterParams["адрес"]);

                                if (curString == null || curString.Equals(String.Empty))
                                {
                                    exceptions.Add("Не определен адрес потребителя " + consumer.Name);
                                    continue;
                                }

                                Building building;
                                var consumerBuildings = buildings.Where(t => t.Address.Equals(curString, StringComparison.OrdinalIgnoreCase));
                                if (consumerBuildings.Count() == 0)
                                {
                                    building = new Building();
                                    building.Address = curString;
                                    buildings.Add(building);
                                }
                                else
                                {
                                    building = consumerBuildings.FirstOrDefault();
                                }


                                // чтение данных о счетчике
                                // чтение типа счетчика

                                curString = GetCounterData(dt, i, j, Counter.CounterParams["тип"]);

                                if (curString == null || curString.Equals(String.Empty))
                                {
                                    exceptions.Add("Не определен тип теплосчетчика по адресу " + building.Address);
                                }

                                var counter = new Counter();
                                counter.CounterType = counter.CounterTypes.Find(t => curString.Contains(t));

                                if (counter.CounterType == null)
                                {
                                    exceptions.Add("Не определен вид системы по адресу " + building.Address);
                                }

                                // чтение номера счетчика

                                curString = GetCounterData(dt, i, j, Counter.CounterParams["номер"]);
                                if (curString == null || curString.Equals(String.Empty))
                                {
                                    exceptions.Add("Не определен номер теплосчетчика по адресу " + building.Address);
                                }
                                counter.CounterID = curString;


                                // чтение вида системы счетчика

                                curString = GetCounterData(dt, i, j, Counter.CounterParams["система"]);

                                if (curString == null || curString.Equals(String.Empty))
                                {
                                    exceptions.Add("Не определен вид системы по адресу " + building.Address);
                                }

                                    //var _system = curString.Split(":")[1].Trim();
                                counter.CounterSystem = counter.CounterSystems.Find(t => curString.Contains(t));

                                if (counter.CounterSystem == null)
                                {
                                    exceptions.Add("Не определен вид системы по адресу " + building.Address);
                                }


                                // чтение показаний счетчика
                                DateTime _date = new DateTime();
                                while (!DateTime.TryParse(curString, out _date))
                                {
                                    i++;
                                    curString = GetCell(dt, i, 0);
                                }

                                // Запомним строку с названиями
                                var i_param_names = i - 1;

                                // пока в первом столбце дата показания
                                while (DateTime.TryParse(curString, out _date) ||
                                    curString.Contains("Итого"))
                                {
                                    var _counterData = new CounterData();
                                    _counterData.Date = curString.Contains("Итого") ?
                                        DateTime.MinValue :
                                        _date;

                                    // обработаем параметры из таблицы показаний
                                    j = 1;

                                    while (j < dt[i].Count)
                                    {
                                        // Считаем название параметра
                                        String _paramName = String.Empty;
                                        var _param = GetCell(dt, i_param_names, j);

                                        if (_param.Equals(String.Empty))
                                        {
                                            _param = GetCell(dt, i_param_names - 1, j);
                                        }

                                        _counterData.ParamNames.TryGetValue(_param, out _paramName);
                                        if (_paramName != null && _paramName != String.Empty)
                                        {
                                            var _paramValue = String.Empty;
                                            if (counter.CounterType != null && (
                                                counter.CounterType.Equals("ТС") ||
                                                counter.CounterType.Equals("TC") ||
                                                counter.CounterType.Equals("TС") ||
                                                counter.CounterType.Equals("ТC") ||
                                                counter.CounterType.Equals("ВИС.Т")) &&
                                                !curString.Contains("Итого"))
                                            {
                                                var _j = j + 1;
                                                while (!double.TryParse(_paramValue.Replace(".", ","), out _) && _paramValue.Equals(String.Empty) && _j - j <= 3)
                                                {
                                                    _paramValue = GetCell(dt, i, _j++);
                                                }
                                            }
                                            else
                                                _paramValue = GetCell(dt, i, j);

                                            // Если значение отсутствует, заполним null
                                            if (_paramValue == null || _paramValue == String.Empty)
                                            {
                                                _counterData.ParamValues.Add(_paramName, null);
                                            }
                                            else
                                            {
                                                //Если число с точкой, заменим её на запятую
                                                try
                                                {
                                                    if (_paramValue.Contains("."))
                                                        _paramValue = _paramValue.Replace(".", ",");
                                                    double _value = Convert.ToDouble(_paramValue);
                                                    _counterData.ParamValues.Add(_paramName, _value);
                                                }
                                                catch
                                                {
                                                    _counterData.ParamValues.Add(_paramName, null);
                                                }
                                            }

                                        }
                                        j++;
                                    }

                                    counter.CounterData.Add(_counterData);

                                    // Обработаем следующую дату
                                    i++;
                                    curString = GetCell(dt, i, 0);
                                }


                                building.Counters.Add(counter);

                            }
                            else
                            {
                                exceptions.Add("Не определен потребитель в строке " + i.ToString());
                            }
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex.Message);
                    }
                    j++;

                }
                i++;
            }


            return consumers;
        }
        
        #endregion

        #region Process and check files

        // Проверка показаний счетчиков по потребителям
        // consumers - список потребителей 
        public void CheckConsumers(IList<Consumer> consumers, IList<string> exceptions)
        {
            if (consumers == null || consumers.Count == 0)
            {
                exceptions.Add("Не считаны потребители. Неверный формат файла.");
            }
            foreach (var consumer in consumers)
            {
                if (consumer == null || consumer.Buildings.Count == 0)
                {
                    exceptions.Add("Не считаны адреса потребителя " + consumer.Name);
                }
                foreach (var building in consumer.Buildings)
                {
                    var _counters = building.Counters;
                    if (_counters == null || _counters.Count == 0)
                    {
                        exceptions.Add("Не считаны счетчики по адресу " + building.Address);
                    }

                    foreach (var _counter in _counters)
                    {
                        var _counterData = _counter.CounterData;
                        if (_counterData == null || _counterData.Count == 0)
                        {
                            exceptions.Add("Не считаны показания счетчика " + _counter.CounterID);
                            continue;
                        }

                        _counterData.ToList().ForEach(delegate (CounterData _cData)
                        {
                            _cData.CheckResult = true;
                            _cData.CheckResultError.Clear();
                        });

                        for (int i = 0; i < _counterData.Count - 1; i++)
                        {
                            // 12. Проверка на потерю связи
                            try
                            {
                                bool result_con = _counter.CheckConnLost(_counterData[i]);
                                if (result_con)
                                    continue;
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка на заполненность параметров: " + ex.Message);
                                continue;
                            }

                            // 14. Проверка на заполненность параметров
                            try
                            {
                                bool result_con = _counter.CheckFill(_counterData[i]);
                                if (result_con)
                                    continue;
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка на заполненность параметров: " + ex.Message);
                                continue;
                            }
                            
                            // 2. Проверка на скачки температуры T1
                            try
                            {
                                if (i >= 1)
                                {
                                    _counter.CheckTemperatureJump(_counterData[i], _counterData[i - 1], "T1", tbTz.Value);
                                }
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка температуры T1: " + ex.Message);
                            }

                            // 3. Проверка на изменяемость температуры T2
                            // 4. Проверка на скачки температуры T2
                            try
                            {
                                if (i >= 1)
                                {
                                    _counter.CheckTemperatureJump(_counterData[i], _counterData[i - 1], "T2", tbTz.Value);
                                }
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка температуры T2: " + ex.Message);
                            }


                            // 5. Превышен порог T1-T2
                            try
                            {
                                _counter.CheckTemperatureDiff(_counterData[i], "T", tbTy.Value);
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка разницы температур: " + ex.Message);
                            }


                            // 13. Проверка на Q равно 0
                            try
                            {
                                _counter.CheckQ(_counterData[i]);
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка теплопотребления Q: " + ex.Message);
                            }

                            // 9. Проверка на V
                            try
                            {
                                _counter.CheckPodmes(_counterData[i]);
                            }
                            catch (Exception ex)
                            {
                                exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка на подмес V: " + ex.Message);
                            }

                        }

                        // 1. Проверка на изменяемость температуры T1
                        try
                        {
                            _counter.CheckTemperatureChange("T1");
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка изменения T1: " + ex.Message);
                        }
                        // 3. Проверка на изменяемость температуры T2
                        try
                        {
                            _counter.CheckTemperatureChange("T2");
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка изменения T2: " + ex.Message);
                        }

                        // 6. Превышен порог G1-G2
                        // 7. G меньше или равен 0
                        try
                        {
                            _counter.CheckGV("G", tbGx.Value);
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка массы G: " + ex.Message);
                        }

                        // 10. V меньше или равен 0
                        try
                        {
                            _counter.CheckGV("V", 4);
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка объема ГВС V: " + ex.Message);
                        }

                        // 11. T работы меньше минимума
                        try
                        {
                            _counter.CheckWorkTime();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Add("По адресу " + building.Address +
                                    "по счетчику " + _counter.CounterID +
                                    " не пройдена проверка времени наработки: " + ex.Message);
                        }
                    }



                    // 16. Превышение объема ГВС
                    try
                    {
                        building.CheckContractVolumes(new string[] { "Горячее водоснабжение" },
                            "ГВС", new string[] { "V1", "V2" });
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add("По адресу " + building.Address +
                                    " не пройдена проверка превышения порога ГВС: " + ex.Message);
                    }

                    // 17. Превышение объема ТЭ
                    try
                    {
                        building.CheckContractVolumes(new string[] { "Отопление", "Вентиляция", "Индивидуальный тепловой пункт" },
                            "ТЭ", new string[] { "Q" });
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add("По адресу " + building.Address +
                                    " не пройдена проверка првышения порога ТЭ: " + ex.Message);
                    }

                    building.Counters.ToList().ForEach(delegate (Counter _counter)
                    {
                        _counter.CheckResult = _counter.CounterData.Where(t => t.CheckResult == false).Count() == 0;

                        IList<Error> _errors = new List<Error>();
                        _counter.CounterData.Where(t => t.CheckResult == false).
                            ToList().
                            ForEach(x => _errors.ToList().AddRange(x.CheckResultError));
                        _counter.CheckResultErrors = _errors;
                    });

                    building.CheckResult = building.Counters.Where(t => t.CheckResult == false).Count() > 0;


                }
                consumer.CheckResult = consumer.Buildings.Where(t => t.CheckResult == false).Count() > 0;

            }


        }

        // Метод, вызываемый при нажатии "Проверить"
        // backgroundWorker - рабочий процесс 
        private void ProcessFiles(BackgroundWorker backgroundWorker)
        {
            string[] _files;
            if (!folderPath.Equals(String.Empty))
            {
                _files = Directory.GetFiles(folderPath, "*.csv",
                SearchOption.AllDirectories);
            }
            else { _files = new string[] { filePath }; }

            var numFiles = _files.Length;

            var _sb = new StringBuilder();

            int _reportedProgress = 0;

            foreach (var _file in _files)
            {
                // Чтение файла формата csv
                try
                {
                    _data.AddRange(ReadCSV(_file));
                    _reportedProgress += (100 / numFiles) / 5;
                    if (backgroundWorker != null)
                    {
                        backgroundWorker.ReportProgress(_reportedProgress, DateTime.Now.ToString("G") + ": Прочитали файл " +
                            _file + "\r\n");
                    }
                }
                catch (Exception ex)
                {
                    var _message = DateTime.Now.ToString("G") + ": Ошибка чтения файла " +
                        _file + " " +
                        ex.Message + "\r\n";
                    if (backgroundWorker != null)
                    {
                        backgroundWorker.ReportProgress(100, _message);
                    }
                    throw new Exception(_message, ex);
                }
            }

            // Преобразование считанных данных в структуру
            try
            {
                consumerList = FillData(_data, _exceptions);
                _reportedProgress += 20;
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(_reportedProgress, DateTime.Now.ToString("G") + ": Разобрали показания. \r\n");
                }
            }
            catch (Exception ex)
            {
                var _message = DateTime.Now.ToString("G") + ": Ошибка при разборе показаний. " +
                    ex.Message + "\r\n";
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(100, _message);
                }

                _sb.AppendLine(_message);
                _exceptions.ToList().ForEach(ex => _sb.AppendLine(ex));

                throw new Exception(_sb.ToString(), ex);
            }

            // Чтение файла договорных объемов
            if (contractPath != String.Empty && cbReadVolumes.Checked)
            {
                try
                {
                    contractList = readContractVolumes(contractPath, _exceptions);
                    _reportedProgress += 20;
                    if (backgroundWorker != null)
                    {
                        backgroundWorker.ReportProgress(_reportedProgress, DateTime.Now.ToString("G") + ": Прочитали файл " +
                        contractPath + "\r\n");
                    }
                }
                catch (Exception ex)
                {
                    var _message = DateTime.Now.ToString("G") + ": Ошибка чтения файла " +
                        contractPath + " " +
                        ex.Message + "\r\n";
                    if (backgroundWorker != null)
                    {
                        backgroundWorker.ReportProgress(100, _message);
                    }
                    throw new Exception(_message, ex);
                }

            }
            else
            {
                _reportedProgress += 20;
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(_reportedProgress, DateTime.Now.ToString("G") + ": Отсутствует файл договорных объемов.\r\n");

                }
            }


            // Проверка показаний счетчиков
            try
            {
                CheckConsumers(consumerList, _exceptions);
                _reportedProgress += 20;
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(_reportedProgress, DateTime.Now.ToString("G") + ": Проверили счетчики. \r\n");
                }
            }
            catch (Exception ex)
            {
                var _message = DateTime.Now.ToString("G") + ": Ошибка при проверке показаний " +
                    ex.Message + "\r\n";
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(100, _message);
                }

                _sb.AppendLine(_message);
                _exceptions.ToList().ForEach(ex => _sb.AppendLine(ex));

                throw new Exception(_sb.ToString(), ex);
            }

            // Формирование отчета
            try
            {

                _report = MakeReport(consumerList);
                _reportedProgress += 20;
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(_reportedProgress, DateTime.Now.ToString("G") + ": Собрали отчет. \r\n");
                }
            }
            catch (Exception ex)
            {
                var _message = DateTime.Now.ToString("G") + ": Ошибка при формировании отчета " +

                    ex.Message + "\r\n";
                if (backgroundWorker != null)
                {
                    backgroundWorker.ReportProgress(100, _message);
                }

                _sb.AppendLine(_message);
                _exceptions.ToList().ForEach(ex => _sb.AppendLine(ex));

                throw new Exception(_sb.ToString(), ex);
            }


        }
        private void PrepareAndRunProcessFiles()
        {
            PrepareDataToProcess();
            PrepareFormToProcess();
            backgroundWorker1.RunWorkerAsync();
        }

        private void PrepareDataToProcess()
        {
            _exceptions = new List<string>();
            consumerList = new List<Consumer>();
            contractList = new List<Contract>();
            _data = new List<IList<string>>();


        }
        private void PrepareFormToProcess()
        {
            pnResult.Visible = false;
            pnProgress.Visible = true;
            tbLog.Clear();
            tbExceptions.Clear();
            pg1.Value = 0;

        }

        #endregion

        #region Get data
        // Получить значение в ячейке таблицы 
        // dt - таблица
        // row - индекс строки
        // col - индекс столбца
        private string GetCell(IList<IList<string>> dt, int row, int col)
        {
            //DataRow _row = dt.Rows[row];
            //DataColumn _column = dt.Columns[col];
            //var _cell = _row[_column];           
                
            if (dt[row][col] != null)
                return dt[row][col].ToString().Trim();
            else
                return String.Empty;
        }
        // Получить данные о счетчике 
        // dt - таблица
        // row - индекс строки
        // col - индекс столбца
        // paramName - имя параметра
        // headerRowSize - размер "шапки"
        private string GetCounterData(IList<IList<string>> dt, int row, int col, IList<string> paramName, int headerRowSize = 10)
        {

            var maxRow = dt.Count;
            //var maxCol = dt.Columns.Count;

            var _row = row;
            var _col = -1;

            // Найдем ячейку с наименованием парметра
            while (_col == -1 && _row < row + headerRowSize)
            {

                _col = dt[_row].ToList().FindIndex(delegate (string line)
                {
                    if (line != null)
                        return paramName.ToList().Any(p=>line.ToLower().Contains(p.ToLower()));
                    else
                        return false;
                });
                _row++;

            }

            _row--;

            // Проверим данные в найденной ячейке

            if (dt[_row][_col] != null && dt[_row][_col].Contains(":"))
            {
                var _split = dt[_row][_col].ToString().Split(":");
                if (_split.Length > 1)
                {
                    var _value = _split[1].Trim();
                    if (!_value.Equals(String.Empty))
                        return _value;
                }
            }
            _col++;
            var _paramValue = GetCell(dt, _row, _col);

            // Двигаемся вправо, пока ячейка пустая
            while (_col < maxCol - 1 && (_paramValue == null || _paramValue.Equals(String.Empty)))
            {
                _col++;
                _paramValue = GetCell(dt, _row, _col);
            }



            return _paramValue;
        }

        #endregion

        #region Make report

        //Сформировать отчет по ошибкам
        // consumers - список потребителей
        // setReminder - краткое наименование ошибки
        public DataTable MakeReport(IList<Consumer> consumers, bool setReminder = true)
        {
            DataTable _report = new DataTable();
            _report.Columns.Add("N");
            _report.Columns.Add("Адрес");
            _report.Columns.Add("Описание ошибки");

            if (setReminder)
            {
                _report.Columns.Add("Раскрыта");
                _report.Columns[3].DataType = typeof(bool);
            }


            var j = 1;

            foreach (var consumer in consumers)
            {
                for (int i = 0; i < consumer.Buildings.Count; i++)
                {
                    var building = consumer.Buildings[i];
                    var row = _report.NewRow();
                    row[0] = j++;
                    row[1] = building.Address;
                    

                    var _counters = building.Counters;

                    foreach (var counter in _counters)
                    {


                        var _errorTextCounter = new StringBuilder();
                        counter.CounterData.Where(n => n.CheckResult == false).ToList().
                        ForEach(delegate (CounterData y)
                        {
                            //var _checkResult = ;
                            foreach (var e in y.CheckResultError)
                            {
                                var _errorPattern = UserSettings.userSettings.reportErrorPatterns.
                                Where(p => p.errorClass.Equals(e.ErrorClass.ToString())).First();
                                var errorName = _errorPattern.errorName;
                                var major = _errorPattern.major;
                                var errorMessage = _errorPattern.errorMessage;
                                if (lbErrors.CheckedItems.Cast<String>().ToList().Contains(errorName))
                                {
                                    _errorTextCounter.Append((y.Date == DateTime.MinValue ? "" : y.Date.ToString("dd.MM.yyyy")
                                        + " - ") + errorMessage + "\r\n");
                                }
                            }
                        });




                        counter.CheckResultMessage = _errorTextCounter.ToString();
                    }


                    row[2] = String.Empty;
                    var _errorText = new StringBuilder();
                    building.Counters.ToList().
                        Where(t => t.CheckResult == false && t.CheckResultMessage != String.Empty).
                        ToList().
                        ForEach(r => _errorText.Append(r.CounterID + " - " +
                        r.CounterSystem + ":\r\n" +
                        r.CheckResultMessage + "\r\n"));

                    row[2] = _errorText.ToString().Trim('\r', '\n');
                    if (setReminder)
                    {
                        var _errorTextReminder = building.Counters.
                            Where(t => t.CheckResult == false && t.CheckResultMessage != String.Empty).
                            Count() > 0 ? Error.errorTextDgv + "\n" : "";
                        row[2] = _errorTextReminder + row[2]; //+ _errorText.ToString();
                        row[3] = false;
                    }


                    
                    _report.Rows.Add(row);

                }


            }

            return _report;
        }
        // Обновить отчет на форме
        private void RefreshReport()
        {
            _report = MakeReport(consumerList);
            dgvReport.DataSource = _report;
        }
        // Подготовить форму к отображению отчета
        private void ReportReady()
        {
            dgvReport.DataSource = _report;

            dgvReport.AutoGenerateColumns = true;
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvReport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvReport.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvReport.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            pnResult.Visible = true;
            pnProgress.Visible = false;

            this.AutoSize = false;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
        }

        #endregion

        #region Savers
        // Сохранить настройки формы в хранилище
        private void SaveReportSettings()
        {

            UserSettings.userSettings.readVolumes = cbReadVolumes.Checked;
            //UserSettings.userSettings.warningsChecked = cbWarnings.Checked;
            //UserSettings.userSettings.errorsChecked = cbErrors.Checked;
            //UserSettings.userSettings.conLostChecked = cbConLost.Checked;

            UserSettings.userSettings.countersFilePath = filePath;
            UserSettings.userSettings.countersFolderPath = folderPath;
            UserSettings.userSettings.volumesFilePath = tbVolumesFilePath.Text;

            UserSettings.userSettings.Tz = tbTz.Value;
            UserSettings.userSettings.Ty = tbTy.Value;
            UserSettings.userSettings.Gx = tbGx.Value;

            List<BuildingReport> buildingReport = new List<BuildingReport>();
            buildingReport = (from DataRow dr in _report.Rows
                              select new BuildingReport
                              {
                                  number = dr[0].ToString(),
                                  address = dr[1].ToString(),
                                  error = dr[2].ToString()

                              }).ToList();

            UserSettings.userSettings.buildingReport = buildingReport;

            UserSettings.userSettings.Save();

        }
        // Сохранить настройки ошибок в хранилище
        private void SaveErrorSettings()
        {

            List<ErrorDescription> _errorPatterns = new List<ErrorDescription>();

            for (int i = 0; i < dgvSettings.RowCount; i++)
            {

                string errorClass = (String)dgvSettings[0, i].Value;
                string errorName = (String)dgvSettings[1, i].Value;
                bool major = (bool)dgvSettings[2, i].Value;
                string errorMessage = (String)dgvSettings[3, i].Value;
                bool checkedState = lbErrors.CheckedIndices.Cast<int>().ToList().Contains(i);


                _errorPatterns.Add(new ErrorDescription
                {
                    index = i,
                    errorClass = errorClass,
                    errorName = errorName,
                    major = major,
                    errorMessage = errorMessage,
                    checkedState = checkedState
                });
            }

            UserSettings.userSettings.showMajor = cbMajor.Checked;
            UserSettings.userSettings.reportErrorPatterns = _errorPatterns;

            UserSettings.userSettings.Save();

        }
        // Сохранить файл отчета
        private string saveToExcel(string _strPath)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {

                    var newReport = MakeReport(consumerList, false);


                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");


                    worksheet.Cells["A1"].LoadFromDataTable(newReport, true);
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    worksheet.Cells.Style.WrapText = true;


                    if (File.Exists(_strPath))
                        File.Delete(_strPath);

                    FileStream objFileStrm = File.Create(_strPath);
                    objFileStrm.Close();

                    File.WriteAllBytes(_strPath, excelPackage.GetAsByteArray());

                    return "Файл " + _strPath + " успешно сохранен.";
                }
            }
            catch (Exception ex)
            {
                return "Возникла ошибка при сохранении файла: " + ex.Message;
            }
        }
        #endregion

        #region Reloaders
        private void ReloadErrors()
        {
            lbErrors.Items.Clear();
            if (cbMajor.Checked)
            {
                foreach (var error in UserSettings.userSettings.reportErrorPatterns.ToList())
                {
                    lbErrors.Items.Add(error.errorName, error.major);
                }

            }
            else
            {
                foreach (var error in UserSettings.userSettings.reportErrorPatterns.ToList())
                {
                    lbErrors.Items.Add(error.errorName, error.checkedState);
                }
            }

            lbErrors.Enabled = !(cbMajor.Checked);

            //foreach (var error in UserSettings.userSettings.reportErrorPatterns.Where(e => e.errorType == Error.errorTypeCriticalError).ToList())
            //{
            //    lbCriticalErrors.Items.Add(error.errorName, error.checkedState);
            //}

            //cbConLost.Checked = UserSettings.userSettings.errorPatterns.Where(e => e.errorType == Error.errorTypeConLost).First().checkedState;
        }
        private void ReloadReport()
        {
            if (UserSettings.userSettings.countersFilePath == "" && UserSettings.userSettings.countersFolderPath == "")
            {
                //UserSettings.userSettings.checkReport = Error.errorPatterns.ToList();
                PrepareForm();
            }
            else
            {
                //PrepareAndRunProcessFiles();
                PrepareDataToProcess();
                ProcessFiles(null);
                ReportReady();
            }
        }

        #endregion

        #region Form members events

        private void fMain_Shown(object sender, EventArgs e)
        {
            lblGx.Text = tbGx.Value + " %";
            lblTy.Text = tbTy.Value + " °C";
            lblTz.Text = tbTz.Value + " °C";

            ReloadErrors();
            ChangeReadVolumesBtnVisibility();
            //ReloadReport();

        }
        private void fMain_Load(object sender, EventArgs e)
        {

            if (UserSettings.userSettings.reportErrorPatterns == null)
            {

                    UserSettings.userSettings.reportErrorPatterns = Error.errorPatterns.ToList();
                    UserSettings.userSettings.Save();
            }

            cbReadVolumes.Checked = UserSettings.userSettings.readVolumes;
            
            //cbWarnings.Checked = UserSettings.userSettings.warningsChecked;
            //cbErrors.Checked = UserSettings.userSettings.errorsChecked;
            //cbConLost.Checked = UserSettings.userSettings.conLostChecked;

            tbVolumesFilePath.Text = UserSettings.userSettings.volumesFilePath;

            contractPath = UserSettings.userSettings.volumesFilePath;
            filePath = UserSettings.userSettings.countersFilePath;
            folderPath = UserSettings.userSettings.countersFolderPath;
            tbCountersFilePath.Text = filePath.Equals(String.Empty) ? UserSettings.userSettings.countersFolderPath :
                UserSettings.userSettings.countersFilePath;
            

            tbTz.Value = UserSettings.userSettings.Tz;
            tbTy.Value = UserSettings.userSettings.Ty;
            tbGx.Value = UserSettings.userSettings.Gx;

            foreach (var error in UserSettings.userSettings.reportErrorPatterns)
            {
                dgvSettings.Rows.Add(error.errorClass, error.errorName, error.major,
                    error.errorMessage);
            }

            dgvSettings.ClearSelection();

            cbMajor.Checked = UserSettings.userSettings.showMajor;
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveErrorSettings();
            SaveReportSettings();
        }
 
        private void ChangeReadVolumesBtnVisibility()
        {
            if (cbReadVolumes.Checked)
            {
                pnVolumes.Visible = true;
            }
            else
            {
                pnVolumes.Visible = false;
            }
        }


        private void btnChooseCountersFile_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    tbCountersFilePath.Text = filePath;
                    folderPath = String.Empty;
                    //PrepareAndRunProcessFiles();
                }
            }
        }

        private void PrepareForm()
        {
            pnResult.Visible = false;
            pnProgress.Visible = false;
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Thread.Sleep(1000);
            this.Invoke((MethodInvoker)delegate ()
            {
                pg1.Value = e.ProgressPercentage;
                pg1.Update();
                tbLog.AppendText(e.UserState.ToString());
            });

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;

            ProcessFiles(backgroundWorker);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                ReportReady();
                _exceptions.ToList().ForEach(e => tbExceptions.AppendText(e + "\r\n"));
                tbExceptions.SelectionStart = 0;
                tbExceptions.ScrollToCaret();
            }
        }

        private void tbTz_Scroll(object sender, EventArgs e)
        {
            lblTz.Text = tbTz.Value.ToString() + "°C";
            CheckConsumers(consumerList, _exceptions);
            RefreshReport();
        }

        private void tbTy_Scroll(object sender, EventArgs e)
        {
            lblTy.Text = tbTy.Value.ToString() + "°C";
            CheckConsumers(consumerList, _exceptions);
            RefreshReport();
        }
        private void tbGx_Scroll(object sender, EventArgs e)
        {
            lblGx.Text = tbGx.Value.ToString() + " %";
            CheckConsumers(consumerList, _exceptions);
            RefreshReport();
        }

        private void btnVolumesFile_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    contractPath = openFileDialog.FileName;
                    tbVolumesFilePath.Text = contractPath;
                }
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                //saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    var _filePath = saveFileDialog.FileName;
                    var _result = saveToExcel(_filePath);

                    string caption = "Результат сохранения файла";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    MessageBox.Show(_result, caption, buttons);
                }
            }
        }

        private void dgvReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                int i = e.RowIndex;

                var _value = !(bool)dgvReport.Rows[i].Cells[3].Value;
                _report.Rows[i][3] = _value;

                dgvReport.CurrentCell = null;
            }

            dgvReport.DataSource = _report;

        }

        private void dgvReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (e.ListChangedType == ListChangedType.Reset)
            {
                for (int i = 0; i < dgvReport.Rows.Count - 1; i++)
                {
                    FormatDgv(i);
                }
            }
            else
            {
                FormatDgv(((DataGridView)sender).CurrentRow.Index);
            }
            dgvReport.Columns[3].Visible = false;

        }

        private void FormatDgv(int i)
        {
            if (_report.Rows[i][2] != null)
            {
                if (_report.Rows[i][3].Equals(false))
                {
                    dgvReport[2, i].Style = new DataGridViewCellStyle() { ForeColor = Color.DarkRed };
                    dgvReport[2, i].Style.Font = new Font(dgvReport.Font, FontStyle.Underline);
                    dgvReport.AutoResizeRow(i, DataGridViewAutoSizeRowMode.AllCells);
                    dgvReport.Rows[i].Height = dgvReport.RowTemplate.Height;
                }
                else //if (_report.Rows[i][2].ToString() != "")
                {
                    dgvReport[2, i].Style = new DataGridViewCellStyle() { ForeColor = Color.Black };
                    dgvReport[2, i].Style.Font = new Font(dgvReport.Font, FontStyle.Regular);
                    dgvReport.AutoResizeRow(i, DataGridViewAutoSizeRowMode.AllCells);
                }
            }

        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folderPath = fbd.SelectedPath;
                    tbCountersFilePath.Text = folderPath;
                    filePath = String.Empty;
                }
            }

        }

        private void btn_Process_Click(object sender, EventArgs e)
        {
            string _result = String.Empty;
            string caption = "Ошибка";
            if (folderPath.Equals(String.Empty) && filePath.Equals(String.Empty))
            {
                _result = "Выберите файлы показаний.";
                MessageBox.Show(_result, caption, MessageBoxButtons.OKCancel);
                return;
            }
            //if (contractPath.Equals(String.Empty))
            //{

            //    _result = "Выберите файл договорных объемов.";
            //    MessageBox.Show(_result, caption, MessageBoxButtons.OKCancel);
            //    return;
            //}

            PrepareAndRunProcessFiles();
        }

        private void cbReadVolumes_CheckedChanged(object sender, EventArgs e)
        {
            ChangeReadVolumesBtnVisibility();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SaveErrorSettings();
            ReloadErrors();
            RefreshReport();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvReport.DataSource = null;
            filePath = String.Empty;
            folderPath = String.Empty;
            tbCountersFilePath.Text = String.Empty;
        }
        private void lbErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReport();
        }

        private void cbMajor_CheckedChanged(object sender, EventArgs e)
        {
            SaveErrorSettings();
            ReloadErrors();
            RefreshReport();
        }
        #endregion


    }
}