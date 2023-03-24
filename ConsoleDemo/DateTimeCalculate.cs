namespace ConsoleDemo;

/// <summary>
/// 工作班次，按日分组
/// </summary>
public class DateTimeCalculate {
    private readonly DateTime _dateStart;
    private readonly DateTime _endTime;
    private readonly List<ReliefSeekCondition> _firstConditions = new List<ReliefSeekCondition>();
    private readonly List<ReliefSeekCondition> _secondConditions = new List<ReliefSeekCondition>();
    private readonly int _shiftDays;
    private readonly DateTime _startTime;


    public DateTimeCalculate(DateTime dateStart,
        int shiftDays,
        DateTime startTime,
        DateTime endTime) {
        _dateStart = dateStart.Date;
        _shiftDays = shiftDays;
        _startTime = startTime;
        _endTime = endTime;
    }

    public void Calculate(DateTime seekStartDate, DateTime seekEndDate) {
        _firstConditions.Clear();
        _secondConditions.Clear();
        var totalDays = (seekStartDate - _dateStart).TotalDays;
        var multiple = Math.Floor(totalDays / _shiftDays);
        var daysLeft = (int)(totalDays - multiple * _shiftDays);

        // 是否是首班工作时间
        var isFirstRelief = multiple / 2 == 0;
        var seekDays = (seekEndDate - seekStartDate).TotalDays;

        var list1 = isFirstRelief ? _firstConditions : _secondConditions;
        var list2 = isFirstRelief ? _secondConditions : _firstConditions;
        var no = 0;
        for(var i = 0; i <= seekDays; i++){
            no++;
            var daysAdded = i + daysLeft;
            if(daysAdded == _shiftDays - 1){
                list1.Add(new ReliefSeekCondition(no,
                    GetDateTime(seekStartDate, i, _startTime),
                    GetDateTime(seekStartDate, i + 1, _startTime)));
                (list1, list2) = (list2, list1);
            }
            else{
                list1.Add(new ReliefSeekCondition(no,
                    GetDateTime(seekStartDate, i, _startTime),
                    GetDateTime(seekStartDate, i, _endTime)));
                list2.Add(new ReliefSeekCondition(no,
                    GetDateTime(seekStartDate, i, _endTime),
                    GetDateTime(seekStartDate, i + 1, _startTime)));
            }
        }

        Console.WriteLine($"\n查询时段:{
            seekStartDate.ToLongDateString()
        }——{
            seekEndDate.ToLongDateString()
        }");
        Console.WriteLine($"距离设置日期:{totalDays}天，周期数:{multiple}，"
                          + $"余数:{daysLeft}天 查询天数:{seekDays + 1} ");
        PrintList("A班", _firstConditions);
        Console.WriteLine("");
        PrintList("B班", _secondConditions);
    }

    private void PrintList(string name, List<ReliefSeekCondition> list) {
        Console.WriteLine($"{name} 记录数:{list.Count}， 工作时间:");
        foreach(var condition in list)
            Console.WriteLine($"   序号:{condition.No:000} 天:{condition.StartTime.Day:D2} "
                              + $"Times:{
                                  condition.StartTime
                                  :yyyy-MM-dd HH:mm}~{
                                  condition.EndTime
                                  :yyyy-MM-dd HH:mm}");
    }

    private DateTime GetDateTime(DateTime startDate, int daysAdded, DateTime time) {
        return Convert.ToDateTime($"{
            startDate.AddDays(daysAdded).ToLongDateString()
        } {
            time.ToLongTimeString()
        }");
    }

    public static void Test() {
        var dateTimeCalculate = new DateTimeCalculate(new DateTime(2023, 3, 6), 7,
            new DateTime(2023, 3, 6, 7, 0, 0),
            new DateTime(2023, 3, 6, 19, 30, 0));
        dateTimeCalculate.Calculate(new DateTime(2023, 3, 6),
            new DateTime(2023, 3, 13));

        dateTimeCalculate.Calculate(new DateTime(2023, 3, 8),
            new DateTime(2023, 3, 13));

        dateTimeCalculate.Calculate(new DateTime(2023, 3, 13),
            new DateTime(2023, 3, 22));

        dateTimeCalculate.Calculate(new DateTime(2023, 2, 27),
            new DateTime(2023, 3, 8));
    }
}

public class ReliefSeekCondition {
    public ReliefSeekCondition(int no, DateTime startTime, DateTime endTime) {
        No = no;
        StartTime = startTime;
        EndTime = endTime;
    }

    /// <summary>
    ///     时间段内的序号
    /// </summary>
    public int No {get; set;}

    /// <summary>
    ///     开始时间
    /// </summary>
    public DateTime StartTime {get; set;}

    /// <summary>
    ///     结束时间
    /// </summary>
    public DateTime EndTime {get; set;}
}